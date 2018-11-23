using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabajoTitulacion.Modelos.CMM
{
    public class Parameter
    {
        [JsonProperty(PropertyName = "name")]
        public string Name;
        [JsonProperty(PropertyName = "listValue")]
        public List<string> ListValue;
        [JsonProperty(PropertyName = "simpleValue")]
        public string SimpleValue;
    }
}
