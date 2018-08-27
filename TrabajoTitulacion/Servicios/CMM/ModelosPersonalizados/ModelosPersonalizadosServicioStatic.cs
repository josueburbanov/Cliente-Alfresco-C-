using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrabajoTitulacion.Modelos.CMM;

namespace TrabajoTitulacion.Servicios.CMM.ModelosPersonalizados
{
    class ModelosPersonalizadosServicioStatic
    {
        public static async Task<List<Model>> ObtenerModelosPersonalizados()
        {
            List<Model> modelosPersonalizados = new List<Model>();
            ModelosPersonalizadosServicio modelosPersonalizadosServicio = new ModelosPersonalizadosServicio();
            string respuestaJson = await modelosPersonalizadosServicio.ObtenerModelosPersonalizados();

            //Se deserializa y luego serializa para obtener una lista de nodos (Elimina metadatos de descarga)
            dynamic respuestaDeserializada = JsonConvert.DeserializeObject(respuestaJson);
            string modelosPersonalizadosJson = JsonConvert.SerializeObject(respuestaDeserializada.list.entries);
            dynamic modelosNoMapeados = JsonConvert.DeserializeObject(modelosPersonalizadosJson);

            //Nota: No se deserializa directo, porque hay que eliminar metadatos de descarga de cada nodo
            foreach (var modelo in modelosNoMapeados)
            {
                string modeloJson = JsonConvert.SerializeObject(modelo.entry);
                Model modeloLimpio = JsonConvert.DeserializeObject<Model>(modeloJson);
                modelosPersonalizados.Add(modeloLimpio);
            }
            return modelosPersonalizados;
        }

        public static async Task<Model> ObtenerModeloPersonalizado(string nombreModelo)
        {
            ModelosPersonalizadosServicio modelosPersonalizadosServicio = new ModelosPersonalizadosServicio();
            string respuestaJson = await modelosPersonalizadosServicio.ObtenerModeloPersonalizado(nombreModelo);
            dynamic respuestaDeserializada = JsonConvert.DeserializeObject(respuestaJson);
            string modeloPersonalizadoJson = JsonConvert.SerializeObject(respuestaDeserializada.entry);
            return JsonConvert.DeserializeObject<Model>(modeloPersonalizadoJson);

        }
        public static async Task<Model> ObtenerModeloPersonalizadoxPrefijo(string prefijoModelo)
        {
            List<Model> modelosPersonalizados = await ObtenerModelosPersonalizados();
            Model modeloPersonalizado = modelosPersonalizados.Find(x => x.NamespacePrefix == prefijoModelo);
            return modeloPersonalizado;
        }
        public static async Task CrearModeloPersonalizado(Model modelo)
        {
            ModelosPersonalizadosServicio modelosPersonalizadosServicio = new ModelosPersonalizadosServicio();
            string modeloJson = JsonConvert.SerializeObject(modelo);
            await modelosPersonalizadosServicio.CrearModeloPersonalizado(modeloJson);
        }
        public static async Task ActualizarModeloPersonalizado(Model modelo)
        {
            ModelosPersonalizadosServicio modelosPersonalizadosServicio = new ModelosPersonalizadosServicio();
            string modeloJson = JsonConvert.SerializeObject(modelo);
            await modelosPersonalizadosServicio.ActualizarModeloPersonalizado(modelo.Name,modeloJson);
        }
        public static async Task CambiarEstadoModeloPersonalizado(Model modelo)
        {
            ModelosPersonalizadosServicio modelosPersonalizadosServicio = new ModelosPersonalizadosServicio();
            if (modelo.Status == "ACTIVE")
                await modelosPersonalizadosServicio.DesactivarModeloPersonalizado(modelo.Name);
            else if (modelo.Status == "DRAFT")
                await modelosPersonalizadosServicio.ActivarModeloPersonalizado(modelo.Name);
        }
        public static async Task EliminarModeloPersonalizado(string nombreModelo)
        {
            ModelosPersonalizadosServicio modelosPersonalizadosServicio = new ModelosPersonalizadosServicio();
            await modelosPersonalizadosServicio.EliminarModeloPersonalizado(nombreModelo);
        }
    }
}
