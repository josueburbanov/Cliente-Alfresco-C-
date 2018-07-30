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

        /// <summary>
        /// Autenticación de usuario
        /// </summary>
        /// <param name="nombreUsuario">Nombre de usuario</param>
        /// <param name="contraseña">Contraseña de usuario</param>
        public async static Task Login(string nombreUsuario, string contraseña)
        {
            Autenticacion servicioAutenticacion = new Autenticacion();
            string respuestaJson = await servicioAutenticacion.Login(nombreUsuario, contraseña);
            dynamic respuestaDeserializada = JsonConvert.DeserializeObject(respuestaJson);
            //Se obtiene el ticket generado a partir de la respuesta
            Ticket = respuestaDeserializada.entry.id;
        }

        public async static void Logout(string userId)
        {
            Autenticacion auth = new Autenticacion();
            await auth.Logout(userId);
            Ticket = "";
        }
    }
}




