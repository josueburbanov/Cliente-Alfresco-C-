using Newtonsoft.Json;

namespace TrabajoTitulacion.Modelos.CoreAPI
{
    public class PermissionElement
    {
        /// <summary>
        /// Initializes a new instance of the PermissionElement class.
        /// </summary>
        public PermissionElement() { }

        /// <summary>
        /// Initializes a new instance of the PermissionElement class.
        /// </summary>
        public PermissionElement(string authorityId = default(string), string name = default(string), string accessStatus = default(string))
        {
            AuthorityId = authorityId;
            Name = name;
            AccessStatus = accessStatus;
        }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "authorityId")]
        public string AuthorityId { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Possible values include: 'ALLOWED', 'DENIED'
        /// </summary>
        [JsonProperty(PropertyName = "accessStatus")]
        public string AccessStatus { get; set; }

    }
}