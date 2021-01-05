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
    public class OrderMarketChange : IEquatable<OrderMarketChange> {
        /// <summary>
        ///     Initializes a new instance of the <see cref="OrderMarketChange" /> class.
        ///     Initializes a new instance of the <see cref="OrderMarketChange" />class.
        /// </summary>
        /// <param name="AccountId">AccountId.</param>
        /// <param name="Orc">Order Changes - a list of changes to orders on a selection.</param>
        /// <param name="Closed">Closed.</param>
        /// <param name="Id">Market Id - the id of the market the order is on.</param>
        /// <param name="FullImage">FullImage.</param>
        public OrderMarketChange(long? AccountId = null, List<OrderRunnerChange> Orc = null, bool? Closed = null, string Id = null, bool? FullImage = null) {
            this.AccountId = AccountId;
            this.Orc = Orc;
            this.Closed = Closed;
            this.Id = Id;
            this.FullImage = FullImage;
        }


        /// <summary>
        ///     Gets or Sets AccountId
        /// </summary>
        [DataMember(Name = "accountId", EmitDefaultValue = false)]
        public long? AccountId { get; set; }

        /// <summary>
        ///     Order Changes - a list of changes to orders on a selection
        /// </summary>
        /// <value>Order Changes - a list of changes to orders on a selection</value>
        [DataMember(Name = "orc", EmitDefaultValue = false)]
        public List<OrderRunnerChange> Orc { get; set; }

        /// <summary>
        ///     Gets or Sets Closed
        /// </summary>
        [DataMember(Name = "closed", EmitDefaultValue = false)]
        public bool? Closed { get; set; }

        /// <summary>
        ///     Market Id - the id of the market the order is on
        /// </summary>
        /// <value>Market Id - the id of the market the order is on</value>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public string Id { get; set; }

        /// <summary>
        ///     Gets or Sets FullImage
        /// </summary>
        [DataMember(Name = "fullImage", EmitDefaultValue = false)]
        public bool? FullImage { get; set; }

        /// <summary>
        ///     Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString() {
            var sb = new StringBuilder();
            sb.Append("class OrderMarketChange {\n");
            sb.Append("  AccountId: ")
                .Append(AccountId)
                .Append("\n");
            sb.Append("  Orc: ")
                .Append(Orc)
                .Append("\n");
            sb.Append("  Closed: ")
                .Append(Closed)
                .Append("\n");
            sb.Append("  Id: ")
                .Append(Id)
                .Append("\n");
            sb.Append("  FullImage: ")
                .Append(FullImage)
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
            return Equals(obj as OrderMarketChange);
        }

        /// <summary>
        ///     Returns true if OrderMarketChange instances are equal
        /// </summary>
        /// <param name="other">Instance of OrderMarketChange to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(OrderMarketChange other) {
            // credit: http://stackoverflow.com/a/10454552/677735
            if (other == null)
                return false;

            return (AccountId == other.AccountId || AccountId != null && AccountId.Equals(other.AccountId)) &&
                   (Orc == other.Orc || Orc != null && Orc.SequenceEqual(other.Orc)) &&
                   (Closed == other.Closed || Closed != null && Closed.Equals(other.Closed)) &&
                   (Id == other.Id || Id != null && Id.Equals(other.Id)) &&
                   (FullImage == other.FullImage || FullImage != null && FullImage.Equals(other.FullImage));
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

                if (AccountId != null)
                    hash = hash * 59 + AccountId.GetHashCode();

                if (Orc != null)
                    hash = hash * 59 + Orc.GetHashCode();

                if (Closed != null)
                    hash = hash * 59 + Closed.GetHashCode();

                if (Id != null)
                    hash = hash * 59 + Id.GetHashCode();

                if (FullImage != null)
                    hash = hash * 59 + FullImage.GetHashCode();

                return hash;
            }
        }
    }
}
