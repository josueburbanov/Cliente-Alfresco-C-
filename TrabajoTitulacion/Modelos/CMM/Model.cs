using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabajoTitulacion.Modelos.CMM
{
    public class Model
    {
        [JsonProperty(PropertyName = "namespaceUri")]
        public string NamespaceUri { get; set; }
        [JsonProperty(PropertyName = "namespacePrefix")]
        public string NamespacePrefix { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "author")]
        public string Author { get; set; }
        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
