using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrabajoTitulacion.Modelos.CMM;
using TrabajoTitulacion.Modelos.Core;
using TrabajoTitulacion.Servicios.CMM.AspectosPersonalizados;
using TrabajoTitulacion.Servicios.CMM.ModelosPersonalizados;
using TrabajoTitulacion.Servicios.CMM.TiposPersonalizados;
using RestSharp.Extensions;
using DSOFile;
using TrabajoTitulacion.Modelos.CoreAPI;
using System.IO;
using TrabajoTitulacion.Modelos.Utils;

namespace TrabajoTitulacion.Servicios.Core.Nodos
{
    class NodosStatic
    {
        static NodosServicio servicioNodos = new NodosServicio();
        public async static Task ObtenerContenido(string idNodo, string path)
        {
            (await servicioNodos.ObtenerContenido(idNodo)).SaveAs(path);
        }

        public async static Task ObtenerContenido(Nodo nodo, string path)
        {
            if (nodo.IsFile)
            {
                (await servicioNodos.ObtenerContenido(nodo.Id)).SaveAs(path);
            }
            else
            {
                Directory.CreateDirectory(path);
                nodo.NodosHijos = await ObtenerListaNodosHijos(nodo.Id);
                if (!(nodo.NodosHijos is null))
                {
                    foreach (var item in nodo.NodosHijos)
                    {
                        await ObtenerContenido(item, path + "\\" + item.Name);
                    }
                }

            }

        }

        public async static Task<Nodo> CrearNodoContenido(string idNodoPadre, Nodo nodo, byte[] contenido)
        {
            NodeBodyCreate nodeBodyCreate = new NodeBodyCreate
            {
                Name = nodo.Name,
                NodeType = nodo.NodeType,
                Properties = (Dictionary<string, string>)nodo.Properties
            };
            string nodeBodyCreateJson = JsonConvert.SerializeObject(nodeBodyCreate);
            string respuestaJson = await servicioNodos.CrearNodo(idNodoPadre, nodeBodyCreateJson);
            Nodo nodoListo = DeserializarNodoJson(respuestaJson);

            await servicioNodos.ActualizarContenido(nodoListo.Id, true, null, nodoListo.Name, null, null, contenido);
            return nodoListo;
        }

        public async static Task<Nodo> ObtenerNodo(string idNodo)
        {
            string respuestaJson = await servicioNodos.ObtenerNodo(idNodo);

            if (respuestaJson is null)
            {
                return null;
            }
            else
            {

                return DeserializarNodoJson(respuestaJson);
            }
        }

        public async static Task<Nodo> ObtenerNodoPersonalizado(string idNodo)
        {
            string respuestaJson = await servicioNodos.ObtenerNodo(idNodo);

            if (respuestaJson is null)
            {
                return null;
            }
            else
            {

                //Se deserializa y luego serializa para obtener una lista de nodos (Elimina metadatos de descarga)
                dynamic respuestaDeserializada = JsonConvert.DeserializeObject(respuestaJson);
                string nodoJson = JsonConvert.SerializeObject(respuestaDeserializada.entry);
                Nodo nodoListo = JsonConvert.DeserializeObject<Nodo>(nodoJson);

                await AñadirTipoPersonalizado(respuestaDeserializada.entry.properties, nodoListo);
                //Añadir Aspectos por defecto (Objetos) del nodo
                await AñadirAspectos(respuestaDeserializada.entry.properties, nodoListo);
                return nodoListo;
            }
        }


