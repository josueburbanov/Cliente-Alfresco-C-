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

    public partial class RequestSortDefinitionItem
    {
        /// <summary>
        /// Initializes a new instance of the RequestSortDefinitionItem class.
        /// </summary>
        public RequestSortDefinitionItem() { }

        /// <summary>
        /// Initializes a new instance of the RequestSortDefinitionItem class.
        /// </summary>
        public RequestSortDefinitionItem(string type = default(string), string field = default(string), bool? ascending = default(bool?))
        {
            Type = type;
            Field = field;
            Ascending = ascending;
        }

        /// <summary>
        /// How to order - using a field, when position of the document in the
        /// index, score/relevence. Possible values include: 'FIELD',
        /// 'DOCUMENT', 'SCORE'
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        /// <summary>
        /// The name of the field
        /// </summary>
        [JsonProperty(PropertyName = "field")]
        public string Field { get; set; }

        /// <summary>
        /// The sort order. (The ordering of nulls is determined by the SOLR
        /// configuration)
        /// </summary>
        [JsonProperty(PropertyName = "ascending")]
        public bool? Ascending { get; set; }

    }
}
