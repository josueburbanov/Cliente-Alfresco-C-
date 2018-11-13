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

    public partial class ResultBuckets
    {
        /// <summary>
        /// Initializes a new instance of the ResultBuckets class.
        /// </summary>
        public ResultBuckets() { }

        /// <summary>
        /// Initializes a new instance of the ResultBuckets class.
        /// </summary>
        public ResultBuckets(string label = default(string), IList<ResultBucketsBucketsItem> buckets = default(IList<ResultBucketsBucketsItem>))
        {
            Label = label;
            Buckets = buckets;
        }

        /// <summary>
        /// The field name or its explicit label, if provided on the request
        /// </summary>
        [JsonProperty(PropertyName = "label")]
        public string Label { get; set; }

        /// <summary>
        /// An array of buckets and values
        /// </summary>
        [JsonProperty(PropertyName = "buckets")]
        public IList<ResultBucketsBucketsItem> Buckets { get; set; }

    }
}
