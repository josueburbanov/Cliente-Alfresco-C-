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

    public partial class RequestTemplatesItem
    {
        /// <summary>
        /// Initializes a new instance of the RequestTemplatesItem class.
        /// </summary>
        public RequestTemplatesItem() { }

        /// <summary>
        /// Initializes a new instance of the RequestTemplatesItem class.
        /// </summary>
        public RequestTemplatesItem(string name = default(string), string template = default(string))
        {
            Name = name;
            Template = template;
        }

        /// <summary>
        /// The template name
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// The template
        /// </summary>
        [JsonProperty(PropertyName = "template")]
        public string Template { get; set; }

    }
}