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

        public async Task<string> ActualizarContenido(string idNodo, bool majorVersion, string comment, string name, string[] include, string[] fields, byte[] contentBodyUpdate)
        {
            var solicitud = new RestRequest(Method.PUT);
            solicitud.Resource = "nodes/" + idNodo +"/content";
            solicitud.AddParameter("application/octect-stream", contentBodyUpdate, ParameterType.RequestBody);
            solicitud.AddQueryParameter("majorVersion", majorVersion.ToString());
            if(!(comment is null))solicitud.AddQueryParameter("comment", comment);
            if (!(name is null)) solicitud.AddQueryParameter("name", name);
            if (!(include is null)) solicitud.AddQueryParameter("include", String.Join(",", include));
            if (!(fields is null)) solicitud.AddQueryParameter("fields", String.Join(",", fields));
            solicitud.AddQueryParameter("alf_ticket", AutenticacionStatic.Ticket);
            IRestResponse respuesta = await cliente.ExecuteTaskAsync(solicitud);
            var contenidoRespuesta = respuesta.Content;
            if (!respuesta.IsSuccessful) throw new UnauthorizedAccessException();
            return contenidoRespuesta;
        }

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

        public async Task<string> CrearNodo(string idNodoPadre, string nodoCreate)
        {
            var solicitud = new RestRequest(Method.POST);
            solicitud.Resource = "nodes/" + idNodoPadre + "/children";
            solicitud.AddParameter("application/json", nodoCreate, ParameterType.RequestBody);
            solicitud.AddQueryParameter("alf_ticket", AutenticacionStatic.Ticket);
            IRestResponse respuesta = await cliente.ExecuteTaskAsync(solicitud);
            var contenidoRespuesta = respuesta.Content;
            //if (!respuesta.IsSuccessful) throw new UnauthorizedAccessException();
            Console.WriteLine(contenidoRespuesta);
            return contenidoRespuesta;
        }

        public async Task<string> EliminarNodo(string idNodo)
        {
            var solicitud = new RestRequest(Method.DELETE);
            solicitud.Resource = "nodes/" + idNodo;
            solicitud.AddQueryParameter("alf_ticket", AutenticacionStatic.Ticket);
            IRestResponse respuesta = await cliente.ExecuteTaskAsync(solicitud);
            var contenidoRespuesta = respuesta.Content;
            if (!respuesta.IsSuccessful) throw new UnauthorizedAccessException();
            return contenidoRespuesta;
        }

        public async Task<byte[]> ObtenerContenido(string idNodo)
        {
            var solicitud = new RestRequest(Method.GET);
            solicitud.Resource = "nodes/" + idNodo + "/content";
            solicitud.AddQueryParameter("alf_ticket", AutenticacionStatic.Ticket);
            IRestResponse respuesta = await cliente.ExecuteTaskAsync(solicitud);
            var contenidoRespuesta = respuesta.RawBytes;
            if (!respuesta.IsSuccessful) throw new UnauthorizedAccessException();
            return contenidoRespuesta;
        }

        public async Task<string> ObtenerListaNodosHijos(string nodoPadre)
        {
            var solicitud = new RestRequest(Method.GET);
            solicitud.Resource = "nodes/" + nodoPadre + "/children";
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
