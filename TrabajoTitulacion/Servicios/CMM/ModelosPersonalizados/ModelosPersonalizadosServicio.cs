using RestSharp;
using System;
using System.Threading.Tasks;

namespace TrabajoTitulacion.Servicios.CMM.ModelosPersonalizados
{
    class ModelosPersonalizadosServicio : IModeloPersonalizadoServicio
    {
        const string URL_BASE = "http://127.0.0.1:8090/alfresco/api/-default-/private/alfresco/versions/1";
        RestClient cliente = new RestClient(URL_BASE);

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

    }
}
