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
    public partial class MarketChange :  IEquatable<MarketChange>
    { 
    
        /// <summary>
        /// Initializes a new instance of the <see cref="MarketChange" /> class.
        /// Initializes a new instance of the <see cref="MarketChange" />class.
        /// </summary>
        /// <param name="Rc">Runner Changes - a list of changes to runners (or null if un-changed).</param>
        /// <param name="Img">Image - replace existing prices / data with the data supplied: it is not a delta (or null if delta).</param>
        /// <param name="Tv">The total amount matched across the market. This value is truncated at 2dp (or null if un-changed).</param>
        /// <param name="Con">Conflated - have more than a single change been combined (or null if not conflated).</param>
        /// <param name="MarketDefinition">Market Definition - the definition of the market (or null if un-changed).</param>
        /// <param name="Id">Market Id - the id of the market.</param>

        public MarketChange(List<RunnerChange> Rc = null, bool? Img = null, double? Tv = null, bool? Con = null, MarketDefinition MarketDefinition = null, string Id = null)
        {
            this.Rc = Rc;
            this.Img = Img;
            this.Tv = Tv;
            this.Con = Con;
            this.MarketDefinition = MarketDefinition;
            this.Id = Id;
            
        }
        
    
        /// <summary>
        /// Runner Changes - a list of changes to runners (or null if un-changed)
        /// </summary>
        /// <value>Runner Changes - a list of changes to runners (or null if un-changed)</value>
        [DataMember(Name="rc", EmitDefaultValue=false)]
        public List<RunnerChange> Rc { get; set; }
    
        /// <summary>
        /// Image - replace existing prices / data with the data supplied: it is not a delta (or null if delta)
        /// </summary>
        /// <value>Image - replace existing prices / data with the data supplied: it is not a delta (or null if delta)</value>
        [DataMember(Name="img", EmitDefaultValue=false)]
        public bool? Img { get; set; }
    
        /// <summary>
        /// The total amount matched across the market. This value is truncated at 2dp (or null if un-changed)
        /// </summary>
        /// <value>The total amount matched across the market. This value is truncated at 2dp (or null if un-changed)</value>
        [DataMember(Name="tv", EmitDefaultValue=false)]
        public double? Tv { get; set; }
    
        /// <summary>
        /// Conflated - have more than a single change been combined (or null if not conflated)
        /// </summary>
        /// <value>Conflated - have more than a single change been combined (or null if not conflated)</value>
        [DataMember(Name="con", EmitDefaultValue=false)]
        public bool? Con { get; set; }
    
        /// <summary>
        /// Market Definition - the definition of the market (or null if un-changed)
        /// </summary>
        /// <value>Market Definition - the definition of the market (or null if un-changed)</value>
        [DataMember(Name="marketDefinition", EmitDefaultValue=false)]
        public MarketDefinition MarketDefinition { get; set; }
    
        /// <summary>
        /// Market Id - the id of the market
        /// </summary>
        /// <value>Market Id - the id of the market</value>
        [DataMember(Name="id", EmitDefaultValue=false)]
        public string Id { get; set; }
    
        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class MarketChange {\n");
            sb.Append("  Rc: ").Append(Rc).Append("\n");
            sb.Append("  Img: ").Append(Img).Append("\n");
            sb.Append("  Tv: ").Append(Tv).Append("\n");
            sb.Append("  Con: ").Append(Con).Append("\n");
            sb.Append("  MarketDefinition: ").Append(MarketDefinition).Append("\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            
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
            return this.Equals(obj as MarketChange);
        }

        /// <summary>
        /// Returns true if MarketChange instances are equal
        /// </summary>
        /// <param name="other">Instance of MarketChange to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(MarketChange other)
        {
            // credit: http://stackoverflow.com/a/10454552/677735
            if (other == null)
                return false;

            return 
                (
                    this.Rc == other.Rc ||
                    this.Rc != null &&
                    this.Rc.SequenceEqual(other.Rc)
                ) && 
                (
                    this.Img == other.Img ||
                    this.Img != null &&
                    this.Img.Equals(other.Img)
                ) && 
                (
                    this.Tv == other.Tv ||
                    this.Tv != null &&
                    this.Tv.Equals(other.Tv)
                ) && 
                (
                    this.Con == other.Con ||
                    this.Con != null &&
                    this.Con.Equals(other.Con)
                ) && 
                (
                    this.MarketDefinition == other.MarketDefinition ||
                    this.MarketDefinition != null &&
                    this.MarketDefinition.Equals(other.MarketDefinition)
                ) && 
                (
                    this.Id == other.Id ||
                    this.Id != null &&
                    this.Id.Equals(other.Id)
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
                
                if (this.Rc != null)
                    hash = hash * 59 + this.Rc.GetHashCode();
                
                if (this.Img != null)
                    hash = hash * 59 + this.Img.GetHashCode();
                
                if (this.Tv != null)
                    hash = hash * 59 + this.Tv.GetHashCode();
                
                if (this.Con != null)
                    hash = hash * 59 + this.Con.GetHashCode();
                
                if (this.MarketDefinition != null)
                    hash = hash * 59 + this.MarketDefinition.GetHashCode();
                
                if (this.Id != null)
                    hash = hash * 59 + this.Id.GetHashCode();
                
                return hash;
            }
        }

    }
}
