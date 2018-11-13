using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabajoTitulacion.Servicios.Search
{
    interface IBusqueda
    {
        Task<string> Buscar(string searchRequest);
    }
}