        /// <summary>
        /// Mapea el tipo actual del nodo, al atributo TipoNodo del objeto Nodo
        /// </summary>
        /// <param name="propiedadesDeserializadas"></param>
        /// <param name="nodoListo"></param>
        /// <returns></returns>
        private async static Task AñadirTipoPersonalizado(dynamic propiedadesDeserializadas, Nodo nodoListo)
        {
            string prefijoModeloNodo = nodoListo.NodeType.Split(':')[0];
            string nombreTipoNodo = nodoListo.NodeType.Split(':')[1];

            //Si no existe el modelo, entonces no se añade el tipo personalizado
            Model modelo = await ModelosPersonalizadosStatic.ObtenerModeloPersonalizadoxPrefijo(prefijoModeloNodo);
            if (!(modelo is null))
            {
                Modelos.CMM.Type tipoNodo = await TiposPersonalizadosStatic.ObtenerTipoPersonalizado(modelo.Name, nombreTipoNodo);

                string nodoPropertiesJson = JsonConvert.SerializeObject(propiedadesDeserializadas);
                Dictionary<string, dynamic> nodoProperties = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(nodoPropertiesJson);

                if (!(nodoProperties is null))
                {
                    foreach (var propiedad in nodoProperties)
                    {
                        Property propiedadTemp = tipoNodo.Properties.Find(x => x.PrefixedName == propiedad.Key);
                        if (!(propiedadTemp is null))
                        {
                            propiedadTemp.Value = propiedad.Value;
                            continue;
                        }
                    }
                }

                tipoNodo.ModeloPerteneciente = modelo;
                nodoListo.TipoNodo = tipoNodo;
            }
        }

        /// <summary>
        /// Mapea Aspectos y sus propiedades de un Nodo JSON a los Aspectos y sus propiedades de un Objeto Nodo
        /// </summary>
        /// <param name="propiedadesDeserializadas">Nodo JSON con metadatos de descarga</param>
        /// <param name="nodoListo">Objeto Nodo, deserializado pero aun sin Aspectos. Se le añadirá sus Aspectos</param>
        private static async Task AñadirAspectos(dynamic propiedadesDeserializadas, Nodo nodoListo)
        {
            List<string> nodoAspectnames = (List<string>)nodoListo.AspectNames;

            List<Aspect> aspectosNodo = new List<Aspect>();
            List<Aspect> aspectosDisponibles = Aspect.Aspects();
            foreach (var aspecto in nodoAspectnames)
            {
                foreach (var aspectoDisponible in aspectosDisponibles)
                {
                    if (aspectoDisponible.Name.Equals(aspecto))
                    {
                        aspectosNodo.Add(aspectoDisponible);
                        break;
                    }
                }
            }


            //Mapeo de aspectos personalizados
            List<Aspect> aspectosFaltantes = new List<Aspect>();
            foreach (var aspectoNoMap in nodoAspectnames)
            {
                if (aspectosNodo.Find(x => x.Name == aspectoNoMap) is null)
                {
                    string prefijoModelodesdeAspecto = aspectoNoMap.Split(':')[0];
                    string nombreAspecto = aspectoNoMap.Split(':')[1];

                    if ((await ModelosPersonalizadosStatic.ObtenerModeloPersonalizadoxPrefijo(prefijoModelodesdeAspecto) is null))
                    {
                        continue;
                    }
                    Model modelo = await ModelosPersonalizadosStatic.ObtenerModeloPersonalizadoxPrefijo(prefijoModelodesdeAspecto);
                    Aspect aspectoPersonalizado = await AspectosPersonalizadosStatic.ObtenerAspectoPersonalizado(modelo.Name, nombreAspecto);
                    aspectoPersonalizado.Showable = true;
                    aspectosFaltantes.Add(aspectoPersonalizado);
                }
            }

            if (aspectosFaltantes.Count != 0) aspectosNodo.AddRange(aspectosFaltantes);

            //Properties de Aspectos:
            AñadirPropsAspectos(propiedadesDeserializadas, aspectosNodo, nodoListo);
        }

