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

        public static Node ObtenerNodo(string idNodo)
        {
            Nodos servicioNodos = new Nodos();
            string respuestaJson = servicioNodos.ObtenerNodo(idNodo);
            //Se deserializa y luego serializa para obtener una lista de nodos (Elimina metadatos de descarga)
            dynamic respuestaDeserializada = JsonConvert.DeserializeObject(respuestaJson);
            string nodoJson = JsonConvert.SerializeObject(respuestaDeserializada.entry);
            return JsonConvert.DeserializeObject<Node>(nodoJson);
        }

        public static List<Node> ObtenerListaNodosHijos(string nodoPadreId)
        {
            List<Node> nodosHijos = new List<Node>();
            Nodos servicioNodos = new Nodos();
            string respuestaJson = servicioNodos.ObtenerListaNodosHijos(nodoPadreId);
            
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
        public static void PoblarNodosHijos(List<Node> nodosPadres)
        {
            foreach (var nodoPadre in nodosPadres)
            {
                nodoPadre.NodosHijos = ObtenerListaNodosHijos(nodoPadre.Id);

                //Si el nodo tiene hijos
                if (nodoPadre.NodosHijos.Count()!=0 && nodoPadre.IsFolder)
                {
                    PoblarNodosHijos(nodoPadre.NodosHijos);
                }
            }
        }

        //public static Node BuscarPadre(List<Node> nodosBuscar,Node nodoHijo)
        //{
        //    Node nodoPadre = null;
        //    foreach(var nodo in nodosBuscar)
        //    {
        //        //Si el padre está en la lista nodosBuscar entonces lo devuelve
        //        if (nodo.Id == nodoHijo.ParentId)
        //        {
        //            nodoPadre = nodo;
        //            break;
        //        }
        //        else
        //        {
        //            //Si el padre no está en la lista nodosBuscar entonces busca en los hijos de de nodosBuscar
        //            if(nodo.NodosHijos != null)
        //            {
        //                nodoPadre = BuscarPadre(nodo.NodosHijos, nodoHijo);
        //                if(nodoPadre != null)
        //                {
        //                    break;
        //                }
        //            }
        //        }
        //    }            
        //    Node nodoRoot = ObtenerNodo("-root-");
        //    if (nodoRoot.Id.Equals(nodoHijo.ParentId))
        //    {
        //        nodoPadre = nodoRoot;
        //    }

        //    return nodoPadre;
        //}

    }
}
