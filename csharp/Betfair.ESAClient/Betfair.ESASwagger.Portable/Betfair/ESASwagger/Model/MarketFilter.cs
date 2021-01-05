using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Betfair.ESASwagger.Model
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class MarketFilter :  IEquatable<MarketFilter>
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public enum BettingTypesEnum
        {
            
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
        /// Initializes a new instance of the <see cref="MarketFilter" /> class.
        /// Initializes a new instance of the <see cref="MarketFilter" />class.
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

        public MarketFilter(List<string> CountryCodes = null, List<BettingTypesEnum?> BettingTypes = null, bool? TurnInPlayEnabled = null, List<string> MarketTypes = null, List<string> Venues = null, List<string> MarketIds = null, List<string> EventTypeIds = null, List<string> EventIds = null, bool? BspMarket = null)
        {
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
        /// Gets or Sets CountryCodes
        /// </summary>
        [DataMember(Name="countryCodes", EmitDefaultValue=false)]
        public List<string> CountryCodes { get; set; }
    
        /// <summary>
        /// Gets or Sets BettingTypes
        /// </summary>
        [DataMember(Name="bettingTypes", EmitDefaultValue=false)]
        public List<BettingTypesEnum?> BettingTypes { get; set; }
    
        /// <summary>
        /// Gets or Sets TurnInPlayEnabled
        /// </summary>
        [DataMember(Name="turnInPlayEnabled", EmitDefaultValue=false)]
        public bool? TurnInPlayEnabled { get; set; }
    
        /// <summary>
        /// Gets or Sets MarketTypes
        /// </summary>
        [DataMember(Name="marketTypes", EmitDefaultValue=false)]
        public List<string> MarketTypes { get; set; }
    
        /// <summary>
        /// Gets or Sets Venues
        /// </summary>
        [DataMember(Name="venues", EmitDefaultValue=false)]
        public List<string> Venues { get; set; }
    
        /// <summary>
        /// Gets or Sets MarketIds
        /// </summary>
        [DataMember(Name="marketIds", EmitDefaultValue=false)]
        public List<string> MarketIds { get; set; }
    
        /// <summary>
        /// Gets or Sets EventTypeIds
        /// </summary>
        [DataMember(Name="eventTypeIds", EmitDefaultValue=false)]
        public List<string> EventTypeIds { get; set; }
    
        /// <summary>
        /// Gets or Sets EventIds
        /// </summary>
        [DataMember(Name="eventIds", EmitDefaultValue=false)]
        public List<string> EventIds { get; set; }
    
        /// <summary>
        /// Gets or Sets BspMarket
        /// </summary>
        [DataMember(Name="bspMarket", EmitDefaultValue=false)]
        public bool? BspMarket { get; set; }
    
        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class MarketFilter {\n");
            sb.Append("  CountryCodes: ").Append(CountryCodes).Append("\n");
            sb.Append("  BettingTypes: ").Append(BettingTypes).Append("\n");
            sb.Append("  TurnInPlayEnabled: ").Append(TurnInPlayEnabled).Append("\n");
            sb.Append("  MarketTypes: ").Append(MarketTypes).Append("\n");
            sb.Append("  Venues: ").Append(Venues).Append("\n");
            sb.Append("  MarketIds: ").Append(MarketIds).Append("\n");
            sb.Append("  EventTypeIds: ").Append(EventTypeIds).Append("\n");
            sb.Append("  EventIds: ").Append(EventIds).Append("\n");
            sb.Append("  BspMarket: ").Append(BspMarket).Append("\n");
            
            sb.Append("}\n");
            return sb.ToString();
        }
  
        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="obj">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object obj)
        {
            // credit: http://stackoverflow.com/a/10454552/677735
            return this.Equals(obj as MarketFilter);
        }

        /// <summary>
        /// Returns true if MarketFilter instances are equal
        /// </summary>
        /// <param name="other">Instance of MarketFilter to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(MarketFilter other)
        {
            // credit: http://stackoverflow.com/a/10454552/677735
            if (other == null)
                return false;

            return 
                (
                    this.CountryCodes == other.CountryCodes ||
                    this.CountryCodes != null &&
                    this.CountryCodes.SequenceEqual(other.CountryCodes)
                ) && 
                (
                    this.BettingTypes == other.BettingTypes ||
                    this.BettingTypes != null &&
                    this.BettingTypes.SequenceEqual(other.BettingTypes)
                ) && 
                (
                    this.TurnInPlayEnabled == other.TurnInPlayEnabled ||
                    this.TurnInPlayEnabled != null &&
                    this.TurnInPlayEnabled.Equals(other.TurnInPlayEnabled)
                ) && 
                (
                    this.MarketTypes == other.MarketTypes ||
                    this.MarketTypes != null &&
                    this.MarketTypes.SequenceEqual(other.MarketTypes)
                ) && 
                (
                    this.Venues == other.Venues ||
                    this.Venues != null &&
                    this.Venues.SequenceEqual(other.Venues)
                ) && 
                (
                    this.MarketIds == other.MarketIds ||
                    this.MarketIds != null &&
                    this.MarketIds.SequenceEqual(other.MarketIds)
                ) && 
                (
                    this.EventTypeIds == other.EventTypeIds ||
                    this.EventTypeIds != null &&
                    this.EventTypeIds.SequenceEqual(other.EventTypeIds)
                ) && 
                (
                    this.EventIds == other.EventIds ||
                    this.EventIds != null &&
                    this.EventIds.SequenceEqual(other.EventIds)
                ) && 
                (
                    this.BspMarket == other.BspMarket ||
                    this.BspMarket != null &&
                    this.BspMarket.Equals(other.BspMarket)
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            // credit: http://stackoverflow.com/a/263416/677735
            unchecked // Overflow is fine, just wrap
            {
                int hash = 41;
                // Suitable nullity checks etc, of course :)
                
                if (this.CountryCodes != null)
                    hash = hash * 59 + this.CountryCodes.GetHashCode();
                
                if (this.BettingTypes != null)
                    hash = hash * 59 + this.BettingTypes.GetHashCode();
                
                if (this.TurnInPlayEnabled != null)
                    hash = hash * 59 + this.TurnInPlayEnabled.GetHashCode();
                
                if (this.MarketTypes != null)
                    hash = hash * 59 + this.MarketTypes.GetHashCode();
                
                if (this.Venues != null)
                    hash = hash * 59 + this.Venues.GetHashCode();
                
                if (this.MarketIds != null)
                    hash = hash * 59 + this.MarketIds.GetHashCode();
                
                if (this.EventTypeIds != null)
                    hash = hash * 59 + this.EventTypeIds.GetHashCode();
                
                if (this.EventIds != null)
                    hash = hash * 59 + this.EventIds.GetHashCode();
                
                if (this.BspMarket != null)
                    hash = hash * 59 + this.BspMarket.GetHashCode();
                
                return hash;
            }
        }

    }
}
