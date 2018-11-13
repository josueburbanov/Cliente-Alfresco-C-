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

    /// <summary>
    /// A metric used in faceting
    /// </summary>
    public partial class GenericMetric
    {
        /// <summary>
        /// Initializes a new instance of the GenericMetric class.
        /// </summary>
        public GenericMetric() { }

        /// <summary>
        /// Initializes a new instance of the GenericMetric class.
        /// </summary>
        public GenericMetric(string type = default(string), object value = default(object))
        {
            Type = type;
            Value = value;
        }

        /// <summary>
        /// The type of metric, e.g. count
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        /// <summary>
        /// The metric value, e.g. {"count": 34}
        /// </summary>
        [JsonProperty(PropertyName = "value")]
        public object Value { get; set; }

    }
}
