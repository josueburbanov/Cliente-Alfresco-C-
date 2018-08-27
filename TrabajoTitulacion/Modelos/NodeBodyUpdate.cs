﻿// Code generated by Microsoft (R) AutoRest Code Generator 0.16.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace srvApi.Models
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Microsoft.Rest;
    using Microsoft.Rest.Serialization;

    public partial class NodeBodyUpdate
    {
        /// <summary>
        /// Initializes a new instance of the NodeBodyUpdate class.
        /// </summary>
        public NodeBodyUpdate() { }

        /// <summary>
        /// Initializes a new instance of the NodeBodyUpdate class.
        /// </summary>
        public NodeBodyUpdate(string name = default(string), string nodeType = default(string), IList<string> aspectNames = default(IList<string>), IDictionary<string, string> properties = default(IDictionary<string, string>), PermissionsBodyUpdate permissions = default(PermissionsBodyUpdate))
        {
            Name = name;
            NodeType = nodeType;
            AspectNames = aspectNames;
            Properties = properties;
            Permissions = permissions;
        }

        /// <summary>
        /// The name must not contain spaces or the following special
        /// characters: * " &lt; &gt; \\\\ / ? : and |.
        /// The character . must not be used at the end of the name.
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "nodeType")]
        public string NodeType { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "aspectNames")]
        public IList<string> AspectNames { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "properties")]
        public IDictionary<string, string> Properties { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "permissions")]
        public PermissionsBodyUpdate Permissions { get; set; }

        /// <summary>
        /// Validate the object. Throws ValidationException if validation fails.
        /// </summary>
        public virtual void Validate()
        {
            if (this.Name != null)
            {
                if (!System.Text.RegularExpressions.Regex.IsMatch(this.Name, "^(?!(.*[\"\\*\\\\\\>\\<\\?\\/\\:\\|]+.*)|(.*[\\.]?.*[\\.]+$)|(.*[ ]+$))"))
                {
                    throw new ValidationException(ValidationRules.Pattern, "Name", "^(?!(.*[\"\\*\\\\\\>\\<\\?\\/\\:\\|]+.*)|(.*[\\.]?.*[\\.]+$)|(.*[ ]+$))");
                }
            }
        }
    }
}
