using Newtonsoft.Json;

namespace TrabajoTitulacion.Modelos
{
    public class PathElement
    {
        /// <summary>
        /// Initializes a new instance of the PathElement class.
        /// </summary>
        public PathElement() { }

        /// <summary>
        /// Initializes a new instance of the PathElement class.
        /// </summary>
        public PathElement(string id = default(string), string name = default(string))
        {
            Id = id;
            Name = name;
        }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

    }
}