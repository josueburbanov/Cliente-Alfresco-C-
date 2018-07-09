using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabajoTitulacion.Servicios
{
    interface IAutenticacion
    {
        string Login(string userId, string password);
        void Logout();
    }
}