        /// <summary>
        /// Añade las propiedades de un Aspecto, provenientes de un nodo JSON, a un Objeto Nodo
        /// </summary>
        /// <param name="propiedadesDeserializadas">Nodo JSON con metadatos de descarga</param>
        /// <param name="aspectosNodo">Lista de Aspectos que posee el Objeto Nodo</param>
        /// <param name="nodoListo">Objeto Nodo, deserializado pero aun sin Aspectos. Se le añadirá sus Aspectos</param>
        private static void AñadirPropsAspectos(dynamic propiedadesDeserializadas, List<Aspect> aspectosNodo, Nodo nodoListo)
        {
            string nodoPropertiesJson = JsonConvert.SerializeObject(propiedadesDeserializadas);
            Dictionary<string, dynamic> nodoProperties = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(nodoPropertiesJson);

            if (!(nodoProperties is null))
            {
                foreach (var property in nodoProperties)
                {
                    var propiedadBuscada = (from aspecto in aspectosNodo
                                            from propiedad in aspecto.Properties
                                            where propiedad.PrefixedName == property.Key || propiedad.Name == property.Key
                                            select propiedad).FirstOrDefault();
                    if (!(propiedadBuscada is null))
                    {
                        propiedadBuscada.Value = property.Value;
                    }
                }

            }
            nodoListo.Aspectos = aspectosNodo;
        }

        public async static Task<List<Nodo>> ObtenerListaNodosHijos(string nodoPadreId)
        {
            List<Nodo> nodosHijos = new List<Nodo>();
            string respuestaJson = await servicioNodos.ObtenerListaNodosHijos(nodoPadreId);

            //Se deserializa y luego serializa para obtener una lista de nodos (Elimina metadatos de descarga)
            dynamic respuestaDeserializada = JsonConvert.DeserializeObject(respuestaJson);
            string nodosJson = JsonConvert.SerializeObject(respuestaDeserializada.list.entries);
            dynamic nodosNoMapeados = JsonConvert.DeserializeObject(nodosJson);

            //Se itera para mapear cada nodo de la lista de nodos y añadirlos como hijos de cada nodo
            foreach (var nodo in nodosNoMapeados)
            {
                string nodoJson = JsonConvert.SerializeObject(nodo.entry);
                Nodo nodoLimpio = JsonConvert.DeserializeObject<Nodo>(nodoJson);
                nodosHijos.Add(nodoLimpio);
                //Nota: Esta respuesta no contiene Propiedades ni aspectos
                //await AñadirTipoPersonalizado(nodo.entry.properties, nodoLimpio);
                //Añadir Aspectos por defecto (Objetos) del nodo
                //await AñadirAspectos(nodo.entry.properties, nodoLimpio);
            }
            return nodosHijos;
        }


        /// <summary>
        /// Pobla los nodos hijos de una lista de nodos padres de un mismo nivel
        /// </summary>
        /// <param name="nodosPadres">Nodos padres de un mismo nivel</param>
        public async static Task PoblarNodosHijos(List<Nodo> nodosPadres)
        {
            foreach (var nodoPadre in nodosPadres)
            {
                nodoPadre.NodosHijos = await ObtenerListaNodosHijos(nodoPadre.Id);

                //Si el nodo tiene hijos
                if (nodoPadre.NodosHijos.Count() != 0 && nodoPadre.IsFolder)
                {
                    await PoblarNodosHijos(nodoPadre.NodosHijos);
                }
            }
        }

        public async static Task PoblarNodosHijosContenidos(List<Nodo> nodosPadres)
        {
            foreach (var nodoPadre in nodosPadres)
            {
                nodoPadre.NodosHijos = await ObtenerListaNodosHijos(nodoPadre.Id);
                if(nodoPadre.IsFile)nodoPadre.Contenido = await servicioNodos.ObtenerContenido(nodoPadre.Id);
                //Si el nodo tiene hijos
                if (nodoPadre.NodosHijos.Count() != 0 && nodoPadre.IsFolder)
                {
                    await PoblarNodosHijos(nodoPadre.NodosHijos);
                }
            }
        }

