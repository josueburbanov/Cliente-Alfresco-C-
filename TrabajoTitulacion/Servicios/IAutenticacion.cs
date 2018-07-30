using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabajoTitulacion.Servicios
{
    interface IAutenticacion
    {
        Task<string> Login(string userId, string password);
        Task Logout(string userId);
    }
}
