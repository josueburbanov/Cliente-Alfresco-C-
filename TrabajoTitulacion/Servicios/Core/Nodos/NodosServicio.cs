using RestSharp;
using System;
using RestSharp.Extensions;
using System.Threading.Tasks;
using TrabajoTitulacion.Modelos.CoreAPI;

namespace TrabajoTitulacion.Servicios.Core.Nodos
{
    class NodosServicio : INodos
    {
        private string URL_BASE;
        private RestClient cliente;

        public NodosServicio()
        {
            URL_BASE = Properties.Settings.Default.URL_SERVIDOR + "alfresco/api/-default-/public/alfresco/versions/1";
            cliente = new RestClient(URL_BASE);
        }

        /// <summary>
        /// Actualiza el contenido de un Nodo
        /// </summary>
        /// <param name="idNodo">Id del nodo</param>
        /// <param name="majorVersion">True inicia una versión, false es mantiene el subversionamiento anterior</param>
        /// <param name="comment">Comentario de la nueva versión</param>
        /// <param name="name">Nuevo nombre del contenido</param>
        /// <param name="include"></param>
        /// <param name="fields"></param>
        /// <param name="contentBodyUpdate">Contenido del nodo a actualizar</param>
        /// <returns></returns>
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

        /// <summary>
        /// Actualiza un nodo
        /// </summary>
        /// <param name="idNodo">Id del nodo</param>
        /// <param name="nodoActualizar">Nodo a actualizar en formato JSON</param>
        /// <returns></returns>
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

        /// <summary>
        /// Crea un nodo
        /// </summary>
        /// <param name="idNodoPadre">Id del nodo padre</param>
        /// <param name="nodoCreate">nodo JSON a crear</param>
        /// <returns></returns>
        public async Task<string> CrearNodo(string idNodoPadre, string nodoCreate)
        {
            var solicitud = new RestRequest(Method.POST);
            solicitud.Resource = "nodes/" + idNodoPadre + "/children";
            solicitud.AddParameter("application/json", nodoCreate, ParameterType.RequestBody);
            solicitud.AddQueryParameter("alf_ticket", AutenticacionStatic.Ticket);
            IRestResponse respuesta = await cliente.ExecuteTaskAsync(solicitud);
            var contenidoRespuesta = respuesta.Content;
            if (!respuesta.IsSuccessful) throw new UnauthorizedAccessException();            
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
            solicitud.AddParameter("include", "path");
            solicitud.AddQueryParameter("alf_ticket", AutenticacionStatic.Ticket);
            var respuesta = await cliente.ExecuteTaskAsync(solicitud);
            var contenidoRespuesta = respuesta.Content;
            if (!respuesta.IsSuccessful) return null;
            return contenidoRespuesta;
        }
    }
}

