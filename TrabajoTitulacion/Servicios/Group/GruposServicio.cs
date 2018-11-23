using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabajoTitulacion.Servicios.Group
{
    class GruposServicio : IGrupos
    {
        private string URL_BASE;
        private RestClient cliente;

        public GruposServicio()
        {
            URL_BASE = Properties.Settings.Default.URL_SERVIDOR + "alfresco/api/-default-/public/alfresco/versions/1";
            cliente = new RestClient(URL_BASE);
        }
        public async Task<string> AñadirMiembrosGrupo(string idGrupo, string groupMembershipBodyCreate)
        {
            var solicitud = new RestRequest(Method.POST);
            solicitud.Resource = "groups/" + idGrupo + "/members";
            solicitud.AddParameter("application/json", groupMembershipBodyCreate, ParameterType.RequestBody);
            solicitud.AddQueryParameter("alf_ticket", AutenticacionStatic.Ticket);
            IRestResponse respuesta = await cliente.ExecuteTaskAsync(solicitud);
            var contenidoRespuesta = respuesta.Content;
            if (!respuesta.IsSuccessful) throw new UnauthorizedAccessException();
            return contenidoRespuesta;
        }

        public async Task<string> ObtenerMiembrosGrupo(string idGrupo)
        {
            var solicitud = new RestRequest(Method.GET);
            solicitud.Resource = "groups/" + idGrupo + "/members";
            solicitud.AddQueryParameter("alf_ticket", AutenticacionStatic.Ticket);
            IRestResponse respuesta = await cliente.ExecuteTaskAsync(solicitud);
            var contenidoRespuesta = respuesta.Content;
            if (!respuesta.IsSuccessful) throw new UnauthorizedAccessException();
            return contenidoRespuesta;
        }
    }
}
