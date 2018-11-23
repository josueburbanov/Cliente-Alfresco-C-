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

    public partial class GroupMemberEntry
    {
        /// <summary>
        /// Initializes a new instance of the GroupMemberEntry class.
        /// </summary>
        public GroupMemberEntry() { }

        /// <summary>
        /// Initializes a new instance of the GroupMemberEntry class.
        /// </summary>
        public GroupMemberEntry(GroupMember entry)
        {
            Entry = entry;
        }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "entry")]
        public GroupMember Entry { get; set; }

        /// <summary>
        /// Validate the object. Throws ValidationException if validation fails.
        /// </summary>
        public virtual void Validate()
        {
            if (Entry == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "Entry");
            }
            if (this.Entry != null)
            {
                this.Entry.Validate();
            }
        }
    }
}
