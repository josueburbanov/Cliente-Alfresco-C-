using Newtonsoft.Json;
using RestSharp;
using System;
using System.Net;
using System.Threading.Tasks;
using TrabajoTitulacion.Modelos.Utils;

namespace TrabajoTitulacion.Servicios.CMM.ModelosPersonalizados
{
    class ModelosPersonalizadosServicio : IModelosPersonalizados
    {
        string URL_BASE = "/alfresco/api/-default-/private/alfresco/versions/1";
        RestClient cliente;

        public ModelosPersonalizadosServicio()
        {
            URL_BASE = Properties.Settings.Default.URL_SERVIDOR + URL_BASE;
            cliente = new RestClient(URL_BASE);
        }

        public async Task<string> ActivarModelo(string nombreModelo)
        {
            var solicitud = new RestRequest(Method.PUT);
            solicitud.Resource = "cmm/" + nombreModelo;
            solicitud.AddQueryParameter("select", "status");
            solicitud.AddParameter("application/octect-stream", JsonConvert.SerializeObject(new { status = "ACTIVE" }), ParameterType.RequestBody);
            solicitud.AddQueryParameter("alf_ticket", AutenticacionStatic.Ticket);
            IRestResponse respuesta = await cliente.ExecuteTaskAsync(solicitud);
            var contenidoRespuesta = respuesta.Content;
            HttpStatusCode statusCode = respuesta.StatusCode;
            int numericStatusCode = (int)statusCode;
            ValidacionRespuesta(respuesta, numericStatusCode);
            return contenidoRespuesta;
        }
        public async Task<string> DesactivarModelo(string nombreModelo)
        {
            var solicitud = new RestRequest(Method.PUT);
            solicitud.Resource = "cmm/" + nombreModelo;
            solicitud.AddQueryParameter("select", "status");
            solicitud.AddParameter("application/octect-stream", JsonConvert.SerializeObject(new { status = "DRAFT" }), ParameterType.RequestBody);
            solicitud.AddQueryParameter("alf_ticket", AutenticacionStatic.Ticket);
            IRestResponse respuesta = await cliente.ExecuteTaskAsync(solicitud);
            HttpStatusCode statusCode = respuesta.StatusCode;
            int numericStatusCode = (int)statusCode;
            var contenidoRespuesta = respuesta.Content;
            ValidacionRespuesta(respuesta, numericStatusCode);
            return contenidoRespuesta;
        }
        public async Task<string> ActualizarModelo(string nombreModelo, string modelo)
        {
            var solicitud = new RestRequest(Method.PUT);
            solicitud.Resource = "cmm/" + nombreModelo;
            solicitud.AddParameter("application/json", modelo, ParameterType.RequestBody);
            solicitud.AddQueryParameter("alf_ticket", AutenticacionStatic.Ticket);
            IRestResponse respuesta = await cliente.ExecuteTaskAsync(solicitud);
            var contenidoRespuesta = respuesta.Content;
            HttpStatusCode statusCode = respuesta.StatusCode;
            int numericStatusCode = (int)statusCode;
            ValidacionRespuesta(respuesta, numericStatusCode);
            return contenidoRespuesta;
        }

        private static void ValidacionRespuesta(IRestResponse respuesta, int numericStatusCode)
        {
            if (numericStatusCode == 404)
            {
                throw new ModelException(404);
            }
            else if (numericStatusCode == 409)
            {
                throw new ModelException(409);
            }
            else if (!respuesta.IsSuccessful)
            {
                throw new ModelException("Error desconocido");
            }
        }

        public async Task<string> CrearModelo(string modelo)
        {
            var solicitud = new RestRequest(Method.POST);
            solicitud.Resource = "cmm/";
            solicitud.AddParameter("application/json", modelo, ParameterType.RequestBody);
            solicitud.AddQueryParameter("alf_ticket", AutenticacionStatic.Ticket);
            IRestResponse respuesta = await cliente.ExecuteTaskAsync(solicitud);
            var contenidoRespuesta = respuesta.Content;
            HttpStatusCode statusCode = respuesta.StatusCode;
            int numericStatusCode = (int)statusCode;
            ValidacionRespuesta(respuesta, numericStatusCode);
            return contenidoRespuesta;
        }


        public async Task<string> ObtenerModelo(string nombreModelo)
        {
            var solicitud = new RestRequest(Method.GET);
            solicitud.Resource = "cmm/" + nombreModelo;
            solicitud.AddQueryParameter("alf_ticket", AutenticacionStatic.Ticket);
            IRestResponse respuesta = await cliente.ExecuteTaskAsync(solicitud);
            var contenidoRespuesta = respuesta.Content;
            HttpStatusCode statusCode = respuesta.StatusCode;
            int numericStatusCode = (int)statusCode;
            ValidacionRespuesta(respuesta, numericStatusCode);
            return contenidoRespuesta;
        }

        public async Task<string> ObtenerModelos()
        {
            var solicitud = new RestRequest(Method.GET);
            solicitud.Resource = "cmm";
            solicitud.AddQueryParameter("alf_ticket", AutenticacionStatic.Ticket);
            IRestResponse respuesta = await cliente.ExecuteTaskAsync(solicitud);
            var contenidoRespuesta = respuesta.Content;
            HttpStatusCode statusCode = respuesta.StatusCode;
            int numericStatusCode = (int)statusCode;
            ValidacionRespuesta(respuesta, numericStatusCode);
            return contenidoRespuesta;
        }

        public async Task<string> EliminarModelo(string nombreModelo)
        {
            var solicitud = new RestRequest(Method.DELETE);
            solicitud.Resource = "cmm/" + nombreModelo;
            solicitud.AddQueryParameter("alf_ticket", AutenticacionStatic.Ticket);
            IRestResponse respuesta = await cliente.ExecuteTaskAsync(solicitud);
            var contenidoRespuesta = respuesta.Content;
            HttpStatusCode statusCode = respuesta.StatusCode;
            int numericStatusCode = (int)statusCode;
            ValidacionRespuesta(respuesta, numericStatusCode);
            return contenidoRespuesta;
        }
    }
}
