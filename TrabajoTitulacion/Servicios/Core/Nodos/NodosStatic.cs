using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrabajoTitulacion.Modelos;

namespace TrabajoTitulacion.Servicios.Core.Nodos
{
    class NodosStatic
    {
        private static List<Node> nodosDeRoot = new List<Node>();        

        public static void ObtenerContenido(string idNodo, string path)
        {
            Nodos servicioNodos = new Nodos();
            servicioNodos.ObtenerContenido(idNodo, path);
        }

        public async static Task<Node> ObtenerNodo(string idNodo)
        {
            Nodos servicioNodos = new Nodos();
            string respuestaJson = await servicioNodos.ObtenerNodo(idNodo);
            //Se deserializa y luego serializa para obtener una lista de nodos (Elimina metadatos de descarga)
            dynamic respuestaDeserializada = JsonConvert.DeserializeObject(respuestaJson);
            string nodoJson = JsonConvert.SerializeObject(respuestaDeserializada.entry);            
            return JsonConvert.DeserializeObject<Node>(nodoJson);
        }

        public async static Task<List<Node>> ObtenerListaNodosHijos(string nodoPadreId)
        {
            List<Node> nodosHijos = new List<Node>();
            Nodos servicioNodos = new Nodos();
            string respuestaJson = await servicioNodos.ObtenerListaNodosHijos(nodoPadreId);
            
            //Se deserializa y luego serializa para obtener una lista de nodos (Elimina metadatos de descarga)
            dynamic respuestaDeserializada = JsonConvert.DeserializeObject(respuestaJson);
            string nodosJson = JsonConvert.SerializeObject(respuestaDeserializada.list.entries);
            dynamic nodosNoMapeados = JsonConvert.DeserializeObject(nodosJson);

            //Se itera para mapear cada nodo de la lista de nodos y añadirlos como hijos de cada nodo
            foreach (var nodo in nodosNoMapeados)
            {
                string nodoLimpioJson = JsonConvert.SerializeObject(nodo.entry);
                Node nodoLimpio = JsonConvert.DeserializeObject<Node>(nodoLimpioJson);
                nodosHijos.Add(nodoLimpio);
            }
            return nodosHijos;
        }

        /// <summary>
        /// Pobla los nodos hijos de una lista de nodos padres de un mismo nivel
        /// </summary>
        /// <param name="nodosPadres">Nodos padres de un mismo nivel</param>
        public async static Task PoblarNodosHijos(List<Node> nodosPadres)
        {
            foreach (var nodoPadre in nodosPadres)
            {
                nodoPadre.NodosHijos = await ObtenerListaNodosHijos(nodoPadre.Id);

                //Si el nodo tiene hijos
                if (nodoPadre.NodosHijos.Count()!=0 && nodoPadre.IsFolder)
                {
                    await PoblarNodosHijos(nodoPadre.NodosHijos);
                }
            }
        }
    }
}
