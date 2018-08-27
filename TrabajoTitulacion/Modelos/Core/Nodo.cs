using Newtonsoft.Json;
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
        [JsonIgnore]
        public List<Aspect> Aspectos { get; set; }

        /// <summary>
        /// Lista de nodos hijos
        /// </summary>
        [JsonIgnore]
        public List<Nodo> NodosHijos { get; set; }
        [JsonIgnore]
        public Type1 TipoNodo { get;  set; }
    }
}
