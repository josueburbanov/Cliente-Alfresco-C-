using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabajoTitulacion.Modelos.CMM
{
    public class Constraint
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "prefixedName")]
        public string PrefixedName { get; set; }
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }
        [JsonProperty(PropertyName = "parameters")]
        public List<Parameter> Parameters { get; set; }

        public Constraint(string name)
        {
            Name = name;
        }
    }
}
