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
    /// Additional information of nested facet
    /// </summary>
    public partial class GenericBucketBucketInfo
    {
        /// <summary>
        /// Initializes a new instance of the GenericBucketBucketInfo class.
        /// </summary>
        public GenericBucketBucketInfo() { }

        /// <summary>
        /// Initializes a new instance of the GenericBucketBucketInfo class.
        /// </summary>
        public GenericBucketBucketInfo(string start = default(string), bool? startInclusive = default(bool?), string end = default(string), bool? endInclusive = default(bool?))
        {
            Start = start;
            StartInclusive = startInclusive;
            End = end;
            EndInclusive = endInclusive;
        }

        /// <summary>
        /// The start of range
        /// </summary>
        [JsonProperty(PropertyName = "start")]
        public string Start { get; set; }

        /// <summary>
        /// Includes values greater or equal to "start"
        /// </summary>
        [JsonProperty(PropertyName = "startInclusive")]
        public bool? StartInclusive { get; set; }

        /// <summary>
        /// The end of range
        /// </summary>
        [JsonProperty(PropertyName = "end")]
        public string End { get; set; }

        /// <summary>
        /// Includes values less than or equal to "end"
        /// </summary>
        [JsonProperty(PropertyName = "endInclusive")]
        public bool? EndInclusive { get; set; }

    }
}
