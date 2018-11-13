using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabajoTitulacion.Servicios
{
    interface IAutenticacion
    {
        Task<string> Login(string idUsuario, string contrasena);
        Task<string> Logout(string idUsuario);
    }
}