        public async static Task CompletarNodos(Nodo nodoRaiz)
        {
            foreach (var nodo in nodoRaiz.NodosHijos)
            {
                await CompletarNodo(nodo);
                if (nodo.NodosHijos.Count() != 0 && nodo.IsFolder)
                {
                    await CompletarNodos(nodo);
                }
            }
        }

        private async static Task CompletarNodo(Nodo nodoCompletar)
        {
            Nodo nodoCompleto = await ObtenerNodo(nodoCompletar.Id);
            nodoCompletar.AspectNames = nodoCompleto.AspectNames;
            nodoCompletar.Path = nodoCompleto.Path;
        }

        public async static Task<Nodo> ActualizarAspectosNodo(Nodo nodoActualizar)
        {
            NodeBodyUpdate nodeBodyUpdate = new NodeBodyUpdate(nodoActualizar.Name, null, nodoActualizar.AspectNames);
            string nodoBodyUpdateJson = JsonConvert.SerializeObject(nodeBodyUpdate);
            string respuestaJson = await servicioNodos.ActualizarNodo(nodoActualizar.Id, nodoBodyUpdateJson);

            dynamic respuestaDeserializada = JsonConvert.DeserializeObject(respuestaJson);
            string nodoJson = JsonConvert.SerializeObject(respuestaDeserializada.entry);
            Nodo nodoListo = JsonConvert.DeserializeObject<Nodo>(nodoJson);

            //Añadir Aspectos por defecto (Objetos) del nodo
            await AñadirAspectos(respuestaDeserializada.entry.properties, nodoListo);

            await AñadirTipoPersonalizado(respuestaDeserializada.entry.properties, nodoListo);
            return nodoListo;
        }

        public async static Task<Nodo> ActualizarPropiedadesNodo(Nodo nodoActualizar)
        {
            FormatearPropiedades(nodoActualizar);

            //Nota: nodoType=null porque no se puede actualizar al mismo tipo y aspectNames=null porque no se actualiza aspectos
            NodeBodyUpdate nodeBodyUpdate = new NodeBodyUpdate(nodoActualizar.Name, null, null,
                (Dictionary<string, string>)nodoActualizar.Properties);
            string nodoBodyUpdateJson = JsonConvert.SerializeObject(nodeBodyUpdate);
            string respuestaJson = await servicioNodos.ActualizarNodo(nodoActualizar.Id, nodoBodyUpdateJson);

            dynamic respuestaDeserializada = JsonConvert.DeserializeObject(respuestaJson);
            string nodoJson = JsonConvert.SerializeObject(respuestaDeserializada.entry);
            Nodo nodoListo = JsonConvert.DeserializeObject<Nodo>(nodoJson);


            AñadirAspectos(respuestaDeserializada.entry.properties, nodoListo);

            await AñadirTipoPersonalizado(respuestaDeserializada.entry.properties, nodoListo);
            return nodoListo;
        }


