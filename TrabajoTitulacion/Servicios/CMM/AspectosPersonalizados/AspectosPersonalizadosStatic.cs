using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrabajoTitulacion.Modelos.CMM;
using TrabajoTitulacion.Servicios.CMM.ModelosPersonalizados;

namespace TrabajoTitulacion.Servicios.CMM.AspectosPersonalizados
{
    class AspectosPersonalizadosStatic
    {
        public async static Task<List<Aspect>> ObtenerAspectosPersonalizados(string nombreModelo)
        {
            List<Aspect> aspectosPersonalizados = new List<Aspect>();
            AspectosPersonalizadosServicio aspectosPersonalizadosServicio = new AspectosPersonalizadosServicio();
            string respuestaJson = await aspectosPersonalizadosServicio.ObtenerAspectos(nombreModelo);

            //Se deserializa y luego serializa para obtener una lista de nodos (Elimina metadatos de descarga)
            dynamic respuestaDeserializada = JsonConvert.DeserializeObject(respuestaJson);
            string aspectosPersonalizadosJson = JsonConvert.SerializeObject(respuestaDeserializada.list.entries);
            dynamic aspectosNoMapeados = JsonConvert.DeserializeObject(aspectosPersonalizadosJson);

            //Nota: No se deserializa directo, porque hay que eliminar metadatos de descarga de cada nodo
            foreach (var aspecto in aspectosNoMapeados)
            {
                string aspectoJson = JsonConvert.SerializeObject(aspecto.entry);
                Aspect aspectoLimpio = JsonConvert.DeserializeObject<Aspect>(aspectoJson);
                aspectosPersonalizados.Add(aspectoLimpio);
            }
            return aspectosPersonalizados;
        }

        public async static Task<List<Aspect>> ObtenerAspectosActivos()
        {
            List<Model> modelos = await ModelosPersonalizadosStatic.ObtenerModelosPersonalizados();
            List<Aspect> aspectosPersonalizados = new List<Aspect>();
            foreach (var modelo in modelos)
            {
                if (modelo.Status == "ACTIVE")
                {
                    aspectosPersonalizados.AddRange(await ObtenerAspectosPersonalizados(modelo.Name));
                }
            }
            return aspectosPersonalizados;
        }

        public async static Task CrearAspectoPersonalizado(Aspect aspectoCrear)
        {
            AspectosPersonalizadosServicio aspectosPersonalizadosServicio = new AspectosPersonalizadosServicio();
            string aspectoJson = JsonConvert.SerializeObject(aspectoCrear);
            await aspectosPersonalizadosServicio.CrearAspecto(
                aspectoCrear.ModeloPerteneciente.Name, aspectoJson);
        }
        public async static Task ActualizarAspectoPersonalizado(Aspect aspectoActualizar)
        {
            AspectosPersonalizadosServicio aspectosPersonalizadosServicio = new AspectosPersonalizadosServicio();
            string aspectoJson = JsonConvert.SerializeObject(aspectoActualizar);
            await aspectosPersonalizadosServicio.ActualizarAspecto(
                aspectoActualizar.ModeloPerteneciente.Name,
                aspectoActualizar.Name,
                aspectoJson);
        }
        public async static Task EliminarAspectoPersonalizado(string nombreModelo, string nombreAspecto)
        {
            AspectosPersonalizadosServicio aspectosPersonalizadosServicio = new AspectosPersonalizadosServicio();
            await aspectosPersonalizadosServicio.EliminarAspecto(
                nombreModelo, nombreAspecto);
        }
        public async static Task<Aspect> ObtenerAspectoPersonalizado(string nombreModelo, string nombreAspecto)
        {
            AspectosPersonalizadosServicio aspectosPersonalizadosServicio = new AspectosPersonalizadosServicio();
            string respuestaJson = await aspectosPersonalizadosServicio.ObtenerAspecto(nombreModelo, nombreAspecto);

            //Se deserializa y luego serializa para obtener una lista de aspectos (Elimina metadatos de descarga)
            dynamic respuestaDeserializada = JsonConvert.DeserializeObject(respuestaJson);
            string aspectoPersonalizadoJson = JsonConvert.SerializeObject(respuestaDeserializada.entry);
            return JsonConvert.DeserializeObject<Aspect>(aspectoPersonalizadoJson);
        }
        public async static Task AñadirPropiedadeAspecto(string nombreModelo, string nombreAspecto, PropertiesBodyUpdate propertiesBodyCreate)
        {
            AspectosPersonalizadosServicio aspectosPersonalizadosServicio = new AspectosPersonalizadosServicio();
            string propertiesBodyUpdateJson = JsonConvert.SerializeObject(propertiesBodyCreate);
            await aspectosPersonalizadosServicio.AñadirPropiedadAspecto(
                nombreModelo,
                nombreAspecto,
                propertiesBodyUpdateJson);
        }
        public async static Task ActualizarPropiedadAspecto(string nombreModelo, string nombreAspecto, string nombrePropiedad, PropertiesBodyUpdate propiedadesActualizar)
        {
            AspectosPersonalizadosServicio aspectosPersonalizadosServicio = new AspectosPersonalizadosServicio();
            string propiedadActualizarJson = JsonConvert.SerializeObject(propiedadesActualizar);
            await aspectosPersonalizadosServicio.ActualizarPropiedadAspecto(nombreModelo, nombreAspecto, nombrePropiedad, propiedadActualizarJson);
        }
        public async static Task EliminarPropiedadAspecto(string nombreModelo, string nombreAspecto, string nombrePropiedad)
        {
            AspectosPersonalizadosServicio aspectosPersonalizadosServicio = new AspectosPersonalizadosServicio();
            await aspectosPersonalizadosServicio.EliminarPropiedadAspecto(nombreModelo, nombreAspecto, nombrePropiedad);
        }
    }
}