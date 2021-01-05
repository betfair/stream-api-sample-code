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
    public class MarketFilter : IEquatable<MarketFilter> {
        [JsonConverter(typeof(StringEnumConverter))]
        public enum BettingTypesEnum {
            [EnumMember(Value = "ODDS")]
            Odds,

            [EnumMember(Value = "LINE")]
            Line,

            [EnumMember(Value = "RANGE")]
            Range,

            [EnumMember(Value = "ASIAN_HANDICAP_DOUBLE_LINE")]
            AsianHandicapDoubleLine,

            [EnumMember(Value = "ASIAN_HANDICAP_SINGLE_LINE")]
            AsianHandicapSingleLine
        }


        /// <summary>
        ///     Initializes a new instance of the <see cref="MarketFilter" /> class.
        ///     Initializes a new instance of the <see cref="MarketFilter" />class.
        /// </summary>
        /// <param name="CountryCodes">CountryCodes.</param>
        /// <param name="BettingTypes">BettingTypes.</param>
        /// <param name="TurnInPlayEnabled">TurnInPlayEnabled.</param>
        /// <param name="MarketTypes">MarketTypes.</param>
        /// <param name="Venues">Venues.</param>
        /// <param name="MarketIds">MarketIds.</param>
        /// <param name="EventTypeIds">EventTypeIds.</param>
        /// <param name="EventIds">EventIds.</param>
        /// <param name="BspMarket">BspMarket.</param>
        public MarketFilter(
            List<string> CountryCodes = null,
            List<BettingTypesEnum?> BettingTypes = null,
            bool? TurnInPlayEnabled = null,
            List<string> MarketTypes = null,
            List<string> Venues = null,
            List<string> MarketIds = null,
            List<string> EventTypeIds = null,
            List<string> EventIds = null,
            bool? BspMarket = null) {
            this.CountryCodes = CountryCodes;
            this.BettingTypes = BettingTypes;
            this.TurnInPlayEnabled = TurnInPlayEnabled;
            this.MarketTypes = MarketTypes;
            this.Venues = Venues;
            this.MarketIds = MarketIds;
            this.EventTypeIds = EventTypeIds;
            this.EventIds = EventIds;
            this.BspMarket = BspMarket;
        }


        /// <summary>
        ///     Gets or Sets CountryCodes
        /// </summary>
        [DataMember(Name = "countryCodes", EmitDefaultValue = false)]
        public List<string> CountryCodes { get; set; }

        /// <summary>
        ///     Gets or Sets BettingTypes
        /// </summary>
        [DataMember(Name = "bettingTypes", EmitDefaultValue = false)]
        public List<BettingTypesEnum?> BettingTypes { get; set; }

        /// <summary>
        ///     Gets or Sets TurnInPlayEnabled
        /// </summary>
        [DataMember(Name = "turnInPlayEnabled", EmitDefaultValue = false)]
        public bool? TurnInPlayEnabled { get; set; }

        /// <summary>
        ///     Gets or Sets MarketTypes
        /// </summary>
        [DataMember(Name = "marketTypes", EmitDefaultValue = false)]
        public List<string> MarketTypes { get; set; }

        /// <summary>
        ///     Gets or Sets Venues
        /// </summary>
        [DataMember(Name = "venues", EmitDefaultValue = false)]
        public List<string> Venues { get; set; }

        /// <summary>
        ///     Gets or Sets MarketIds
        /// </summary>
        [DataMember(Name = "marketIds", EmitDefaultValue = false)]
        public List<string> MarketIds { get; set; }

        /// <summary>
        ///     Gets or Sets EventTypeIds
        /// </summary>
        [DataMember(Name = "eventTypeIds", EmitDefaultValue = false)]
        public List<string> EventTypeIds { get; set; }

        /// <summary>
        ///     Gets or Sets EventIds
        /// </summary>
        [DataMember(Name = "eventIds", EmitDefaultValue = false)]
        public List<string> EventIds { get; set; }

        /// <summary>
        ///     Gets or Sets BspMarket
        /// </summary>
        [DataMember(Name = "bspMarket", EmitDefaultValue = false)]
        public bool? BspMarket { get; set; }

        /// <summary>
        ///     Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString() {
            var sb = new StringBuilder();
            sb.Append("class MarketFilter {\n");
            sb.Append("  CountryCodes: ")
                .Append(CountryCodes)
                .Append("\n");
            sb.Append("  BettingTypes: ")
                .Append(BettingTypes)
                .Append("\n");
            sb.Append("  TurnInPlayEnabled: ")
                .Append(TurnInPlayEnabled)
                .Append("\n");
            sb.Append("  MarketTypes: ")
                .Append(MarketTypes)
                .Append("\n");
            sb.Append("  Venues: ")
                .Append(Venues)
                .Append("\n");
            sb.Append("  MarketIds: ")
                .Append(MarketIds)
                .Append("\n");
            sb.Append("  EventTypeIds: ")
                .Append(EventTypeIds)
                .Append("\n");
            sb.Append("  EventIds: ")
                .Append(EventIds)
                .Append("\n");
            sb.Append("  BspMarket: ")
                .Append(BspMarket)
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
            return Equals(obj as MarketFilter);
        }

        /// <summary>
        ///     Returns true if MarketFilter instances are equal
        /// </summary>
        /// <param name="other">Instance of MarketFilter to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(MarketFilter other) {
            // credit: http://stackoverflow.com/a/10454552/677735
            if (other == null)
                return false;

            return (CountryCodes == other.CountryCodes || CountryCodes != null && CountryCodes.SequenceEqual(other.CountryCodes)) &&
                   (BettingTypes == other.BettingTypes || BettingTypes != null && BettingTypes.SequenceEqual(other.BettingTypes)) &&
                   (TurnInPlayEnabled == other.TurnInPlayEnabled || TurnInPlayEnabled != null && TurnInPlayEnabled.Equals(other.TurnInPlayEnabled)) &&
                   (MarketTypes == other.MarketTypes || MarketTypes != null && MarketTypes.SequenceEqual(other.MarketTypes)) &&
                   (Venues == other.Venues || Venues != null && Venues.SequenceEqual(other.Venues)) &&
                   (MarketIds == other.MarketIds || MarketIds != null && MarketIds.SequenceEqual(other.MarketIds)) &&
                   (EventTypeIds == other.EventTypeIds || EventTypeIds != null && EventTypeIds.SequenceEqual(other.EventTypeIds)) &&
                   (EventIds == other.EventIds || EventIds != null && EventIds.SequenceEqual(other.EventIds)) &&
                   (BspMarket == other.BspMarket || BspMarket != null && BspMarket.Equals(other.BspMarket));
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

                if (CountryCodes != null)
                    hash = hash * 59 + CountryCodes.GetHashCode();

                if (BettingTypes != null)
                    hash = hash * 59 + BettingTypes.GetHashCode();

                if (TurnInPlayEnabled != null)
                    hash = hash * 59 + TurnInPlayEnabled.GetHashCode();

                if (MarketTypes != null)
                    hash = hash * 59 + MarketTypes.GetHashCode();

                if (Venues != null)
                    hash = hash * 59 + Venues.GetHashCode();

                if (MarketIds != null)
                    hash = hash * 59 + MarketIds.GetHashCode();

                if (EventTypeIds != null)
                    hash = hash * 59 + EventTypeIds.GetHashCode();

                if (EventIds != null)
                    hash = hash * 59 + EventIds.GetHashCode();

                if (BspMarket != null)
                    hash = hash * 59 + BspMarket.GetHashCode();

                return hash;
            }
        }
    }
}
