using Microsoft.Rest;
using Newtonsoft.Json;

namespace TrabajoTitulacion.Modelos.CoreAPI
{
    public class ContentInfo
    {
        /// <summary>
        /// Initializes a new instance of the ContentInfo class.
        /// </summary>
        public ContentInfo() { }

        /// <summary>
        /// Initializes a new instance of the ContentInfo class.
        /// </summary>
        public ContentInfo(string mimeType, string mimeTypeName, int sizeInBytes, string encoding = default(string))
        {
            MimeType = mimeType;
            MimeTypeName = mimeTypeName;
            SizeInBytes = sizeInBytes;
            Encoding = encoding;
        }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "mimeType")]
        public string MimeType { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "mimeTypeName")]
        public string MimeTypeName { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "sizeInBytes")]
        public int SizeInBytes { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "encoding")]
        public string Encoding { get; set; }

        /// <summary>
        /// Validate the object. Throws ValidationException if validation fails.
        /// </summary>
        public virtual void Validate()
        {
            if (MimeType == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "MimeType");
            }
            if (MimeTypeName == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "MimeTypeName");
            }
        }
    }
}