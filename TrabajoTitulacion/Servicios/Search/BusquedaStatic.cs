using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrabajoTitulacion.Modelos.CMM;
using TrabajoTitulacion.Modelos.Core;
using TrabajoTitulacion.Models;

namespace TrabajoTitulacion.Servicios.Search
{
    class BusquedaStatic
    {
        private static BusquedaServicio busquedaServicio = new BusquedaServicio();

        public static async Task<List<Nodo>> BuscarNodosPorAspecto(Aspect aspecto)
        {
            RequestQuery requestQuery = new RequestQuery("ASPECT:'" + aspecto.PrefixedName + "'");
            SearchRequest searchRequest = new SearchRequest();
            searchRequest.Query = requestQuery;
            string searchRequestJson = JsonConvert.SerializeObject(searchRequest);
            string respuestaJson = await busquedaServicio.Buscar(searchRequestJson);
            return DeserializarArrayJson(respuestaJson);
        }


        public static async Task<List<Nodo>> BuscarNodosPorTipo(Modelos.CMM.Type tipo)
        {
            RequestQuery requestQuery = new RequestQuery("TYPE:'" + tipo.PrefixedName + "'");
            SearchRequest searchRequest = new SearchRequest();
            searchRequest.Query = requestQuery;
            string searchRequestJson = JsonConvert.SerializeObject(searchRequest);
            string respuestaJson = await busquedaServicio.Buscar(searchRequestJson);
            return DeserializarArrayJson(respuestaJson);
        }

        public static async Task<List<Nodo>> BuscarNodosPorPropiedad(Property propiedad)
        {
            RequestQuery requestQuery = new RequestQuery(propiedad.PrefixedName + ":*");
            SearchRequest searchRequest = new SearchRequest();
            searchRequest.Query = requestQuery;
            string searchRequestJson = JsonConvert.SerializeObject(searchRequest);
            string respuestaJson = await busquedaServicio.Buscar(searchRequestJson);
            return DeserializarArrayJson(respuestaJson);
        }

        public static async Task<List<Nodo>> BuscarNodosPorPropiedad(Property propiedad, string valorPropiedad)
        {
            RequestQuery requestQuery = new RequestQuery(propiedad.PrefixedName + ":" + valorPropiedad);
            SearchRequest searchRequest = new SearchRequest();
            searchRequest.Query = requestQuery;
            string searchRequestJson = JsonConvert.SerializeObject(searchRequest);
            string respuestaJson = await busquedaServicio.Buscar(searchRequestJson);
            return DeserializarArrayJson(respuestaJson);
        }

        public static async Task<List<Nodo>> BuscarNodosPor1Propiedad(dynamic aspectoTipo, 
            Property propiedad, string valorPropiedad1, string operacion)
        {
            string query = "";
            switch (operacion)
            {
                case "Menor que":
                    query = "select * from " + aspectoTipo.PrefixedName + " where " +
                        propiedad.PrefixedName + "<'" + valorPropiedad1 + "'";
                    break;
                case "Mayor que":
                    query = "select * from " + aspectoTipo.PrefixedName + " where " + 
                        propiedad.PrefixedName + ">'" + valorPropiedad1 + "'";
                    break;
                case "Mayor o igual que":
                    query = "select * from " + aspectoTipo.PrefixedName + " where " + 
                        propiedad.PrefixedName + ">='" + valorPropiedad1 + "'";
                    break;
                case "Menor o igual que":
                    query = "select * from " + aspectoTipo.PrefixedName + " where " +
                        propiedad.PrefixedName + "<='" + valorPropiedad1 + "'";
                    break;
                default:
                    break;
            }
            RequestQuery requestQuery = new RequestQuery(query);
            requestQuery.Language = "cmis";
            SearchRequest searchRequest = new SearchRequest();
            searchRequest.Query = requestQuery;
            string searchRequestJson = JsonConvert.SerializeObject(searchRequest);
            string respuestaJson = await busquedaServicio.Buscar(searchRequestJson);
            return DeserializarArrayJson(respuestaJson);
        }

        public static async Task<List<Nodo>> BuscarNodosPor2Propiedades(dynamic aspectoTipo, Property propiedad, string valorPropiedad1, string valorPropiedad2, string operacion)
        {
            string query = "";
            switch (operacion)
            {
                case "Rango[]":
                    query = "select * from " + aspectoTipo.PrefixedName + " where " +
                propiedad.PrefixedName + "<='" + valorPropiedad1 + "'" + " and " + propiedad.PrefixedName + ">='" + valorPropiedad2 + "'";
                    break;
                case "Rango()":
                    query = "select * from " + aspectoTipo.PrefixedName + " where " +
                propiedad.PrefixedName + "<'" + valorPropiedad1 + "'" + " and " + propiedad.PrefixedName + ">'" + valorPropiedad2 + "'";
                    break;
                case "Rango[)":
                    query = "select * from " + aspectoTipo.PrefixedName + " where " +
            propiedad.PrefixedName + "<='" + valorPropiedad1 + "'" + " and " + propiedad.PrefixedName + ">'" + valorPropiedad2 + "'";
                    break;
                case "Rango(]":
                    query = "select * from " + aspectoTipo.PrefixedName + " where " +
            propiedad.PrefixedName + "<'" + valorPropiedad1 + "'" + " and " + propiedad.PrefixedName + ">='" + valorPropiedad2 + "'";
                    break;
                default:
                    break;
            }
            RequestQuery requestQuery = new RequestQuery("select * from " + aspectoTipo.PrefixedName + " where "
                + propiedad.PrefixedName + "<'" + valorPropiedad1 + "'" + " and " + propiedad.PrefixedName + ">'" + valorPropiedad2 + "'");
            requestQuery.Language = "cmis";
            SearchRequest searchRequest = new SearchRequest();
            searchRequest.Query = requestQuery;
            string searchRequestJson = JsonConvert.SerializeObject(searchRequest);
            string respuestaJson = await busquedaServicio.Buscar(searchRequestJson);
            return DeserializarArrayJson(respuestaJson);
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
    }
}
