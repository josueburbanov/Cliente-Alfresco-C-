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

    public partial class RequestFacetIntervalsIntervalsItem
    {
        /// <summary>
        /// Initializes a new instance of the
        /// RequestFacetIntervalsIntervalsItem class.
        /// </summary>
        public RequestFacetIntervalsIntervalsItem() { }

        /// <summary>
        /// Initializes a new instance of the
        /// RequestFacetIntervalsIntervalsItem class.
        /// </summary>
        public RequestFacetIntervalsIntervalsItem(string field = default(string), string label = default(string), IList<RequestFacetSet> sets = default(IList<RequestFacetSet>))
        {
            Field = field;
            Label = label;
            Sets = sets;
        }

        /// <summary>
        /// The field to facet on
        /// </summary>
        [JsonProperty(PropertyName = "field")]
        public string Field { get; set; }

        /// <summary>
        /// A label to use to identify the field facet
        /// </summary>
        [JsonProperty(PropertyName = "label")]
        public string Label { get; set; }

        /// <summary>
        /// Sets the intervals for all fields.
        /// </summary>
        [JsonProperty(PropertyName = "sets")]
        public IList<RequestFacetSet> Sets { get; set; }

    }
}