using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabajoTitulacion.Modelos.CMM
{
    class PropertiesBodyUpdate
    {
        [JsonProperty(PropertyName = "name")]
        private string Name { get; set; }
        [JsonProperty(PropertyName = "properties")]
        private List<Property> Properties { get; set; }

        public PropertiesBodyUpdate(string name ,List<Property>properties)
        {
            Properties = properties;
            Name = name;
        }
    }
}
