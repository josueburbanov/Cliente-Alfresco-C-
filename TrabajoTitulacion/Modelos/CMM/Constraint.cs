using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabajoTitulacion.Modelos.CMM
{
    public class Constraint
    {
        public string Name { get; set; }
        public string PrefixedName { get; set; }
        public string Type { get; set; }
        public List<Parameter> Parameters { get; set; }

        public Constraint(string name)
        {
            Name = name;
        }
    }
}
