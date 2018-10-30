using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrabajoTitulacion.Modelos.CMM;
using TrabajoTitulacion.Servicios.CMM.ModelosPersonalizados;

namespace TrabajoTitulacion.Servicios.CMM.TiposPersonalizados
{
    class TiposPersonalizadosStatic
    {
        public static async Task<List<Type>> ObtenerTiposPersonalizados(string nombreModelo)
        {
            List<Type> tiposPersonalizados = new List<Type>();
            TiposPersonalizadosServicio tiposPersonalizadosServicio = new TiposPersonalizadosServicio();
            string respuestaJson = await tiposPersonalizadosServicio.ObtenerTipos(nombreModelo);

            //Se deserializa y luego serializa para obtener una lista de nodos (Elimina metadatos de descarga)
            dynamic respuestaDeserializada = JsonConvert.DeserializeObject(respuestaJson);
            string tiposPersonalizadosJson = JsonConvert.SerializeObject(respuestaDeserializada.list.entries);
            dynamic tiposNoMapeados = JsonConvert.DeserializeObject(tiposPersonalizadosJson);

            //Nota: No se deserializa directo, porque hay que eliminar metadatos de descarga de cada nodo
            foreach (var tipo in tiposNoMapeados)
            {
                string tipoJson = JsonConvert.SerializeObject(tipo.entry);
                Type tipoLimpio = JsonConvert.DeserializeObject<Type>(tipoJson);
                tiposPersonalizados.Add(tipoLimpio);
            }
            return tiposPersonalizados;
        }

        public static async Task<Type> ObtenerTipoPersonalizado(string nombreModelo, string nombreTipo)
        {            
            TiposPersonalizadosServicio tiposPersonalizadosServicio = new TiposPersonalizadosServicio();
            string respuestaJson = await tiposPersonalizadosServicio.ObtenerTipo(nombreModelo, nombreTipo);

            //Se deserializa y luego serializa para obtener una lista de tipos (Elimina metadatos de descarga)
            dynamic respuestaDeserializada = JsonConvert.DeserializeObject(respuestaJson);
            string tipoPersonalizadoJson = JsonConvert.SerializeObject(respuestaDeserializada.entry);
            return JsonConvert.DeserializeObject<Type>(tipoPersonalizadoJson);
        }
        public static async Task ActualizarTipoPersonalizado(Type tipo)
        {
            TiposPersonalizadosServicio tiposPersonalizadosServicio = new TiposPersonalizadosServicio();
            string tipoJson = JsonConvert.SerializeObject(tipo);
            await tiposPersonalizadosServicio.ActualizarTipo(tipo.ModeloPerteneciente.Name,
                tipo.Name, tipoJson);
        }
        public static async Task<List<Type>> ObtenerTiposActivos()
        {
            List<Model> modelos = await ModelosPersonalizadosStatic.ObtenerModelosPersonalizados();
            List<Type> tiposPersonalizados = new List<Type>();
            foreach (var modelo in modelos)
            {
                if (modelo.Status == "ACTIVE")
                {
                    tiposPersonalizados.AddRange(await ObtenerTiposPersonalizados(modelo.Name));
                }
            }
            return tiposPersonalizados;
        }
        public static async Task CrearTipoPersonalizado(Type tipo)
        {
            TiposPersonalizadosServicio tiposPersonalizadosServicio = new TiposPersonalizadosServicio();
            string tipoJson = JsonConvert.SerializeObject(tipo);
            await tiposPersonalizadosServicio.CrearTipo(tipo.ModeloPerteneciente.Name, tipoJson);
        }
        public static async Task EliminarTipoPersonalizado(string nombreModelo, string nombreTipo)
        {
            TiposPersonalizadosServicio tiposPersonalizadosServicio = new TiposPersonalizadosServicio();
            await tiposPersonalizadosServicio.EliminarTipo(nombreModelo, nombreTipo);
        }
    }
}
