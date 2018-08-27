using Newtonsoft.Json;
using RestSharp;
using System;
using System.Threading.Tasks;

namespace TrabajoTitulacion.Servicios.CMM.ModelosPersonalizados
{
    class ModelosPersonalizadosServicio : IModeloPersonalizadoServicio
    {
        const string URL_BASE = "http://127.0.0.1:8090/alfresco/api/-default-/private/alfresco/versions/1";
        RestClient cliente = new RestClient(URL_BASE);

        public async Task<string> ActivarModeloPersonalizado(string nombreModelo)
        {
            var solicitud = new RestRequest(Method.PUT);
            solicitud.Resource = "cmm/" + nombreModelo;
            solicitud.AddQueryParameter("select", "status");
            solicitud.AddParameter("application/octect-stream", JsonConvert.SerializeObject(new {status="ACTIVE"}), ParameterType.RequestBody);
            solicitud.AddQueryParameter("alf_ticket", AutenticacionStatic.Ticket);
            IRestResponse respuesta = await cliente.ExecuteTaskAsync(solicitud);
            var contenidoRespuesta = respuesta.Content;
            if (!respuesta.IsSuccessful) throw new UnauthorizedAccessException();
            return contenidoRespuesta;
        }
        public async Task<string> DesactivarModeloPersonalizado(string nombreModelo)
        {
            var solicitud = new RestRequest(Method.PUT);
            solicitud.Resource = "cmm/" + nombreModelo;
            solicitud.AddQueryParameter("select", "status");
            solicitud.AddParameter("application/octect-stream", JsonConvert.SerializeObject(new { status = "DRAFT" }), ParameterType.RequestBody);
            solicitud.AddQueryParameter("alf_ticket", AutenticacionStatic.Ticket);
            IRestResponse respuesta = await cliente.ExecuteTaskAsync(solicitud);
            var contenidoRespuesta = respuesta.Content;
            if (!respuesta.IsSuccessful) throw new UnauthorizedAccessException();
            return contenidoRespuesta;
        }
        public async Task<string> ActualizarModeloPersonalizado(string nombreModelo, string modelo)
        {
            var solicitud = new RestRequest(Method.PUT);
            solicitud.Resource = "cmm/"+nombreModelo;
            solicitud.AddParameter("application/json", modelo, ParameterType.RequestBody);
            solicitud.AddQueryParameter("alf_ticket", AutenticacionStatic.Ticket);
            IRestResponse respuesta = await cliente.ExecuteTaskAsync(solicitud);
            var contenidoRespuesta = respuesta.Content;
            if (!respuesta.IsSuccessful) throw new UnauthorizedAccessException();
            return contenidoRespuesta;
        }
        public async Task<string> CrearModeloPersonalizado(string modelo)
        {
            var solicitud = new RestRequest(Method.POST);
            solicitud.Resource = "cmm/";
            solicitud.AddParameter("application/json", modelo, ParameterType.RequestBody);
            solicitud.AddQueryParameter("alf_ticket", AutenticacionStatic.Ticket);
            IRestResponse respuesta = await cliente.ExecuteTaskAsync(solicitud);
            var contenidoRespuesta = respuesta.Content;
            if (!respuesta.IsSuccessful) throw new UnauthorizedAccessException();
            return contenidoRespuesta;
        }


        public async Task<string> ObtenerModeloPersonalizado(string nombreModelo)
        {
            var solicitud = new RestRequest(Method.GET);
            solicitud.Resource = "cmm/"+nombreModelo;
            solicitud.AddQueryParameter("alf_ticket", AutenticacionStatic.Ticket);
            IRestResponse respuesta = await cliente.ExecuteTaskAsync(solicitud);
            var contenidoRespuesta = respuesta.Content;
            if (!respuesta.IsSuccessful) throw new UnauthorizedAccessException();
            return contenidoRespuesta;
        }

        public async Task<string> ObtenerModelosPersonalizados()
        {
            var solicitud = new RestRequest(Method.GET);
            solicitud.Resource = "cmm";
            solicitud.AddQueryParameter("alf_ticket", AutenticacionStatic.Ticket);
            IRestResponse respuesta = await cliente.ExecuteTaskAsync(solicitud);
            var contenidoRespuesta = respuesta.Content;
            if (!respuesta.IsSuccessful) throw new UnauthorizedAccessException();
            return contenidoRespuesta;
        }

        public async Task<string> EliminarModeloPersonalizado(string nombreModelo)
        {
            var solicitud = new RestRequest(Method.DELETE);
            solicitud.Resource = "cmm/"+nombreModelo;
            solicitud.AddQueryParameter("alf_ticket", AutenticacionStatic.Ticket);
            IRestResponse respuesta = await cliente.ExecuteTaskAsync(solicitud);
            var contenidoRespuesta = respuesta.Content;
            if (!respuesta.IsSuccessful) throw new UnauthorizedAccessException();
            return contenidoRespuesta;
        }
    }
}
