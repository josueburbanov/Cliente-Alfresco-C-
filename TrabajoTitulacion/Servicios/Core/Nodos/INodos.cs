using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabajoTitulacion.Servicios.Core.Nodos
{
    interface INodos
    {
        string ObtenerListaNodosHijos(string recurso);
        string ObtenerNodo(string idNodo);
        void ObtenerContenido(string idNodo, string path);
    }
}
