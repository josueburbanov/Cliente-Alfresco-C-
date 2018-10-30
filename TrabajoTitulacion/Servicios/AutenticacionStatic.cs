using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabajoTitulacion.Servicios
{
    class AutenticacionStatic
    {

        /// <summary>
        /// Encapsulación del atributo ticket
        /// </summary>
        public static string Ticket { get; set; }
        private static AutenticacionServicio servicioAuth = new AutenticacionServicio();

        /// <summary>
        /// Autenticación de usuario
        /// </summary>
        /// <param name="nombreUsuario">Nombre de usuario</param>
        /// <param name="contraseña">Contraseña de usuario</param>
        public async static Task<string> Login(string nombreUsuario, string contrasena)
        {            
            string respuestaJson = await servicioAuth.Login(nombreUsuario, contrasena);
            dynamic respuestaDeserializada = JsonConvert.DeserializeObject(respuestaJson);
            //Se obtiene el ticket generado a partir de la respuesta
            Ticket = respuestaDeserializada.entry.id;
            return respuestaJson;
        }

        public async static Task<string> Logout(string userId)
        {
            Ticket = "";
            return await servicioAuth.Logout(userId);
            
        }
    }
}




