using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Betfair.ESASwagger.Model {
    /// <summary>
    /// </summary>
    [DataContract]
    public class MarketDataFilter : IEquatable<MarketDataFilter> {
        [JsonConverter(typeof(StringEnumConverter))]
        public enum FieldsEnum {
            [EnumMember(Value = "EX_BEST_OFFERS_DISP")]
            ExBestOffersDisp,

            [EnumMember(Value = "EX_BEST_OFFERS")]
            ExBestOffers,

            [EnumMember(Value = "EX_ALL_OFFERS")]
            ExAllOffers,

            [EnumMember(Value = "EX_TRADED")]
            ExTraded,

            [EnumMember(Value = "EX_TRADED_VOL")]
            ExTradedVol,

            [EnumMember(Value = "EX_LTP")]
            ExLtp,

            [EnumMember(Value = "EX_MARKET_DEF")]
            ExMarketDef,

            [EnumMember(Value = "SP_TRADED")]
            SpTraded,

            [EnumMember(Value = "SP_PROJECTED")]
            SpProjected
        }


        /// <summary>
        ///     Initializes a new instance of the <see cref="MarketDataFilter" /> class.
        ///     Initializes a new instance of the <see cref="MarketDataFilter" />class.
        /// </summary>
        /// <param name="LadderLevels">LadderLevels.</param>
        /// <param name="Fields">Fields.</param>
        public MarketDataFilter(int? LadderLevels = null, List<FieldsEnum?> Fields = null) {
            this.LadderLevels = LadderLevels;
            this.Fields = Fields;
        }


        /// <summary>
        ///     Gets or Sets LadderLevels
        /// </summary>
        [DataMember(Name = "ladderLevels", EmitDefaultValue = false)]
        public int? LadderLevels { get; set; }

        /// <summary>
        ///     Gets or Sets Fields
        /// </summary>
        [DataMember(Name = "fields", EmitDefaultValue = false)]
        public List<FieldsEnum?> Fields { get; set; }

        /// <summary>
        ///     Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString() {
            var sb = new StringBuilder();
            sb.Append("class MarketDataFilter {\n");
            sb.Append("  LadderLevels: ")
                .Append(LadderLevels)
                .Append("\n");
            sb.Append("  Fields: ")
                .Append(Fields)
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
            return Equals(obj as MarketDataFilter);
        }

        /// <summary>
        ///     Returns true if MarketDataFilter instances are equal
        /// </summary>
        /// <param name="other">Instance of MarketDataFilter to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(MarketDataFilter other) {
            // credit: http://stackoverflow.com/a/10454552/677735
            if (other == null)
                return false;

            return (LadderLevels == other.LadderLevels || LadderLevels != null && LadderLevels.Equals(other.LadderLevels)) &&
                   (Fields == other.Fields || Fields != null && Fields.SequenceEqual(other.Fields));
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

                if (LadderLevels != null)
                    hash = hash * 59 + LadderLevels.GetHashCode();

                if (Fields != null)
                    hash = hash * 59 + Fields.GetHashCode();

                return hash;
            }
        }
    }
}
