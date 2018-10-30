using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabajoTitulacion.Servicios.Core.Personas
{
    class PersonasServicio : IPersonas
    {
        string URL_BASE;
        RestClient cliente;

        public PersonasServicio()
        {
            URL_BASE = Properties.Settings.Default.URL_SERVIDOR + "alfresco/api/-default-/public/alfresco/versions/1";
            RestClient cliente = new RestClient(URL_BASE);
        }

        /// <summary>
        /// Obtiene una persona
        /// </summary>
        /// <param name="idPersona">Nombre de usuario</param>
        /// <returns>Persona como JSON</returns>
        public async Task<string> ObtenerPersona(string idUsuario)
        {
            var solicitud = new RestRequest(Method.GET);
            solicitud.Resource = "people/" + idUsuario;
            solicitud.AddQueryParameter("alf_ticket", AutenticacionStatic.Ticket);
            IRestResponse respuesta = await cliente.ExecuteTaskAsync(solicitud);
            var contenidoRespuesta = respuesta.Content;
            if (!respuesta.IsSuccessful) throw new UnauthorizedAccessException();
            return contenidoRespuesta;
        }
    }
}