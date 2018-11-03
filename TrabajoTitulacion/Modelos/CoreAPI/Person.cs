using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Microsoft.Rest;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabajoTitulacion.Modelos.CoreAPI
{
    public class Person
    {
        public Person() { }

        /// <summary>
        /// Initializes a new instance of the Person class.
        /// </summary>
        public Person(string id, string firstName, string email, bool enabled, string lastName = default(string), string description = default(string), string avatarId = default(string), string skypeId = default(string), string googleId = default(string), string instantMessageId = default(string), string jobTitle = default(string), string location = default(string), Company company = default(Company), string mobile = default(string), string telephone = default(string), DateTime? statusUpdatedAt = default(DateTime?), string userStatus = default(string), bool? emailNotificationsEnabled = default(bool?), IList<string> aspectNames = default(IList<string>), IDictionary<string, string> properties = default(IDictionary<string, string>))
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Description = description;
            AvatarId = avatarId;
            Email = email;
            SkypeId = skypeId;
            GoogleId = googleId;
            InstantMessageId = instantMessageId;
            JobTitle = jobTitle;
            Location = location;
            Company = company;
            Mobile = mobile;
            Telephone = telephone;
            StatusUpdatedAt = statusUpdatedAt;
            UserStatus = userStatus;
            Enabled = enabled;
            EmailNotificationsEnabled = emailNotificationsEnabled;
            AspectNames = aspectNames;
            Properties = properties;
        }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "firstName")]
        public string FirstName { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "lastName")]
        public string LastName { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "avatarId")]
        public string AvatarId { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "skypeId")]
        public string SkypeId { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "googleId")]
        public string GoogleId { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "instantMessageId")]
        public string InstantMessageId { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "jobTitle")]
        public string JobTitle { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "location")]
        public string Location { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "company")]
        public Company Company { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "mobile")]
        public string Mobile { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "telephone")]
        public string Telephone { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "statusUpdatedAt")]
        public DateTime? StatusUpdatedAt { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "userStatus")]
        public string UserStatus { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "enabled")]
        public bool Enabled { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "emailNotificationsEnabled")]
        public bool? EmailNotificationsEnabled { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "aspectNames")]
        public IList<string> AspectNames { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "properties")]
        public IDictionary<string, string> Properties { get; set; }

        /// <summary>
        /// Validate the object. Throws ValidationException if validation fails.
        /// </summary>
        public virtual void Validate()
        {
            if (Id == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "Id");
            }
            if (FirstName == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "FirstName");
            }
            if (Email == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "Email");
            }
        }
    }
}