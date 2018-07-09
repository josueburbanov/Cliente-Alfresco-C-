using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabajoTitulacion.Servicios.Core.Personas
{
    class Personas : IPersonas
    {
        const string URL_BASE = "http://127.0.0.1:8090/alfresco/api/-default-/public/alfresco/versions/1";
        RestClient cliente = new RestClient(URL_BASE);
        /// <summary>
        /// Obtiene una persona
        /// </summary>
        /// <param name="idPersona">Nombre de usuario</param>
        /// <returns>Persona como JSON</returns>
        public string ObtenerPersona(string idPersona)
        {            
            var solicitud = new RestRequest(Method.GET);
            solicitud.Resource = "people/"+idPersona;
            solicitud.AddQueryParameter("alf_ticket", AutenticacionStatic.Ticket);
            IRestResponse respuesta = cliente.Execute(solicitud);
            var contenidoRespuesta = respuesta.Content;
            if (!respuesta.IsSuccessful) throw new UnauthorizedAccessException();
            return contenidoRespuesta;            
        }
    }
}
