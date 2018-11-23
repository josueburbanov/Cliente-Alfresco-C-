using Newtonsoft.Json;
using System.Collections.Generic;

namespace TrabajoTitulacion.Modelos.CMM
{
    public class Type
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; } //Etiqueta de presentación
        [JsonProperty(PropertyName = "parentName")]
        public string ParentName { get; set; }
        [JsonProperty(PropertyName = "prefixedName")]
        public string PrefixedName { get; set; }
        [JsonProperty(PropertyName = "properties")]
        public List<Property> Properties { get; set; }
        //Propiedad añadida por el desarrollador:
        [JsonIgnore]
        public Model ModeloPerteneciente { get; set; }

        public Type()
        {
            ModeloPerteneciente = new Model();
        }

        public override string ToString()
        {
            return PrefixedName;
        }
    }
}