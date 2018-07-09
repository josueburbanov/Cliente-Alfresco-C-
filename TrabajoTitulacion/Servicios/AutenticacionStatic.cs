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
        private static string ticket;

        /// <summary>
        /// Encapsulación del atributo ticket
        /// </summary>
        public static string Ticket { get => ticket; set => ticket = value; }

        /// <summary>
        /// Autenticación de usuario
        /// </summary>
        /// <param name="nombreUsuario">Nombre de usuario</param>
        /// <param name="contraseña">Contraseña de usuario</param>
        public static void Login(string nombreUsuario, string contraseña)
        {
            Autenticacion servicioAutenticacion = new Autenticacion();
            string respuestaJson = servicioAutenticacion.Login(nombreUsuario, contraseña);
            dynamic respuestaDeserializada = JsonConvert.DeserializeObject(respuestaJson);
            //Se obtiene el ticket generado a partir de la respuesta
            ticket = respuestaDeserializada.entry.id;
        }

        public static void Logout()
        {
            Autenticacion auth = new Autenticacion();
            auth.Logout();
            ticket = "";
        }
    }
}
