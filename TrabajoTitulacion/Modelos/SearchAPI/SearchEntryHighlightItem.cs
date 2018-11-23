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

    public partial class SearchEntryHighlightItem
    {
        /// <summary>
        /// Initializes a new instance of the SearchEntryHighlightItem class.
        /// </summary>
        public SearchEntryHighlightItem() { }

        /// <summary>
        /// Initializes a new instance of the SearchEntryHighlightItem class.
        /// </summary>
        public SearchEntryHighlightItem(string field = default(string), IList<string> snippets = default(IList<string>))
        {
            Field = field;
            Snippets = snippets;
        }

        /// <summary>
        /// The field where a match occured (one of the fields defined on the
        /// request)
        /// </summary>
        [JsonProperty(PropertyName = "field")]
        public string Field { get; set; }

        /// <summary>
        /// Any number of snippets for the specified field highlighting the
        /// matching text
        /// </summary>
        [JsonProperty(PropertyName = "snippets")]
        public IList<string> Snippets { get; set; }

    }
}