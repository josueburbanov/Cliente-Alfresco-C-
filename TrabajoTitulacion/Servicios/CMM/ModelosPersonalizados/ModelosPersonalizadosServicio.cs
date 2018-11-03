using Newtonsoft.Json;
using RestSharp;
using System;
using System.Threading.Tasks;

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
            solicitud.AddParameter("application/octect-stream", JsonConvert.SerializeObject(new {status="ACTIVE"}), ParameterType.RequestBody);
            solicitud.AddQueryParameter("alf_ticket", AutenticacionStatic.Ticket);
            IRestResponse respuesta = await cliente.ExecuteTaskAsync(solicitud);
            var contenidoRespuesta = respuesta.Content;
            if (!respuesta.IsSuccessful) throw new UnauthorizedAccessException();
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
            var contenidoRespuesta = respuesta.Content;
            if (!respuesta.IsSuccessful) throw new UnauthorizedAccessException();
            return contenidoRespuesta;
        }
        public async Task<string> ActualizarModelo(string nombreModelo, string modelo)
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
        public async Task<string> CrearModelo(string modelo)
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


        public async Task<string> ObtenerModelo(string nombreModelo)
        {
            var solicitud = new RestRequest(Method.GET);
            solicitud.Resource = "cmm/"+nombreModelo;
            solicitud.AddQueryParameter("alf_ticket", AutenticacionStatic.Ticket);
            IRestResponse respuesta = await cliente.ExecuteTaskAsync(solicitud);
            var contenidoRespuesta = respuesta.Content;
            if (!respuesta.IsSuccessful) throw new UnauthorizedAccessException();
            return contenidoRespuesta;
        }

        public async Task<string> ObtenerModelos()
        {
            var solicitud = new RestRequest(Method.GET);
            solicitud.Resource = "cmm";
            solicitud.AddQueryParameter("alf_ticket", AutenticacionStatic.Ticket);
            IRestResponse respuesta = await cliente.ExecuteTaskAsync(solicitud);
            var contenidoRespuesta = respuesta.Content;
            if (!respuesta.IsSuccessful) throw new UnauthorizedAccessException();
            return contenidoRespuesta;
        }

        public async Task<string> EliminarModelo(string nombreModelo)
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
