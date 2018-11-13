using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabajoTitulacion.Servicios.Search
{
    class BusquedaServicio : IBusqueda
    {
        string URL_BASE = "alfresco/api/-default-/public/search/versions/1";
        RestClient cliente;

        public BusquedaServicio()
        {
            URL_BASE = Properties.Settings.Default.URL_SERVIDOR + URL_BASE;
            cliente = new RestClient(URL_BASE);
        }
        public async Task<string> Buscar(string searchRequest)
        {
            var solicitud = new RestRequest(Method.POST);
            solicitud.Resource = "search";
            solicitud.AddParameter("application/json", searchRequest, ParameterType.RequestBody);
            solicitud.AddQueryParameter("alf_ticket", AutenticacionStatic.Ticket);
            var respuesta = await cliente.ExecuteTaskAsync(solicitud);
            var contenidoRespuesta = respuesta.Content;
            if (!respuesta.IsSuccessful) throw new UnauthorizedAccessException();
            return contenidoRespuesta;

        }
    }
}
