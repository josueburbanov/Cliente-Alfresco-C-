using Newtonsoft.Json;
using System.Collections.Generic;

namespace TrabajoTitulacion.Modelos.CoreAPI
{
    public class PermissionsInfo
    {
        /// <summary>
        /// Initializes a new instance of the PermissionsInfo class.
        /// </summary>
        public PermissionsInfo() { }

        /// <summary>
        /// Initializes a new instance of the PermissionsInfo class.
        /// </summary>
        public PermissionsInfo(bool? isInheritanceEnabled = default(bool?), IList<PermissionElement> inherited = default(IList<PermissionElement>), IList<PermissionElement> locallySet = default(IList<PermissionElement>), IList<string> settable = default(IList<string>))
        {
            IsInheritanceEnabled = isInheritanceEnabled;
            Inherited = inherited;
            LocallySet = locallySet;
            Settable = settable;
        }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "isInheritanceEnabled")]
        public bool? IsInheritanceEnabled { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "inherited")]
        public IList<PermissionElement> Inherited { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "locallySet")]
        public IList<PermissionElement> LocallySet { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "settable")]
        public IList<string> Settable { get; set; }

    }
}