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

    public partial class RequestFacetQueriesItem
    {
        /// <summary>
        /// Initializes a new instance of the RequestFacetQueriesItem class.
        /// </summary>
        public RequestFacetQueriesItem() { }

        /// <summary>
        /// Initializes a new instance of the RequestFacetQueriesItem class.
        /// </summary>
        public RequestFacetQueriesItem(string query = default(string), string label = default(string))
        {
            Query = query;
            Label = label;
        }

        /// <summary>
        /// A facet query
        /// </summary>
        [JsonProperty(PropertyName = "query")]
        public string Query { get; set; }

        /// <summary>
        /// A label to include in place of the facet query
        /// </summary>
        [JsonProperty(PropertyName = "label")]
        public string Label { get; set; }

    }
}
