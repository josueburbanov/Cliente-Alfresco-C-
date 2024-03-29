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
    /// Context that applies to the whole result set
    /// </summary>
    public partial class ResultSetContext
    {
        /// <summary>
        /// Initializes a new instance of the ResultSetContext class.
        /// </summary>
        public ResultSetContext() { }

        /// <summary>
        /// Initializes a new instance of the ResultSetContext class.
        /// </summary>
        public ResultSetContext(ResponseConsistency consistency = default(ResponseConsistency), SearchRequest request = default(SearchRequest), IList<ResultSetContextFacetQueriesItem> facetQueries = default(IList<ResultSetContextFacetQueriesItem>), IList<ResultBuckets> facetsFields = default(IList<ResultBuckets>), IList<GenericFacetResponse> facets = default(IList<GenericFacetResponse>), IList<ResultSetContextSpellcheckItem> spellcheck = default(IList<ResultSetContextSpellcheckItem>))
        {
            Consistency = consistency;
            Request = request;
            FacetQueries = facetQueries;
            FacetsFields = facetsFields;
            Facets = facets;
            Spellcheck = spellcheck;
        }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "consistency")]
        public ResponseConsistency Consistency { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "request")]
        public SearchRequest Request { get; set; }

        /// <summary>
        /// The counts from facet queries
        /// </summary>
        [JsonProperty(PropertyName = "facetQueries")]
        public IList<ResultSetContextFacetQueriesItem> FacetQueries { get; set; }

        /// <summary>
        /// The counts from field facets
        /// </summary>
        [JsonProperty(PropertyName = "facetsFields")]
        public IList<ResultBuckets> FacetsFields { get; set; }

        /// <summary>
        /// The faceted response
        /// </summary>
        [JsonProperty(PropertyName = "facets")]
        public IList<GenericFacetResponse> Facets { get; set; }

        /// <summary>
        /// Suggested corrections
        /// 
        /// If zero results were found for the original query then a single
        /// entry of type "searchInsteadFor" will be returned.
        /// If alternatives were found that return more results than the
        /// original query they are returned as "didYouMean" options.
        /// The highest quality suggestion is first.
        /// </summary>
        [JsonProperty(PropertyName = "spellcheck")]
        public IList<ResultSetContextSpellcheckItem> Spellcheck { get; set; }

        /// <summary>
        /// Validate the object. Throws ValidationException if validation fails.
        /// </summary>
        public virtual void Validate()
        {
            if (this.Request != null)
            {
                this.Request.Validate();
            }
        }
    }
}
