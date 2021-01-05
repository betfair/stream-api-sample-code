using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;

namespace Betfair.ESASwagger.Model {
    /// <summary>
    /// </summary>
    [DataContract]
    public class OrderFilter : IEquatable<OrderFilter> {
        /// <summary>
        ///     Initializes a new instance of the <see cref="OrderFilter" /> class.
        ///     Initializes a new instance of the <see cref="OrderFilter" />class.
        /// </summary>
        /// <param name="AccountIds">AccountIds.</param>
        public OrderFilter(List<long?> AccountIds = null) {
            this.AccountIds = AccountIds;
        }


        /// <summary>
        ///     Gets or Sets AccountIds
        /// </summary>
        [DataMember(Name = "accountIds", EmitDefaultValue = false)]
        public List<long?> AccountIds { get; set; }

        /// <summary>
        ///     Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString() {
            var sb = new StringBuilder();
            sb.Append("class OrderFilter {\n");
            sb.Append("  AccountIds: ")
                .Append(AccountIds)
                .Append("\n");

            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        ///     Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public string ToJson() {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        ///     Returns true if objects are equal
        /// </summary>
        /// <param name="obj">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object obj) {
            // credit: http://stackoverflow.com/a/10454552/677735
            return Equals(obj as OrderFilter);
        }

        /// <summary>
        ///     Returns true if OrderFilter instances are equal
        /// </summary>
        /// <param name="other">Instance of OrderFilter to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(OrderFilter other) {
            // credit: http://stackoverflow.com/a/10454552/677735
            if (other == null)
                return false;

            return AccountIds == other.AccountIds || AccountIds != null && AccountIds.SequenceEqual(other.AccountIds);
        }

        /// <summary>
        ///     Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode() {
            // credit: http://stackoverflow.com/a/263416/677735
            unchecked // Overflow is fine, just wrap
            {
                var hash = 41;
                // Suitable nullity checks etc, of course :)

                if (AccountIds != null)
                    hash = hash * 59 + AccountIds.GetHashCode();

                return hash;
            }
        }
    }
}
