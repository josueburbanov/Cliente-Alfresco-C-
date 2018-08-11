﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrabajoTitulacion.Modelos.CMM;
using TrabajoTitulacion.Modelos.Core;
using TrabajoTitulacion.Servicios.CMM.ModelosPersonalizados;
using TrabajoTitulacion.Servicios.CMM.TiposPersonalizados;

namespace TrabajoTitulacion.Servicios.Core.Nodos
{
    class NodosServicioStatic
    {
        private static List<Nodo> nodosDeRoot = new List<Nodo>();

        public static void ObtenerContenido(string idNodo, string path)
        {
            NodosServicio servicioNodos = new NodosServicio();
            servicioNodos.ObtenerContenido(idNodo, path);
        }

        public async static Task<Nodo> ObtenerNodo(string idNodo)
        {
            NodosServicio servicioNodos = new NodosServicio();
            string respuestaJson = await servicioNodos.ObtenerNodo(idNodo);

            //Se deserializa y luego serializa para obtener una lista de nodos (Elimina metadatos de descarga)
            dynamic respuestaDeserializada = JsonConvert.DeserializeObject(respuestaJson);
            string nodoJson = JsonConvert.SerializeObject(respuestaDeserializada.entry);
            Nodo nodoListo = JsonConvert.DeserializeObject<Nodo>(nodoJson);

            if (nodoListo.IsFile)
            {
                //Añadir Aspectos por defecto (Objetos) del nodo
                AñadirAspectos(respuestaDeserializada.entry.properties, nodoListo);

                await AñadirTipoPersonalizado(respuestaDeserializada.entry.properties, nodoListo);
            }


            return nodoListo;
        }

        private async static Task AñadirTipoPersonalizado(dynamic propiedadesDeserializadas, Nodo nodoListo)
        {
            string prefijoModeloNodo = nodoListo.NodeType.Split(':')[0];
            string nombreTipoNodo = nodoListo.NodeType.Split(':')[1];


            //Si no existe el modelo, entonces no se añade el tipo personalizado
            Model modelo = await ModelosPersonalizadosServicioStatic.ObtenerModeloPersonalizadoxPrefijo(prefijoModeloNodo);
            if (!(modelo is null))
            {
                Type1 tipoNodo = await TiposPersonalizadosServicioStatic.ObtenerTipoPersonalizado(modelo.Name, nombreTipoNodo);

                string nodoPropertiesJson = JsonConvert.SerializeObject(propiedadesDeserializadas);
                Dictionary<string, dynamic> nodoProperties = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(nodoPropertiesJson);
                foreach (var propiedad in nodoProperties)
                {
                    Property propiedadTemp = tipoNodo.Properties.Find(x => x.PrefixedName == propiedad.Key);
                    if (!(propiedadTemp is null))
                    {
                        propiedadTemp.Value = propiedad.Value;
                        continue;
                    }
                }
                nodoListo.TipoNodo = tipoNodo;
            }
        }

        /// <summary>
        /// Mapea Aspectos y sus propiedades de un Nodo JSON a los Aspectos y sus propiedades de un Objeto Nodo
        /// </summary>
        /// <param name="propiedadesDeserializadas">Nodo JSON con metadatos de descarga</param>
        /// <param name="nodoListo">Objeto Nodo, deserializado pero aun sin Aspectos. Se le añadirá sus Aspectos</param>
        private static void AñadirAspectos(dynamic propiedadesDeserializadas, Nodo nodoListo)
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
            //Properties de Aspectos:
            AñadirPropiedadesAspectos(propiedadesDeserializadas, aspectosNodo, nodoListo);
        }

        /// <summary>
        /// Añade las propiedades de un Aspecto, provenientes de un nodo JSON, a un Objeto Nodo
        /// </summary>
        /// <param name="propiedadesDeserializadas">Nodo JSON con metadatos de descarga</param>
        /// <param name="aspectosNodo">Lista de Aspectos que posee el Objeto Nodo</param>
        /// <param name="nodoListo">Objeto Nodo, deserializado pero aun sin Aspectos. Se le añadirá sus Aspectos</param>
        private static void AñadirPropiedadesAspectos(dynamic propiedadesDeserializadas, List<Aspect> aspectosNodo, Nodo nodoListo)
        {
            string nodoPropertiesJson = JsonConvert.SerializeObject(propiedadesDeserializadas);
            Dictionary<string, dynamic> nodoProperties = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(nodoPropertiesJson);
            foreach (var aspecto in aspectosNodo)
            {
                foreach (var property in nodoProperties)
                {
                    Property propertyTemp = aspecto.Properties.Find(x => x.Name == property.Key);
                    if (!(propertyTemp is null))
                    {
                        propertyTemp.Value = property.Value;
                        continue;
                    }
                }
            }
            nodoListo.Aspectos = aspectosNodo;
        }

        public async static Task<List<Nodo>> ObtenerListaNodosHijos(string nodoPadreId)
        {
            List<Nodo> nodosHijos = new List<Nodo>();
            NodosServicio servicioNodos = new NodosServicio();
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

        public async static Task<Nodo> ActualizarNodo(Nodo nodoActualizar)
        {
            NodosServicio nodosServicio = new NodosServicio();
            string nodoJsonActualizar = JsonConvert.SerializeObject(nodoActualizar);
            Console.WriteLine(nodoJsonActualizar);

            string respuestaJson = await nodosServicio.ActualizarNodo(nodoActualizar.Id, nodoJsonActualizar);

            dynamic respuestaDeserializada = JsonConvert.DeserializeObject(respuestaJson);
            string nodoJson = JsonConvert.SerializeObject(respuestaDeserializada.entry);
            Nodo nodoListo = JsonConvert.DeserializeObject<Nodo>(nodoJson);

            if (nodoListo.IsFile)
            {
                //Añadir Aspectos por defecto (Objetos) del nodo
                AñadirAspectos(respuestaDeserializada.entry.properties, nodoListo);

                await AñadirTipoPersonalizado(respuestaDeserializada.entry.properties, nodoListo);
            }
            return nodoListo;


        }
    }
}