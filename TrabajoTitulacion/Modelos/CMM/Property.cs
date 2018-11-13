using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabajoTitulacion.Modelos.CMM
{
    public class Property
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }
        [JsonProperty(PropertyName = "dataType")]
        public string Datatype { get; set; }
        [JsonProperty(PropertyName = "indexed")]
        public bool Indexed { get; set; }
        [JsonProperty(PropertyName = "mandatory")]
        public bool Mandatory { get; set; }
        [JsonProperty(PropertyName = "defaultValue")]
        public string DefaultValue { get; set; }        
        [JsonProperty(PropertyName = "constraints")]
        public List<Constraint> Constraints { get; set; }
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }
        [JsonIgnore]
        public dynamic Value { get; set; }
        [JsonProperty(PropertyName = "prefixedName")]
        public string PrefixedName { get; set; }
        [JsonProperty(PropertyName = "indexTokenisationMode")]
        public string IndexTokenisationMode { get; set; }
        [JsonProperty(PropertyName = "multiValued")]
        public bool MultiValued { get; set; }
        [JsonProperty(PropertyName = "facetable")]
        public string Facetable { get; set; }
        [JsonProperty(PropertyName = "mandatoryEnforced")]
        public bool MandatoryEnforced { get; set; }

        public Property() {
            Constraints = new List<Constraint>();
        }

        public Property(string name, string title, string datatype)
        {
            Name = name;
            PrefixedName = name;
            Title = title;
            Datatype = datatype;
        }

        public Property(string name, string title, string description,string datatype)
        {
            Name = name;
            PrefixedName = name;
            Title = title;
            Description = description;
            Datatype = datatype;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
