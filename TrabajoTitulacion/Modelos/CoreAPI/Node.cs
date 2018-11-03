using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Microsoft.Rest;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabajoTitulacion.Modelos.CoreAPI
{
    public class Node
    {
        public Node() { }

        /// <summary>
        /// Initializes a new instance of the Node class.
        /// </summary>
        public Node(string id, string name, string nodeType, bool isFolder, bool isFile, DateTime modifiedAt, UserInfo modifiedByUser, DateTime createdAt, UserInfo createdByUser, bool? isLocked = default(bool?), string parentId = default(string), bool? isLink = default(bool?), ContentInfo content = default(ContentInfo), IList<string> aspectNames = default(IList<string>), object properties = default(object), IList<string> allowableOperations = default(IList<string>), PathInfo path = default(PathInfo), PermissionsInfo permissions = default(PermissionsInfo))
        {
            Id = id;
            Name = name;
            NodeType = nodeType;
            IsFolder = isFolder;
            IsFile = isFile;
            IsLocked = isLocked;
            ModifiedAt = modifiedAt;
            ModifiedByUser = modifiedByUser;
            CreatedAt = createdAt;
            CreatedByUser = createdByUser;
            ParentId = parentId;
            IsLink = isLink;
            Content = content;
            AspectNames = aspectNames;
            Properties = properties;
            AllowableOperations = allowableOperations;
            Path = path;
            Permissions = permissions;            
        }

        

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

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
        [JsonProperty(PropertyName = "isFolder")]
        public bool IsFolder { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "isFile")]
        public bool IsFile { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "isLocked")]
        public bool? IsLocked { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "modifiedAt")]
        public DateTime ModifiedAt { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "modifiedByUser")]
        public UserInfo ModifiedByUser { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "createdAt")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "createdByUser")]
        public UserInfo CreatedByUser { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "parentId")]
        public string ParentId { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "isLink")]
        public bool? IsLink { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "content")]
        public ContentInfo Content { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "aspectNames")]
        public IList<string> AspectNames { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "properties")]
        public object Properties { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "allowableOperations")]
        public IList<string> AllowableOperations { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "path")]
        public PathInfo Path { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "permissions")]
        public PermissionsInfo Permissions { get; set; }

        /// <summary>
        /// Validate the object. Throws ValidationException if validation fails.
        /// </summary>
        public virtual void Validate()
        {
            if (Id == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "Id");
            }
            if (Name == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "Name");
            }
            if (NodeType == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "NodeType");
            }
            if (ModifiedByUser == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "ModifiedByUser");
            }
            if (CreatedByUser == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "CreatedByUser");
            }
            if (this.Name != null)
            {
                if (!System.Text.RegularExpressions.Regex.IsMatch(this.Name, "^(?!(.*[\"\\*\\\\\\>\\<\\?\\/\\:\\|]+.*)| (.*[\\.]?.*[\\.] +$)| (.*[ ] +$))"))
                {
                    throw new ValidationException(ValidationRules.Pattern, "Name", "^(?!(.*[\"\\*\\\\\\>\\<\\?\\/\\:\\|]+.*)| (.*[\\.]?.*[\\.] +$)| (.*[ ] +$))");
                }
            }
            if (this.ModifiedByUser != null)
            {
                this.ModifiedByUser.Validate();
            }
            if (this.CreatedByUser != null)
            {
                this.CreatedByUser.Validate();
            }
            if (this.Content != null)
            {
                this.Content.Validate();
            }
        }
    }
}

