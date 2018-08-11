using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabajoTitulacion.Modelos.CMM
{
    public class Model
    {
        public string NamespaceUri { get; set; }
        public string NamespacePrefix { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public List<Type1> Types { get; set; }
    }
}
