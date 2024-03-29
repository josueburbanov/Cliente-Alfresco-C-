﻿// Code generated by Microsoft (R) AutoRest Code Generator 0.16.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace TrabajoTitulacion.Models
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Microsoft.Rest;
    using Microsoft.Rest.Serialization;

    public partial class PathInfo
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
