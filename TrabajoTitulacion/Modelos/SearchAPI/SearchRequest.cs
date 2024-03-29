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

    public partial class SearchRequest
    {
        /// <summary>
        /// Initializes a new instance of the SearchRequest class.
        /// </summary>
        public SearchRequest() { }

        /// <summary>
        /// Initializes a new instance of the SearchRequest class.
        /// </summary>
        public SearchRequest(RequestQuery query, RequestPagination paging = default(RequestPagination), IList<string> include = default(IList<string>), bool? includeRequest = default(bool?), IList<string> fields = default(IList<string>), IList<RequestSortDefinitionItem> sort = default(IList<RequestSortDefinitionItem>), IList<RequestTemplatesItem> templates = default(IList<RequestTemplatesItem>), RequestDefaults defaults = default(RequestDefaults), RequestLocalization localization = default(RequestLocalization), IList<RequestFilterQueriesItem> filterQueries = default(IList<RequestFilterQueriesItem>), IList<RequestFacetQueriesItem> facetQueries = default(IList<RequestFacetQueriesItem>), RequestFacetFields facetFields = default(RequestFacetFields), RequestFacetIntervals facetIntervals = default(RequestFacetIntervals), IList<RequestPivot> pivots = default(IList<RequestPivot>), IList<RequestStats> stats = default(IList<RequestStats>), RequestSpellcheck spellcheck = default(RequestSpellcheck), RequestScope scope = default(RequestScope), RequestLimits limits = default(RequestLimits), RequestHighlight highlight = default(RequestHighlight), IList<RequestRange> ranges = default(IList<RequestRange>))
        {
            Query = query;
            Paging = paging;
            Include = include;
            IncludeRequest = includeRequest;
            Fields = fields;
            Sort = sort;
            Templates = templates;
            Defaults = defaults;
            Localization = localization;
            FilterQueries = filterQueries;
            FacetQueries = facetQueries;
            FacetFields = facetFields;
            FacetIntervals = facetIntervals;
            Pivots = pivots;
            Stats = stats;
            Spellcheck = spellcheck;
            Scope = scope;
            Limits = limits;
            Highlight = highlight;
            Ranges = ranges;
        }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "query")]
        public RequestQuery Query { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "paging")]
        public RequestPagination Paging { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "include")]
        public IList<string> Include { get; set; }

        /// <summary>
        /// When true, include the original request in the response
        /// </summary>
        [JsonProperty(PropertyName = "includeRequest")]
        public bool? IncludeRequest { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "fields")]
        public IList<string> Fields { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "sort")]
        public IList<RequestSortDefinitionItem> Sort { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "templates")]
        public IList<RequestTemplatesItem> Templates { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "defaults")]
        public RequestDefaults Defaults { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "localization")]
        public RequestLocalization Localization { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "filterQueries")]
        public IList<RequestFilterQueriesItem> FilterQueries { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "facetQueries")]
        public IList<RequestFacetQueriesItem> FacetQueries { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "facetFields")]
        public RequestFacetFields FacetFields { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "facetIntervals")]
        public RequestFacetIntervals FacetIntervals { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "pivots")]
        public IList<RequestPivot> Pivots { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "stats")]
        public IList<RequestStats> Stats { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "spellcheck")]
        public RequestSpellcheck Spellcheck { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "scope")]
        public RequestScope Scope { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "limits")]
        public RequestLimits Limits { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "highlight")]
        public RequestHighlight Highlight { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "ranges")]
        public IList<RequestRange> Ranges { get; set; }

        /// <summary>
        /// Validate the object. Throws ValidationException if validation fails.
        /// </summary>
        public virtual void Validate()
        {
            if (Query == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "Query");
            }
            if (this.Query != null)
            {
                this.Query.Validate();
            }
            if (this.Paging != null)
            {
                this.Paging.Validate();
            }
        }
    }
}
