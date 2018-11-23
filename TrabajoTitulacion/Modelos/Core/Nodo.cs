using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrabajoTitulacion.Modelos.CMM;
using TrabajoTitulacion.Modelos.CoreAPI;

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
        public CMM.Type TipoNodo { get;  set; }
        [JsonIgnore]
        public byte[] Contenido { get; set; }

    }
}
