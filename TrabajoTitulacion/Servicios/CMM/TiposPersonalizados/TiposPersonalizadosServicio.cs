using RestSharp;
using System;
using System.Threading.Tasks;

namespace TrabajoTitulacion.Servicios.CMM.TiposPersonalizados
{
    class TiposPersonalizadosServicio : ITiposPersonalizadosServicio
    {
        const string URL_BASE = "http://127.0.0.1:8090/alfresco/api/-default-/private/alfresco/versions/1";
        RestClient cliente = new RestClient(URL_BASE);

        public async Task<string> ActualizarTipoPersonalizado(string nombreModelo, string nombreTipo, string tipo)
        {
            var solicitud = new RestRequest(Method.PUT);
            solicitud.Resource = "cmm/" + nombreModelo + "/types/"+nombreTipo;
            solicitud.AddParameter("application/json", tipo, ParameterType.RequestBody);
            solicitud.AddQueryParameter("alf_ticket", AutenticacionStatic.Ticket);
            IRestResponse respuesta = await cliente.ExecuteTaskAsync(solicitud);
            var contenidoRespuesta = respuesta.Content;
            if (!respuesta.IsSuccessful) throw new UnauthorizedAccessException();
            return contenidoRespuesta;
        }

        public async Task<string> CrearTipoPersonalizado(string nombreModelo, string tipo)
        {
            var solicitud = new RestRequest(Method.POST);
            solicitud.Resource = "cmm/" + nombreModelo + "/types";
            solicitud.AddParameter("application/json", tipo, ParameterType.RequestBody);
            solicitud.AddQueryParameter("alf_ticket", AutenticacionStatic.Ticket);
            IRestResponse respuesta = await cliente.ExecuteTaskAsync(solicitud);
            var contenidoRespuesta = respuesta.Content;
            if (!respuesta.IsSuccessful) throw new UnauthorizedAccessException();
            return contenidoRespuesta;
        }

        public async Task<string> EliminarTipoPersonalizado(string nombreModelo, string nombreTipo)
        {
            var solicitud = new RestRequest(Method.DELETE);
            solicitud.Resource = "cmm/" + nombreModelo + "/types/"+nombreTipo;
            solicitud.AddQueryParameter("alf_ticket", AutenticacionStatic.Ticket);
            IRestResponse respuesta = await cliente.ExecuteTaskAsync(solicitud);
            var contenidoRespuesta = respuesta.Content;
            if (!respuesta.IsSuccessful) throw new UnauthorizedAccessException();
            return contenidoRespuesta;
        }

        public async Task<string> ObtenerTipoPersonalizado(string nombreModelo,string nombreTipo)
        {
            var solicitud = new RestRequest(Method.GET);
            solicitud.Resource = "cmm/" + nombreModelo + "/types/"+nombreTipo;
            solicitud.AddQueryParameter("alf_ticket", AutenticacionStatic.Ticket);
            IRestResponse respuesta = await cliente.ExecuteTaskAsync(solicitud);
            var contenidoRespuesta = respuesta.Content;
            if (!respuesta.IsSuccessful) throw new UnauthorizedAccessException();
            return contenidoRespuesta;
        }

        public async Task<string> ObtenerTiposPersonalizados(string nombreModelo)
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
