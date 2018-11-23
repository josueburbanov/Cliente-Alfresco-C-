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
    /// Limit the time and resources used for query execution
    /// </summary>
    public partial class RequestLimits
    {
        /// <summary>
        /// Initializes a new instance of the RequestLimits class.
        /// </summary>
        public RequestLimits() { }

        /// <summary>
        /// Initializes a new instance of the RequestLimits class.
        /// </summary>
        public RequestLimits(int? permissionEvaluationTime = default(int?), int? permissionEvaluationCount = default(int?))
        {
            PermissionEvaluationTime = permissionEvaluationTime;
            PermissionEvaluationCount = permissionEvaluationCount;
        }

        /// <summary>
        /// Maximum time for post query permission evaluation
        /// </summary>
        [JsonProperty(PropertyName = "permissionEvaluationTime")]
        public int? PermissionEvaluationTime { get; set; }

        /// <summary>
        /// Maximum count of post query permission evaluations
        /// </summary>
        [JsonProperty(PropertyName = "permissionEvaluationCount")]
        public int? PermissionEvaluationCount { get; set; }

    }
}