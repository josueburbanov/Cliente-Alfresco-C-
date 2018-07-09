using Newtonsoft.Json;
using System.Collections.Generic;

namespace TrabajoTitulacion.Modelos
{
    public class PathInfo
    {
        /// <summary>
        /// Initializes a new instance of the PathInfo class.
        /// </summary>
        public PathInfo() { }

        /// <summary>
        /// Initializes a new instance of the PathInfo class.
        /// </summary>
        public PathInfo(IList<PathElement> elements = default(IList<PathElement>), string name = default(string), bool? isComplete = default(bool?))
        {
            Elements = elements;
            Name = name;
            IsComplete = isComplete;
        }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "elements")]
        public IList<PathElement> Elements { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "isComplete")]
        public bool? IsComplete { get; set; }

    }
}