using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabajoTitulacion.Servicios.CMM.AspectosPersonalizados
{
    class AspectosPersonalizadosServicio : IAspectosPersonalizadosServicio
    {
        const string URL_BASE = "http://127.0.0.1:8090/alfresco/api/-default-/private/alfresco/versions/1";
        RestClient cliente = new RestClient(URL_BASE);

        public async Task<string> ObtenerAspectosPersonalizados(string nombreModelo)
        {
            var solicitud = new RestRequest(Method.GET);
            solicitud.Resource = "cmm/" + nombreModelo + "/aspects";
            solicitud.AddQueryParameter("alf_ticket", AutenticacionStatic.Ticket);
            IRestResponse respuesta = await cliente.ExecuteTaskAsync(solicitud);
            var contenidoRespuesta = respuesta.Content;
            if (!respuesta.IsSuccessful) throw new UnauthorizedAccessException();
            return contenidoRespuesta;
        }
        public async Task<string> ActualizarAspectoPersonalizado(string nombreModelo, string nombreAspecto, string aspecto)
        {
            var solicitud = new RestRequest(Method.PUT);
            solicitud.Resource = "cmm/" + nombreModelo + "/aspects/"+nombreAspecto;
            solicitud.AddParameter("application/json", aspecto, ParameterType.RequestBody);
            solicitud.AddQueryParameter("alf_ticket", AutenticacionStatic.Ticket);
            IRestResponse respuesta = await cliente.ExecuteTaskAsync(solicitud);
            var contenidoRespuesta = respuesta.Content;
            if (!respuesta.IsSuccessful) throw new UnauthorizedAccessException();
            return contenidoRespuesta;
        }

        public async Task<string> CrearAspectoPersonalizado(string nombreModelo, string aspecto)
        {
            var solicitud = new RestRequest(Method.POST);
            solicitud.Resource = "cmm/" + nombreModelo + "/aspects";
            solicitud.AddParameter("application/json", aspecto, ParameterType.RequestBody);
            solicitud.AddQueryParameter("alf_ticket", AutenticacionStatic.Ticket);
            IRestResponse respuesta = await cliente.ExecuteTaskAsync(solicitud);
            var contenidoRespuesta = respuesta.Content;
            if (!respuesta.IsSuccessful) throw new UnauthorizedAccessException();
            return contenidoRespuesta;
        }

        public async Task<string> EliminarAspectoPersonalizado(string nombreModelo, string nombreAspecto)
        {
            var solicitud = new RestRequest(Method.DELETE);
            solicitud.Resource = "cmm/" + nombreModelo + "/aspects/" + nombreAspecto;
            solicitud.AddQueryParameter("alf_ticket", AutenticacionStatic.Ticket);
            IRestResponse respuesta = await cliente.ExecuteTaskAsync(solicitud);
            var contenidoRespuesta = respuesta.Content;
            if (!respuesta.IsSuccessful) throw new UnauthorizedAccessException();
            return contenidoRespuesta;
        }

        public async Task<string> ObtenerAspectoPersonalizado(string nombreModelo, string nombreAspecto)
        {
            var solicitud = new RestRequest(Method.GET);
            solicitud.Resource = "cmm/" + nombreModelo + "/aspects/" + nombreAspecto;
            solicitud.AddQueryParameter("alf_ticket", AutenticacionStatic.Ticket);
            IRestResponse respuesta = await cliente.ExecuteTaskAsync(solicitud);
            var contenidoRespuesta = respuesta.Content;
            if (!respuesta.IsSuccessful) throw new UnauthorizedAccessException();
            return contenidoRespuesta;
        }

        public async Task<string> AñadirPropiedadesAspecto(string nombreModelo, string nombreAspecto, string propertiesBodyUpdate)
        {
            var solicitud = new RestRequest(Method.PUT);
            solicitud.Resource = "cmm/" + nombreModelo + "/aspects/" + nombreAspecto;
            solicitud.AddParameter("application/json", propertiesBodyUpdate, ParameterType.RequestBody);
            solicitud.AddQueryParameter("select", "props");
            solicitud.AddQueryParameter("alf_ticket", AutenticacionStatic.Ticket);
            IRestResponse respuesta = await cliente.ExecuteTaskAsync(solicitud);
            var contenidoRespuesta = respuesta.Content;
            if (!respuesta.IsSuccessful) throw new UnauthorizedAccessException();
            return contenidoRespuesta;
        }
    }
}
