﻿using TrabajoTitulacion.Servicios.Core.Personas;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Threading;

namespace TrabajoTitulacion.Servicios
{
    class AutenticacionServicio : IAutenticacion
    {
        string URL_BASE = "/alfresco/api/-default-/public/authentication/versions/1";
        RestClient cliente;

        public AutenticacionServicio()
        {
            URL_BASE = Properties.Settings.Default.URL_SERVIDOR + URL_BASE;
            cliente = new RestClient(URL_BASE);
        }
        /// <summary>
        /// Permite obtener un token de autenticación para acceder a Alfresco
        /// </summary>
        /// <param name="userId">Nombre de usuario</param>
        /// <param name="password">Contraseña</param>
        /// <returns>ticket de autenticacion</returns>
        public async Task<string> IniciarSesion(string idUsuario, string contrasena)
        {
            var solicitud = new RestRequest(Method.POST);
            solicitud.Resource = "tickets/";
            solicitud.RequestFormat = DataFormat.Json;
            solicitud.AddBody(new { userId = idUsuario, password = contrasena });
            var respuesta = await cliente.ExecuteTaskAsync(solicitud);
            var contenidoRespuesta = respuesta.Content;
            if (!respuesta.IsSuccessful) throw new UnauthorizedAccessException();
            return contenidoRespuesta;
        }

        /// <summary>
        /// Elimina un token autenticado
        /// </summary>
        /// <param name="userId">Nombre de usuario</param>
        /// <returns></returns>
        public async Task<string> CerrarSesion(string userId)
        {
            var solicitud = new RestRequest(Method.DELETE);
            solicitud.Resource = "tickets/" + await PersonasStatic.ObtenerPersona(userId);
            solicitud.AddQueryParameter("alf_ticket", AutenticacionStatic.Ticket);
            IRestResponse respuesta = cliente.Execute(solicitud);
            var contenidoRespuesta = respuesta.Content;
            return contenidoRespuesta;
        }
    }
}