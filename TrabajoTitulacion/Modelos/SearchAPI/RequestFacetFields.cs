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
    /// Simple facet fields to include
    /// The Properties reflect the global properties related to field facts in
    /// SOLR.
    /// They are descripbed in detail by the SOLR documentation
    /// </summary>
    public partial class RequestFacetFields
    {
        /// <summary>
        /// Initializes a new instance of the RequestFacetFields class.
        /// </summary>
        public RequestFacetFields() { }

        /// <summary>
        /// Initializes a new instance of the RequestFacetFields class.
        /// </summary>
        public RequestFacetFields(IList<RequestFacetField> facets = default(IList<RequestFacetField>))
        {
            Facets = facets;
        }

        /// <summary>
        /// Define specifc fields on which to facet (adds SOLR facet.field and
        /// f.&lt;field&gt;.facet.* options)
        /// </summary>
        [JsonProperty(PropertyName = "facets")]
        public IList<RequestFacetField> Facets { get; set; }

    }
}
