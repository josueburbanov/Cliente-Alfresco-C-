using RestSharp;
using System;
using System.Net;
using System.Threading.Tasks;
using TrabajoTitulacion.Modelos.Utils;

namespace TrabajoTitulacion.Servicios.CMM.TiposPersonalizados
{
    class TiposPersonalizadosServicio : ITiposPersonalizados
    {
        string URL_BASE = "/alfresco/api/-default-/private/alfresco/versions/1";
        RestClient cliente;

        public TiposPersonalizadosServicio()
        {
            URL_BASE = Properties.Settings.Default.URL_SERVIDOR + URL_BASE;
            cliente = new RestClient(URL_BASE);
        }

        public async Task<string> ActualizarTipo(string nombreModelo, string nombreTipo, string tipo)
        {
            var solicitud = new RestRequest(Method.PUT);
            solicitud.Resource = "cmm/" + nombreModelo + "/types/"+nombreTipo;
            solicitud.AddParameter("application/json", tipo, ParameterType.RequestBody);
            solicitud.AddQueryParameter("alf_ticket", AutenticacionStatic.Ticket);
            IRestResponse respuesta = await cliente.ExecuteTaskAsync(solicitud);
            var contenidoRespuesta = respuesta.Content;
            HttpStatusCode statusCode = respuesta.StatusCode;
            int numericStatusCode = (int)statusCode;
            if (numericStatusCode == 403)
            {
                throw new TypeException("Permiso denegado");
            }
            return contenidoRespuesta;
        }

        public async Task<string> CrearTipo(string nombreModelo, string tipo)
        {
            var solicitud = new RestRequest(Method.POST);
            solicitud.Resource = "cmm/" + nombreModelo + "/types";
            solicitud.AddParameter("application/json", tipo, ParameterType.RequestBody);
            solicitud.AddQueryParameter("alf_ticket", AutenticacionStatic.Ticket);
            IRestResponse respuesta = await cliente.ExecuteTaskAsync(solicitud);
            var contenidoRespuesta = respuesta.Content;
            HttpStatusCode statusCode = respuesta.StatusCode;
            int numericStatusCode = (int)statusCode;
            if (numericStatusCode == 403)
            {
                throw new TypeException("Permiso denegado");
            }
            return contenidoRespuesta;
        }

        public async Task<string> EliminarTipo(string nombreModelo, string nombreTipo)
        {
            var solicitud = new RestRequest(Method.DELETE);
            solicitud.Resource = "cmm/" + nombreModelo + "/types/"+nombreTipo;
            solicitud.AddQueryParameter("alf_ticket", AutenticacionStatic.Ticket);
            IRestResponse respuesta = await cliente.ExecuteTaskAsync(solicitud);
            var contenidoRespuesta = respuesta.Content;
            HttpStatusCode statusCode = respuesta.StatusCode;
            int numericStatusCode = (int)statusCode;
            if (numericStatusCode == 403)
            {
                throw new TypeException("Permiso denegado");
            }
            return contenidoRespuesta;
        }

        public async Task<string> ObtenerTipo(string nombreModelo,string nombreTipo)
        {
            var solicitud = new RestRequest(Method.GET);
            solicitud.Resource = "cmm/" + nombreModelo + "/types/"+nombreTipo;
            solicitud.AddQueryParameter("alf_ticket", AutenticacionStatic.Ticket);
            IRestResponse respuesta = await cliente.ExecuteTaskAsync(solicitud);
            var contenidoRespuesta = respuesta.Content;
            if (!respuesta.IsSuccessful) throw new UnauthorizedAccessException();
            return contenidoRespuesta;
        }

        public async Task<string> ObtenerTipos(string nombreModelo)
        {
            var solicitud = new RestRequest(Method.GET);
            solicitud.Resource = "cmm/"+nombreModelo+"/types";
            solicitud.AddQueryParameter("alf_ticket", AutenticacionStatic.Ticket);
            IRestResponse respuesta = await cliente.ExecuteTaskAsync(solicitud);
            var contenidoRespuesta = respuesta.Content;
            if (!respuesta.IsSuccessful) throw new UnauthorizedAccessException();
            return contenidoRespuesta;
        }
    }
}
