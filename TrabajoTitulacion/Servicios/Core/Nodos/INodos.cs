using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabajoTitulacion.Servicios.Core.Nodos
{
    interface INodos
    {
        Task<string> ObtenerListaNodosHijos(string recurso);
        Task<string> ObtenerNodo(string idNodo);
        void ObtenerContenido(string idNodo, string path);
    }
}
