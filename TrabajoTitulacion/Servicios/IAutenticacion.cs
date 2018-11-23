using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabajoTitulacion.Servicios
{
    interface IAutenticacion
    {
        Task<string> IniciarSesion(string idUsuario, string contrasena);
        Task<string> CerrarSesion(string idUsuario);
    }
}

