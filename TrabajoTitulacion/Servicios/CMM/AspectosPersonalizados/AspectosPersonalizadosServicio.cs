using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabajoTitulacion.Servicios.CMM.AspectosPersonalizados
{
    class AspectosPersonalizadosServicio : IAspectosPersonalizados
    {
        string URL_BASE = "/alfresco/api/-default-/private/alfresco/versions/1";
        RestClient cliente;

        public AspectosPersonalizadosServicio()
        {
            URL_BASE = Properties.Settings.Default.URL_SERVIDOR + URL_BASE;
            cliente = new RestClient(URL_BASE);
        }

        public async Task<string> ObtenerAspectos(string nombreModelo)
        {
            var solicitud = new RestRequest(Method.GET);
            solicitud.Resource = "cmm/" + nombreModelo + "/aspects";
            solicitud.AddQueryParameter("alf_ticket", AutenticacionStatic.Ticket);
            IRestResponse respuesta = await cliente.ExecuteTaskAsync(solicitud);
            var contenidoRespuesta = respuesta.Content;
            if (!respuesta.IsSuccessful) throw new UnauthorizedAccessException();
            return contenidoRespuesta;
        }
        public async Task<string> ActualizarAspecto(string nombreModelo, string nombreAspecto, string aspecto)
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

        public async Task<string> CrearAspecto(string nombreModelo, string aspecto)
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

        public async Task<string> EliminarAspecto(string nombreModelo, string nombreAspecto)
        {
            var solicitud = new RestRequest(Method.DELETE);
            solicitud.Resource = "cmm/" + nombreModelo + "/aspects/" + nombreAspecto;
            solicitud.AddQueryParameter("alf_ticket", AutenticacionStatic.Ticket);
            IRestResponse respuesta = await cliente.ExecuteTaskAsync(solicitud);
            var contenidoRespuesta = respuesta.Content;
            if (!respuesta.IsSuccessful) throw new UnauthorizedAccessException();
            return contenidoRespuesta;
        }

        public async Task<string> ObtenerAspecto(string nombreModelo, string nombreAspecto)
        {
            var solicitud = new RestRequest(Method.GET);
            solicitud.Resource = "cmm/" + nombreModelo + "/aspects/" + nombreAspecto;
            solicitud.AddQueryParameter("alf_ticket", AutenticacionStatic.Ticket);
            IRestResponse respuesta = await cliente.ExecuteTaskAsync(solicitud);
            var contenidoRespuesta = respuesta.Content;
            if (!respuesta.IsSuccessful) throw new UnauthorizedAccessException();
            return contenidoRespuesta;
        }

        public async Task<string> AñadirPropiedadAspecto(string nombreModelo, string nombreAspecto, string propertiesBodyCreate)
        {
            var solicitud = new RestRequest(Method.PUT);
            solicitud.Resource = "cmm/" + nombreModelo + "/aspects/" + nombreAspecto;
            solicitud.AddParameter("application/json", propertiesBodyCreate, ParameterType.RequestBody);
            solicitud.AddQueryParameter("select", "props");
            solicitud.AddQueryParameter("alf_ticket", AutenticacionStatic.Ticket);
            IRestResponse respuesta = await cliente.ExecuteTaskAsync(solicitud);
            var contenidoRespuesta = respuesta.Content;
            if (!respuesta.IsSuccessful) throw new UnauthorizedAccessException();
            return contenidoRespuesta;
        }

        public async Task<string> ActualizarPropiedadAspecto(string nombreModelo, string nombreAspecto, string nombrePropiedad, string propiedadActualizar)
        {
            var solicitud = new RestRequest(Method.PUT);
            solicitud.Resource = "cmm/" + nombreModelo + "/aspects/" + nombreAspecto;
            solicitud.AddParameter("application/octect-stream", propiedadActualizar, ParameterType.RequestBody);
            solicitud.AddQueryParameter("select", "props");
            solicitud.AddQueryParameter("update", nombrePropiedad);
            solicitud.AddQueryParameter("alf_ticket", AutenticacionStatic.Ticket);
            IRestResponse respuesta = await cliente.ExecuteTaskAsync(solicitud);
            var contenidoRespuesta = respuesta.Content;
            if (!respuesta.IsSuccessful) throw new UnauthorizedAccessException();
            return contenidoRespuesta;
        }

        public async Task<string> EliminarPropiedadAspecto(string nombreModelo, string nombreAspecto, string nombrePropiedad)
        {
            var solicitud = new RestRequest(Method.PUT);
            solicitud.Resource = "cmm/" + nombreModelo + "/aspects/" + nombreAspecto;
            solicitud.AddParameter("application/octect-stream", JsonConvert.SerializeObject(new { name = nombreAspecto}), ParameterType.RequestBody);
            solicitud.AddQueryParameter("select", "props");
            solicitud.AddQueryParameter("delete", nombrePropiedad);
            solicitud.AddQueryParameter("alf_ticket", AutenticacionStatic.Ticket);
            IRestResponse respuesta = await cliente.ExecuteTaskAsync(solicitud);
            var contenidoRespuesta = respuesta.Content;
            if (!respuesta.IsSuccessful) throw new UnauthorizedAccessException();
            return contenidoRespuesta;
        }
    }
}