        private static List<string> FormatearAspectos(Nodo nodoActualizar)
        {
            List<string> aspectosJson = new List<string>();
            foreach (var aspecto in nodoActualizar.Aspectos)
            {
                aspectosJson.Add(aspecto.Name);
            }
            return aspectosJson;
        }
        private static void FormatearPropiedades(Nodo nodoActualizar)
        {
            Dictionary<string, string> propiedadesJson = new Dictionary<string, string>();

            //Se cargan las propiedades de aspectos
            foreach (var aspecto in nodoActualizar.Aspectos)
            {
                if (aspecto.Showable == true)
                {
                    foreach (var propiedad in aspecto.Properties)
                    {
                        if(!(propiedad.Value is null))
                        {
                            if (propiedad.Datatype == "d:date")
                            {
                                DateTime dt = DateTime.ParseExact(Convert.ToString(propiedad.Value), "dd/MM/yyyy H:mm:ss", null);
                                propiedad.Value = propiedad.Value = dt.ToString("yyyy-MM-dd");
                            }
                            else if (propiedad.Datatype == "d:date")
                            {
                                DateTime dt = DateTime.ParseExact(Convert.ToString(propiedad.Value), "dd/MM/yyyy H:mm:ss", null);
                                propiedad.Value = dt.ToString("yyyy-MM-dd HH:mm:ss");
                                
                            }
                            else if (!(propiedad.Datatype == "d:text" || propiedad.Datatype == "d:mltext")) propiedad.Value = Convert.ToString(propiedad.Value);                            
                            
                            propiedadesJson.Add(propiedad.PrefixedName, propiedad.Value);
                        }
                    }
                }
            }

            //Se cargan las propiedades del tipo personalizado 
            if (!(nodoActualizar.TipoNodo is null))
            {
                foreach (var propiedad in nodoActualizar.TipoNodo.Properties)
                {
                    if (!(propiedad.Value is null))
                    {
                        propiedadesJson.Add(propiedad.PrefixedName, propiedad.Value);
                    }
                }
            }
            nodoActualizar.Properties = propiedadesJson;
        }
        public static Nodo BuscarNodo(Nodo nodoPadre, string idNodoHijo)
        {
            Nodo nodoEncontrado = null;
            foreach (var nodo in nodoPadre.NodosHijos)
            {
                if (nodo.IsFile && nodo.Id == idNodoHijo)
                {
                    nodoEncontrado = nodo;
                    break;
                }
                else if (nodo.IsFolder && nodo.NodosHijos.Count != 0)
                {
                    nodoEncontrado = BuscarNodo(nodo, idNodoHijo);
                    break;
                }
            }
            return nodoEncontrado;
        }
        public async static Task ActualizarContenido(Nodo nodo, bool majorVersion, string comment, byte[] contentBodyUpdate)
        {
            await servicioNodos.ActualizarContenido(nodo.Id, majorVersion, comment, nodo.Name, null, null, contentBodyUpdate);
        }

        /// <summary>
        /// Crea un nodo en el servidor de Alfresco
        /// </summary>
        /// <param name="idNodoPadre">Id del nodo padre (carpeta)</param>
        /// <param name="nodo">Objeto nodo de la clase Nodo a crearse</param>
        /// <returns></returns>
        public async static Task<Nodo> CrearNodo(string idNodoPadre, Nodo nodo)
        {
            NodeBodyCreate nodeBodyCreate = new NodeBodyCreate
            {
                Name = nodo.Name,
                NodeType = nodo.NodeType,
                Properties = (Dictionary<string, string>)nodo.Properties
            };
            string nodeBodyCreateJson = JsonConvert.SerializeObject(nodeBodyCreate);
            string respuestaJson = await servicioNodos.CrearNodo(idNodoPadre, nodeBodyCreateJson);
            return DeserializarNodoJson(respuestaJson);
        }

        private static Nodo DeserializarNodoJson(string respuestaJson)
        {
            //Se deserializa y luego serializa para obtener una lista de nodos (Elimina metadatos de descarga)
            dynamic respuestaDeserializada = JsonConvert.DeserializeObject(respuestaJson);
            string nodoJson = JsonConvert.SerializeObject(respuestaDeserializada.entry);
            Nodo nodoListo = JsonConvert.DeserializeObject<Nodo>(nodoJson);
            return nodoListo;
        }

        private static List<Nodo> DeserializarArrayJson(string respuestaJson)
        {
            //Se deserializa y luego serializa para obtener una lista de nodos (Elimina metadatos de descarga)
            dynamic respuestaDeserializada = JsonConvert.DeserializeObject(respuestaJson);
            string nodosJson = JsonConvert.SerializeObject(respuestaDeserializada.list.entries);
            dynamic nodosNoMapeados = JsonConvert.DeserializeObject(nodosJson);

            List<Nodo> nodosEncontrados = new List<Nodo>();
            //Nota: No se deserializa directo, porque hay que eliminar metadatos de descarga de cada nodo
            foreach (var nodo in nodosNoMapeados)
            {
                string aspectoJson = JsonConvert.SerializeObject(nodo.entry);
                Nodo aspectoLimpio = JsonConvert.DeserializeObject<Nodo>(aspectoJson);
                nodosEncontrados.Add(aspectoLimpio);
            }
            return nodosEncontrados;
        }

