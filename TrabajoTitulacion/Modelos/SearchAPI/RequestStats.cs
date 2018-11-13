﻿// Code generated by Microsoft (R) AutoRest Code Generator 0.16.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace TrabajoTitulacion.Models
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Microsoft.Rest;
    using Microsoft.Rest.Serialization;

    /// <summary>
    /// A list of stats request.
    /// </summary>
    public partial class RequestStats
    {
        /// <summary>
        /// Initializes a new instance of the RequestStats class.
        /// </summary>
        public RequestStats() { }

        /// <summary>
        /// Initializes a new instance of the RequestStats class.
        /// </summary>
        public RequestStats(string field = default(string), string label = default(string), bool? min = default(bool?), bool? max = default(bool?), bool? sum = default(bool?), bool? countValues = default(bool?), bool? missing = default(bool?), bool? mean = default(bool?), bool? stddev = default(bool?), bool? sumOfSquares = default(bool?), bool? distinctValues = default(bool?), bool? countDistinct = default(bool?), bool? cardinality = default(bool?), double? cardinalityAccuracy = default(double?), IList<string> excludeFilters = default(IList<string>), IList<double?> percentiles = default(IList<double?>))
        {
            Field = field;
            Label = label;
            Min = min;
            Max = max;
            Sum = sum;
            CountValues = countValues;
            Missing = missing;
            Mean = mean;
            Stddev = stddev;
            SumOfSquares = sumOfSquares;
            DistinctValues = distinctValues;
            CountDistinct = countDistinct;
            Cardinality = cardinality;
            CardinalityAccuracy = cardinalityAccuracy;
            ExcludeFilters = excludeFilters;
            Percentiles = percentiles;
        }

        /// <summary>
        /// The stats field
        /// </summary>
        [JsonProperty(PropertyName = "field")]
        public string Field { get; set; }

        /// <summary>
        /// A label to include for reference the stats field
        /// </summary>
        [JsonProperty(PropertyName = "label")]
        public string Label { get; set; }

        /// <summary>
        /// The minimum value of the field
        /// </summary>
        [JsonProperty(PropertyName = "min")]
        public bool? Min { get; set; }

        /// <summary>
        /// The maximum value of the field
        /// </summary>
        [JsonProperty(PropertyName = "max")]
        public bool? Max { get; set; }

        /// <summary>
        /// The sum of all values of the field
        /// </summary>
        [JsonProperty(PropertyName = "sum")]
        public bool? Sum { get; set; }

        /// <summary>
        /// The number which have a value for this field
        /// </summary>
        [JsonProperty(PropertyName = "countValues")]
        public bool? CountValues { get; set; }

        /// <summary>
        /// The number which do not have a value for this field
        /// </summary>
        [JsonProperty(PropertyName = "missing")]
        public bool? Missing { get; set; }

        /// <summary>
        /// The average
        /// </summary>
        [JsonProperty(PropertyName = "mean")]
        public bool? Mean { get; set; }

        /// <summary>
        /// Standard deviation
        /// </summary>
        [JsonProperty(PropertyName = "stddev")]
        public bool? Stddev { get; set; }

        /// <summary>
        /// Sum of all values squared
        /// </summary>
        [JsonProperty(PropertyName = "sumOfSquares")]
        public bool? SumOfSquares { get; set; }

        /// <summary>
        /// The set of all distinct values for the field (This can be very
        /// expensive to calculate)
        /// </summary>
        [JsonProperty(PropertyName = "distinctValues")]
        public bool? DistinctValues { get; set; }

        /// <summary>
        /// The number of distinct values  (This can be very expensive to
        /// calculate)
        /// </summary>
        [JsonProperty(PropertyName = "countDistinct")]
        public bool? CountDistinct { get; set; }

        /// <summary>
        /// A statistical approximation of the number of distinct values
        /// </summary>
        [JsonProperty(PropertyName = "cardinality")]
        public bool? Cardinality { get; set; }

        /// <summary>
        /// Number between 0.0 and 1.0 indicating how aggressively the
        /// algorithm
        /// should try to be accurate. Used with boolean cardinality flag.
        /// </summary>
        [JsonProperty(PropertyName = "cardinalityAccuracy")]
        public double? CardinalityAccuracy { get; set; }

        /// <summary>
        /// A list of filters to exclude
        /// </summary>
        [JsonProperty(PropertyName = "excludeFilters")]
        public IList<string> ExcludeFilters { get; set; }

        /// <summary>
        /// A list of percentile values, e.g. "1,99,99.9"
        /// </summary>
        [JsonProperty(PropertyName = "percentiles")]
        public IList<double?> Percentiles { get; set; }

    }
}
