using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrabajoTitulacion.Modelos.CMM;

namespace TrabajoTitulacion.Servicios.CMM.AspectosPersonalizados
{
    class AspectosPersonalizadosServicioStatic
    {
        public async static Task<List<Aspect>> ObtenerAspectosPersonalizados(string nombreModelo)
        {
            List<Aspect> aspectosPersonalizados = new List<Aspect>();
            AspectosPersonalizadosServicio aspectosPersonalizadosServicio = new AspectosPersonalizadosServicio();
            string respuestaJson = await aspectosPersonalizadosServicio.ObtenerAspectosPersonalizados(nombreModelo);

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
    }
}
