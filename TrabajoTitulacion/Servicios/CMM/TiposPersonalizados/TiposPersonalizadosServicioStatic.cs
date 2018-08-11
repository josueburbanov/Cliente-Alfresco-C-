﻿using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrabajoTitulacion.Modelos.CMM;

namespace TrabajoTitulacion.Servicios.CMM.TiposPersonalizados
{
    class TiposPersonalizadosServicioStatic
    {
        public static async Task<List<Type1>> ObtenerTiposPersonalizados(string nombreModelo)
        {
            List<Type1> tiposPersonalizados = new List<Type1>();
            TiposPersonalizadosServicio tiposPersonalizadosServicio = new TiposPersonalizadosServicio();
            string respuestaJson = await tiposPersonalizadosServicio.ObtenerTiposPersonalizados(nombreModelo);

            //Se deserializa y luego serializa para obtener una lista de nodos (Elimina metadatos de descarga)
            dynamic respuestaDeserializada = JsonConvert.DeserializeObject(respuestaJson);
            string tiposPersonalizadosJson = JsonConvert.SerializeObject(respuestaDeserializada.list.entries);
            dynamic tiposNoMapeados = JsonConvert.DeserializeObject(tiposPersonalizadosJson);

            //Nota: No se deserializa directo, porque hay que eliminar metadatos de descarga de cada nodo
            foreach (var tipo in tiposNoMapeados)
            {
                string tipoJson = JsonConvert.SerializeObject(tipo.entry);
                Type1 tipoLimpio = JsonConvert.DeserializeObject<Type1>(tipoJson);
                tiposPersonalizados.Add(tipoLimpio);
            }
            return tiposPersonalizados;
        }

        public static async Task<Type1> ObtenerTipoPersonalizado(string nombreModelo, string nombreTipo)
        {            
            TiposPersonalizadosServicio tiposPersonalizadosServicio = new TiposPersonalizadosServicio();
            string respuestaJson = await tiposPersonalizadosServicio.ObtenerTipoPersonalizado(nombreModelo, nombreTipo);

            //Se deserializa y luego serializa para obtener una lista de nodos (Elimina metadatos de descarga)
            dynamic respuestaDeserializada = JsonConvert.DeserializeObject(respuestaJson);
            string tipoPersonalizadoJson = JsonConvert.SerializeObject(respuestaDeserializada.entry);
            return JsonConvert.DeserializeObject<Type1>(tipoPersonalizadoJson);
        }
    }
}