        public async static Task EliminarNodo(string idNodo)
        {
            await servicioNodos.EliminarNodo(idNodo);
        }
        public async static Task ActualizarNodo(Nodo nodo)
        {
            NodeBodyUpdate nodeBodyUpdate = new NodeBodyUpdate(nodo.Name);
            Nodo nodoAnterior = await ObtenerNodo(nodo.Id);
            if (nodo.NodeType != nodoAnterior.NodeType)
            {
                nodeBodyUpdate.NodeType = nodo.NodeType;
            }
            string nodoJson = JsonConvert.SerializeObject(nodeBodyUpdate);
            await servicioNodos.ActualizarNodo(nodo.Id, nodoJson);
        }

        public async static Task<Nodo> CambiarTipo(Nodo nodo)
        {
            if (nodo.IsFile)
            {
                byte[] contenidoNodo = await servicioNodos.ObtenerContenido(nodo.Id);
                await EliminarNodo(nodo.Id);
                nodo.Properties = null;
                Nodo nodoCreado = await CrearNodoContenido(nodo.ParentId, nodo, contenidoNodo);
                if(!(nodo.Aspectos.Find(x=>x.PrefixedName == "sync:Sincronizable") is null)){
                    List<string> aspectoSync = new List<string>();
                    aspectoSync.Add("sync:Sincronizable");
                    await ActualizarAspectosNodo(nodoCreado);
                }
                return nodoCreado;
            }
            else if (nodo.IsFolder)
            {
                List<Nodo> nodosHijos = await ObtenerListaNodosHijos(nodo.Id);
                await PoblarNodosHijosContenidos(nodosHijos);
                await EliminarNodo(nodo.Id);
                nodo.Properties = null;
                Nodo nodoCreado = await CrearNodo(nodo.ParentId, nodo);
                if (!(nodo.Aspectos.Find(x => x.PrefixedName == "sync:Sincronizable") is null))
                {
                    List<string> aspectoSync = new List<string>();
                    aspectoSync.Add("sync:Sincronizable");
                    await ActualizarAspectosNodo(nodoCreado);
                }
                if (nodosHijos.Count != 0)
                {
                    await CrearNodosHijos(nodosHijos, nodoCreado.Id);
                }
                
                return nodoCreado;
            }
            return null;

        }

        private static async Task CrearNodosHijos(List<Nodo> nodosHijos, string idPadre)
        {
            foreach (var item in nodosHijos)
            {
                if (item.IsFile)
                {
                    item.Properties = null;
                    Nodo nodoCreado = await CrearNodoContenido(idPadre, item, item.Contenido);
                    if (!(item.Aspectos.Find(x => x.PrefixedName == "sync:Sincronizable") is null))
                    {
                        List<string> aspectoSync = new List<string>();
                        aspectoSync.Add("sync:Sincronizable");
                        await ActualizarAspectosNodo(nodoCreado);
                    }
                }
                else
                {
                    item.Properties = null;
                    Nodo carpetaCreada = await CrearNodo(idPadre, item);
                    if (!(item.Aspectos.Find(x => x.PrefixedName == "sync:Sincronizable") is null))
                    {
                        List<string> aspectoSync = new List<string>();
                        aspectoSync.Add("sync:Sincronizable");
                        await ActualizarAspectosNodo(carpetaCreada);
                    }
                    if (item.NodosHijos.Count != 0)
                    {
                        await CrearNodosHijos(item.NodosHijos, carpetaCreada.Id);
                    }
                }
            }
        }
    }
}


