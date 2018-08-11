using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrabajoTitulacion.Modelos.CMM;

namespace TrabajoTitulacion.Modelos.Core
{
    public class Nodo : Node
    {
        public List<Aspect> Aspectos { get; set; }
        
        /// <summary>
        /// Lista de nodos hijos
        /// </summary>
        public List<Nodo> NodosHijos { get; set; }
        public Type1 TipoNodo { get;  set; }
    }
}
