using System;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Betfair.ESASwagger.Model {
    /// <summary>
    /// </summary>
    [DataContract]
    public class AllResponseTypesExample : IEquatable<AllResponseTypesExample> {
        /// <summary>
        ///     Gets or Sets OpTypes
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum OpTypesEnum {
            [EnumMember(Value = "connection")]
            Connection,

            [EnumMember(Value = "status")]
            Status,

            [EnumMember(Value = "mcm")]
            Mcm,

            [EnumMember(Value = "ocm")]
            Ocm
        }


        /// <summary>
        ///     Gets or Sets OpTypes
        /// </summary>
        [DataMember(Name = "opTypes", EmitDefaultValue = false)]
        public OpTypesEnum? OpTypes { get; set; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="AllResponseTypesExample" /> class.
        ///     Initializes a new instance of the <see cref="AllResponseTypesExample" />class.
        /// </summary>
        /// <param name="OpTypes">OpTypes.</param>
        /// <param name="MarketChangeMessage">MarketChangeMessage.</param>
        /// <param name="Connection">Connection.</param>
        /// <param name="OrderChangeMessage">OrderChangeMessage.</param>
        /// <param name="Status">Status.</param>
        public AllResponseTypesExample(
            OpTypesEnum? OpTypes = null,
            MarketChangeMessage MarketChangeMessage = null,
            ConnectionMessage Connection = null,
            OrderChangeMessage OrderChangeMessage = null,
            StatusMessage Status = null) {
            this.OpTypes = OpTypes;
            this.MarketChangeMessage = MarketChangeMessage;
            this.Connection = Connection;
            this.OrderChangeMessage = OrderChangeMessage;
            this.Status = Status;
        }


        /// <summary>
        ///     Gets or Sets MarketChangeMessage
        /// </summary>
        [DataMember(Name = "marketChangeMessage", EmitDefaultValue = false)]
        public MarketChangeMessage MarketChangeMessage { get; set; }

        /// <summary>
        ///     Gets or Sets Connection
        /// </summary>
        [DataMember(Name = "connection", EmitDefaultValue = false)]
        public ConnectionMessage Connection { get; set; }

        /// <summary>
        ///     Gets or Sets OrderChangeMessage
        /// </summary>
        [DataMember(Name = "orderChangeMessage", EmitDefaultValue = false)]
        public OrderChangeMessage OrderChangeMessage { get; set; }

        /// <summary>
        ///     Gets or Sets Status
        /// </summary>
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public StatusMessage Status { get; set; }

        /// <summary>
        ///     Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString() {
            var sb = new StringBuilder();
            sb.Append("class AllResponseTypesExample {\n");
            sb.Append("  OpTypes: ")
                .Append(OpTypes)
                .Append("\n");
            sb.Append("  MarketChangeMessage: ")
                .Append(MarketChangeMessage)
                .Append("\n");
            sb.Append("  Connection: ")
                .Append(Connection)
                .Append("\n");
            sb.Append("  OrderChangeMessage: ")
                .Append(OrderChangeMessage)
                .Append("\n");
            sb.Append("  Status: ")
                .Append(Status)
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
            return Equals(obj as AllResponseTypesExample);
        }

        /// <summary>
        ///     Returns true if AllResponseTypesExample instances are equal
        /// </summary>
        /// <param name="other">Instance of AllResponseTypesExample to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(AllResponseTypesExample other) {
            // credit: http://stackoverflow.com/a/10454552/677735
            if (other == null)
                return false;

            return (OpTypes == other.OpTypes || OpTypes != null && OpTypes.Equals(other.OpTypes)) &&
                   (MarketChangeMessage == other.MarketChangeMessage || MarketChangeMessage != null && MarketChangeMessage.Equals(other.MarketChangeMessage)) &&
                   (Connection == other.Connection || Connection != null && Connection.Equals(other.Connection)) &&
                   (OrderChangeMessage == other.OrderChangeMessage || OrderChangeMessage != null && OrderChangeMessage.Equals(other.OrderChangeMessage)) &&
                   (Status == other.Status || Status != null && Status.Equals(other.Status));
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

                if (OpTypes != null)
                    hash = hash * 59 + OpTypes.GetHashCode();

                if (MarketChangeMessage != null)
                    hash = hash * 59 + MarketChangeMessage.GetHashCode();

                if (Connection != null)
                    hash = hash * 59 + Connection.GetHashCode();

                if (OrderChangeMessage != null)
                    hash = hash * 59 + OrderChangeMessage.GetHashCode();

                if (Status != null)
                    hash = hash * 59 + Status.GetHashCode();

                return hash;
            }
        }
    }
}
