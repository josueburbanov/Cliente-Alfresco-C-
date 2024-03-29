﻿// Code generated by Microsoft (R) AutoRest Code Generator 0.16.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace TrabajoTitulacion.Modelos.CoreAPI
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Microsoft.Rest;
    using Microsoft.Rest.Serialization;
    using TrabajoTitulacion.Modelos.CoreAPI;

    public partial class PermissionsBodyUpdate
    {
        /// <summary>
        /// Initializes a new instance of the PermissionsBodyUpdate class.
        /// </summary>
        public PermissionsBodyUpdate() { }

        /// <summary>
        /// Initializes a new instance of the PermissionsBodyUpdate class.
        /// </summary>
        public PermissionsBodyUpdate(bool? isInheritanceEnabled = default(bool?), IList<PermissionElement> locallySet = default(IList<PermissionElement>))
        {
            IsInheritanceEnabled = isInheritanceEnabled;
            LocallySet = locallySet;
        }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "isInheritanceEnabled")]
        public bool? IsInheritanceEnabled { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "locallySet")]
        public IList<PermissionElement> LocallySet { get; set; }

    }
}
