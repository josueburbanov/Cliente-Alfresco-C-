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
    /// Facet Intervals
    /// </summary>
    public partial class RequestFacetIntervals
    {
        /// <summary>
        /// Initializes a new instance of the RequestFacetIntervals class.
        /// </summary>
        public RequestFacetIntervals() { }

        /// <summary>
        /// Initializes a new instance of the RequestFacetIntervals class.
        /// </summary>
        public RequestFacetIntervals(IList<RequestFacetSet> sets = default(IList<RequestFacetSet>), IList<RequestFacetIntervalsIntervalsItem> intervals = default(IList<RequestFacetIntervalsIntervalsItem>))
        {
            Sets = sets;
            Intervals = intervals;
        }

        /// <summary>
        /// Sets the intervals for all fields.
        /// </summary>
        [JsonProperty(PropertyName = "sets")]
        public IList<RequestFacetSet> Sets { get; set; }

        /// <summary>
        /// Specifies the fields to facet by interval.
        /// </summary>
        [JsonProperty(PropertyName = "intervals")]
        public IList<RequestFacetIntervalsIntervalsItem> Intervals { get; set; }

    }
}
