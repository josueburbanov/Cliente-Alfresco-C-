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

    public partial class GroupPagingList
    {
        /// <summary>
        /// Initializes a new instance of the GroupPagingList class.
        /// </summary>
        public GroupPagingList() { }

        /// <summary>
        /// Initializes a new instance of the GroupPagingList class.
        /// </summary>
        public GroupPagingList(Pagination pagination = default(Pagination), IList<GroupEntry> entries = default(IList<GroupEntry>))
        {
            Pagination = pagination;
            Entries = entries;
        }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "pagination")]
        public Pagination Pagination { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "entries")]
        public IList<GroupEntry> Entries { get; set; }

        /// <summary>
        /// Validate the object. Throws ValidationException if validation fails.
        /// </summary>
        public virtual void Validate()
        {
            if (this.Pagination != null)
            {
                this.Pagination.Validate();
            }
            if (this.Entries != null)
            {
                foreach (var element in this.Entries)
                {
                    if (element != null)
                    {
                        element.Validate();
                    }
                }
            }
        }
    }
}