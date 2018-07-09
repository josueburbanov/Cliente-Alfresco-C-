using TrabajoTitulacion.Servicios.Core.Personas;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TrabajoTitulacion.Servicios
{
    class Autenticacion : IAutenticacion
    {
        const string URLBASE = "http://127.0.0.1:8090/alfresco/api/-default-/public/authentication/versions/1";
        RestClient cliente = new RestClient(URLBASE);
        /// <summary>
        /// Permite obtener un token de autenticación para acceder a Alfresco
        /// </summary>
        /// <param name="userId">Nombre de usuario</param>
        /// <param name="password">Contraseña</param>
        /// <returns>ticket de autenticacion</returns>
        public string Login(string userId, string password)
        {
            var solicitud = new RestRequest(Method.POST);
            solicitud.Resource = "tickets/";
            solicitud.RequestFormat = DataFormat.Json;
            solicitud.AddBody(new { userId = userId, password = password });
            IRestResponse respuesta = cliente.Execute(solicitud);
            var contenidoRespuesta = respuesta.Content;
            if (!respuesta.IsSuccessful) throw new UnauthorizedAccessException();
            return contenidoRespuesta;
        }

        public void Logout()
        {
            var solicitud = new RestRequest(Method.DELETE);
            solicitud.Resource = "tickets/" + PersonasStatic.PersonaActual.Id;
            IRestResponse respuesta = cliente.Execute(solicitud);
            var contenidoRespuesta = respuesta.Content;            
        }
    }
}
