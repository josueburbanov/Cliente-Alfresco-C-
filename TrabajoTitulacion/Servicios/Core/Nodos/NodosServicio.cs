using RestSharp;
using System;
using RestSharp.Extensions;
using System.Threading.Tasks;
using TrabajoTitulacion.Modelos;

namespace TrabajoTitulacion.Servicios.Core.Nodos
{
    class NodosServicio : INodosServicio
    {
        const string URL_BASE = "http://127.0.0.1:8090/alfresco/api/-default-/public/alfresco/versions/1";
        RestClient cliente = new RestClient(URL_BASE);

        public async Task<string> ActualizarNodo(string idNodo, string nodoActualizar)
        {
            var solicitud = new RestRequest(Method.PUT);
            solicitud.Resource = "nodes/" + idNodo;
            solicitud.AddParameter("application/json", nodoActualizar, ParameterType.RequestBody);
            solicitud.AddQueryParameter("alf_ticket", AutenticacionStatic.Ticket);
            IRestResponse respuesta = await cliente.ExecuteTaskAsync(solicitud);
            var contenidoRespuesta = respuesta.Content;
            if (!respuesta.IsSuccessful) throw new UnauthorizedAccessException();
            return contenidoRespuesta;
        }

        public void ObtenerContenido(string idNodo, string path)
        {
            var solicitud = new RestRequest(Method.GET);
            solicitud.Resource = "nodes/" + idNodo + "/content";
            solicitud.AddQueryParameter("alf_ticket", AutenticacionStatic.Ticket);
            (cliente.DownloadData(solicitud)).SaveAs(path);
        }

        public async Task<string> ObtenerListaNodosHijos(string nodoPadre)
        {
            var solicitud = new RestRequest(Method.GET);
            solicitud.Resource = "nodes/"+nodoPadre+"/children";
            solicitud.AddQueryParameter("alf_ticket", AutenticacionStatic.Ticket);
            IRestResponse respuesta = await cliente.ExecuteTaskAsync(solicitud);
            var contenidoRespuesta = respuesta.Content;
            if (!respuesta.IsSuccessful) throw new UnauthorizedAccessException();
            return contenidoRespuesta;
        }

        public async Task<string> ObtenerNodo(string idNodo)
        {
            var solicitud = new RestRequest(Method.GET);
            solicitud.Resource = "nodes/" + idNodo;
            solicitud.AddQueryParameter("alf_ticket", AutenticacionStatic.Ticket);
            IRestResponse respuesta = await cliente.ExecuteTaskAsync(solicitud);
            var contenidoRespuesta = respuesta.Content;
            if (!respuesta.IsSuccessful) throw new UnauthorizedAccessException();
            return contenidoRespuesta;
        }
    }
}
