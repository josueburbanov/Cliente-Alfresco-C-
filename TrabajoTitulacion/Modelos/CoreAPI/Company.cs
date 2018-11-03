using Newtonsoft.Json;

namespace TrabajoTitulacion.Modelos.CoreAPI
{
    public class Company
    {
        /// <summary>
        /// Initializes a new instance of the Company class.
        /// </summary>
        public Company() { }

        /// <summary>
        /// Initializes a new instance of the Company class.
        /// </summary>
        public Company(string organization = default(string), string address1 = default(string), string address2 = default(string), string address3 = default(string), string postcode = default(string), string telephone = default(string), string fax = default(string), string email = default(string))
        {
            Organization = organization;
            Address1 = address1;
            Address2 = address2;
            Address3 = address3;
            Postcode = postcode;
            Telephone = telephone;
            Fax = fax;
            Email = email;
        }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "organization")]
        public string Organization { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "address1")]
        public string Address1 { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "address2")]
        public string Address2 { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "address3")]
        public string Address3 { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "postcode")]
        public string Postcode { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "telephone")]
        public string Telephone { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "fax")]
        public string Fax { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }
    }
}