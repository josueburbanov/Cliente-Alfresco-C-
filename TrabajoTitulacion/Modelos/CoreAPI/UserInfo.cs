using Microsoft.Rest;
using Newtonsoft.Json;

namespace TrabajoTitulacion.Modelos.CoreAPI
{
    public class UserInfo
    {
        public UserInfo() { }

        /// <summary>
        /// Initializes a new instance of the UserInfo class.
        /// </summary>
        public UserInfo(string displayName, string id)
        {
            DisplayName = displayName;
            Id = id;
        }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "displayName")]
        public string DisplayName { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        /// <summary>
        /// Validate the object. Throws ValidationException if validation fails.
        /// </summary>
        public virtual void Validate()
        {
            if (DisplayName == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "DisplayName");
            }
            if (Id == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "Id");
            }
        }

    }
}