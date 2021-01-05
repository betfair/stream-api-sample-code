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
    public partial class RunnerChange :  IEquatable<RunnerChange>
    { 
    
        /// <summary>
        /// Initializes a new instance of the <see cref="RunnerChange" /> class.
        /// Initializes a new instance of the <see cref="RunnerChange" />class.
        /// </summary>
        /// <param name="Tv">The total amount matched. This value is truncated at 2dp..</param>
        /// <param name="Batb">Best Available To Back - LevelPriceVol triple delta of price changes, keyed by level (0 vol is remove).</param>
        /// <param name="Spb">Starting Price Back - PriceVol tuple delta of price changes (0 vol is remove).</param>
        /// <param name="Bdatl">Best Display Available To Lay (includes virtual prices)- LevelPriceVol triple delta of price changes, keyed by level (0 vol is remove).</param>
        /// <param name="Trd">Traded - PriceVol tuple delta of price changes (0 vol is remove).</param>
        /// <param name="Spf">Starting Price Far - The far starting price (or null if un-changed).</param>
        /// <param name="Ltp">Last Traded Price - The last traded price (or null if un-changed).</param>
        /// <param name="Atb">Available To Back - PriceVol tuple delta of price changes (0 vol is remove).</param>
        /// <param name="Spl">Starting Price Lay - PriceVol tuple delta of price changes (0 vol is remove).</param>
        /// <param name="Spn">Starting Price Near - The far starting price (or null if un-changed).</param>
        /// <param name="Atl">Available To Lay - PriceVol tuple delta of price changes (0 vol is remove).</param>
        /// <param name="Batl">Best Available To Lay - LevelPriceVol triple delta of price changes, keyed by level (0 vol is remove).</param>
        /// <param name="Id">Selection Id - the id of the runner (selection).</param>
        /// <param name="Hc">Handicap - the handicap of the runner (selection) (null if not applicable).</param>
        /// <param name="Bdatb">Best Display Available To Back (includes virtual prices)- LevelPriceVol triple delta of price changes, keyed by level (0 vol is remove).</param>

        public RunnerChange(double? Tv = null, List<List<double?>> Batb = null, List<List<double?>> Spb = null, List<List<double?>> Bdatl = null, List<List<double?>> Trd = null, double? Spf = null, double? Ltp = null, List<List<double?>> Atb = null, List<List<double?>> Spl = null, double? Spn = null, List<List<double?>> Atl = null, List<List<double?>> Batl = null, long? Id = null, double? Hc = null, List<List<double?>> Bdatb = null)
        {
            this.Tv = Tv;
            this.Batb = Batb;
            this.Spb = Spb;
            this.Bdatl = Bdatl;
            this.Trd = Trd;
            this.Spf = Spf;
            this.Ltp = Ltp;
            this.Atb = Atb;
            this.Spl = Spl;
            this.Spn = Spn;
            this.Atl = Atl;
            this.Batl = Batl;
            this.Id = Id;
            this.Hc = Hc;
            this.Bdatb = Bdatb;
            
        }
        
    
        /// <summary>
        /// The total amount matched. This value is truncated at 2dp.
        /// </summary>
        /// <value>The total amount matched. This value is truncated at 2dp.</value>
        [DataMember(Name="tv", EmitDefaultValue=false)]
        public double? Tv { get; set; }
    
        /// <summary>
        /// Best Available To Back - LevelPriceVol triple delta of price changes, keyed by level (0 vol is remove)
        /// </summary>
        /// <value>Best Available To Back - LevelPriceVol triple delta of price changes, keyed by level (0 vol is remove)</value>
        [DataMember(Name="batb", EmitDefaultValue=false)]
        public List<List<double?>> Batb { get; set; }
    
        /// <summary>
        /// Starting Price Back - PriceVol tuple delta of price changes (0 vol is remove)
        /// </summary>
        /// <value>Starting Price Back - PriceVol tuple delta of price changes (0 vol is remove)</value>
        [DataMember(Name="spb", EmitDefaultValue=false)]
        public List<List<double?>> Spb { get; set; }
    
        /// <summary>
        /// Best Display Available To Lay (includes virtual prices)- LevelPriceVol triple delta of price changes, keyed by level (0 vol is remove)
        /// </summary>
        /// <value>Best Display Available To Lay (includes virtual prices)- LevelPriceVol triple delta of price changes, keyed by level (0 vol is remove)</value>
        [DataMember(Name="bdatl", EmitDefaultValue=false)]
        public List<List<double?>> Bdatl { get; set; }
    
        /// <summary>
        /// Traded - PriceVol tuple delta of price changes (0 vol is remove)
        /// </summary>
        /// <value>Traded - PriceVol tuple delta of price changes (0 vol is remove)</value>
        [DataMember(Name="trd", EmitDefaultValue=false)]
        public List<List<double?>> Trd { get; set; }
    
        /// <summary>
        /// Starting Price Far - The far starting price (or null if un-changed)
        /// </summary>
        /// <value>Starting Price Far - The far starting price (or null if un-changed)</value>
        [DataMember(Name="spf", EmitDefaultValue=false)]
        public double? Spf { get; set; }
    
        /// <summary>
        /// Last Traded Price - The last traded price (or null if un-changed)
        /// </summary>
        /// <value>Last Traded Price - The last traded price (or null if un-changed)</value>
        [DataMember(Name="ltp", EmitDefaultValue=false)]
        public double? Ltp { get; set; }
    
        /// <summary>
        /// Available To Back - PriceVol tuple delta of price changes (0 vol is remove)
        /// </summary>
        /// <value>Available To Back - PriceVol tuple delta of price changes (0 vol is remove)</value>
        [DataMember(Name="atb", EmitDefaultValue=false)]
        public List<List<double?>> Atb { get; set; }
    
        /// <summary>
        /// Starting Price Lay - PriceVol tuple delta of price changes (0 vol is remove)
        /// </summary>
        /// <value>Starting Price Lay - PriceVol tuple delta of price changes (0 vol is remove)</value>
        [DataMember(Name="spl", EmitDefaultValue=false)]
        public List<List<double?>> Spl { get; set; }
    
        /// <summary>
        /// Starting Price Near - The far starting price (or null if un-changed)
        /// </summary>
        /// <value>Starting Price Near - The far starting price (or null if un-changed)</value>
        [DataMember(Name="spn", EmitDefaultValue=false)]
        public double? Spn { get; set; }
    
        /// <summary>
        /// Available To Lay - PriceVol tuple delta of price changes (0 vol is remove)
        /// </summary>
        /// <value>Available To Lay - PriceVol tuple delta of price changes (0 vol is remove)</value>
        [DataMember(Name="atl", EmitDefaultValue=false)]
        public List<List<double?>> Atl { get; set; }
    
        /// <summary>
        /// Best Available To Lay - LevelPriceVol triple delta of price changes, keyed by level (0 vol is remove)
        /// </summary>
        /// <value>Best Available To Lay - LevelPriceVol triple delta of price changes, keyed by level (0 vol is remove)</value>
        [DataMember(Name="batl", EmitDefaultValue=false)]
        public List<List<double?>> Batl { get; set; }
    
        /// <summary>
        /// Selection Id - the id of the runner (selection)
        /// </summary>
        /// <value>Selection Id - the id of the runner (selection)</value>
        [DataMember(Name="id", EmitDefaultValue=false)]
        public long? Id { get; set; }
    
        /// <summary>
        /// Handicap - the handicap of the runner (selection) (null if not applicable)
        /// </summary>
        /// <value>Handicap - the handicap of the runner (selection) (null if not applicable)</value>
        [DataMember(Name="hc", EmitDefaultValue=false)]
        public double? Hc { get; set; }
    
        /// <summary>
        /// Best Display Available To Back (includes virtual prices)- LevelPriceVol triple delta of price changes, keyed by level (0 vol is remove)
        /// </summary>
        /// <value>Best Display Available To Back (includes virtual prices)- LevelPriceVol triple delta of price changes, keyed by level (0 vol is remove)</value>
        [DataMember(Name="bdatb", EmitDefaultValue=false)]
        public List<List<double?>> Bdatb { get; set; }
    
        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class RunnerChange {\n");
            sb.Append("  Tv: ").Append(Tv).Append("\n");
            sb.Append("  Batb: ").Append(Batb).Append("\n");
            sb.Append("  Spb: ").Append(Spb).Append("\n");
            sb.Append("  Bdatl: ").Append(Bdatl).Append("\n");
            sb.Append("  Trd: ").Append(Trd).Append("\n");
            sb.Append("  Spf: ").Append(Spf).Append("\n");
            sb.Append("  Ltp: ").Append(Ltp).Append("\n");
            sb.Append("  Atb: ").Append(Atb).Append("\n");
            sb.Append("  Spl: ").Append(Spl).Append("\n");
            sb.Append("  Spn: ").Append(Spn).Append("\n");
            sb.Append("  Atl: ").Append(Atl).Append("\n");
            sb.Append("  Batl: ").Append(Batl).Append("\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  Hc: ").Append(Hc).Append("\n");
            sb.Append("  Bdatb: ").Append(Bdatb).Append("\n");
            
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
            return this.Equals(obj as RunnerChange);
        }

        /// <summary>
        /// Returns true if RunnerChange instances are equal
        /// </summary>
        /// <param name="other">Instance of RunnerChange to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(RunnerChange other)
        {
            // credit: http://stackoverflow.com/a/10454552/677735
            if (other == null)
                return false;

            return 
                (
                    this.Tv == other.Tv ||
                    this.Tv != null &&
                    this.Tv.Equals(other.Tv)
                ) && 
                (
                    this.Batb == other.Batb ||
                    this.Batb != null &&
                    this.Batb.SequenceEqual(other.Batb)
                ) && 
                (
                    this.Spb == other.Spb ||
                    this.Spb != null &&
                    this.Spb.SequenceEqual(other.Spb)
                ) && 
                (
                    this.Bdatl == other.Bdatl ||
                    this.Bdatl != null &&
                    this.Bdatl.SequenceEqual(other.Bdatl)
                ) && 
                (
                    this.Trd == other.Trd ||
                    this.Trd != null &&
                    this.Trd.SequenceEqual(other.Trd)
                ) && 
                (
                    this.Spf == other.Spf ||
                    this.Spf != null &&
                    this.Spf.Equals(other.Spf)
                ) && 
                (
                    this.Ltp == other.Ltp ||
                    this.Ltp != null &&
                    this.Ltp.Equals(other.Ltp)
                ) && 
                (
                    this.Atb == other.Atb ||
                    this.Atb != null &&
                    this.Atb.SequenceEqual(other.Atb)
                ) && 
                (
                    this.Spl == other.Spl ||
                    this.Spl != null &&
                    this.Spl.SequenceEqual(other.Spl)
                ) && 
                (
                    this.Spn == other.Spn ||
                    this.Spn != null &&
                    this.Spn.Equals(other.Spn)
                ) && 
                (
                    this.Atl == other.Atl ||
                    this.Atl != null &&
                    this.Atl.SequenceEqual(other.Atl)
                ) && 
                (
                    this.Batl == other.Batl ||
                    this.Batl != null &&
                    this.Batl.SequenceEqual(other.Batl)
                ) && 
                (
                    this.Id == other.Id ||
                    this.Id != null &&
                    this.Id.Equals(other.Id)
                ) && 
                (
                    this.Hc == other.Hc ||
                    this.Hc != null &&
                    this.Hc.Equals(other.Hc)
                ) && 
                (
                    this.Bdatb == other.Bdatb ||
                    this.Bdatb != null &&
                    this.Bdatb.SequenceEqual(other.Bdatb)
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
                
                if (this.Tv != null)
                    hash = hash * 59 + this.Tv.GetHashCode();
                
                if (this.Batb != null)
                    hash = hash * 59 + this.Batb.GetHashCode();
                
                if (this.Spb != null)
                    hash = hash * 59 + this.Spb.GetHashCode();
                
                if (this.Bdatl != null)
                    hash = hash * 59 + this.Bdatl.GetHashCode();
                
                if (this.Trd != null)
                    hash = hash * 59 + this.Trd.GetHashCode();
                
                if (this.Spf != null)
                    hash = hash * 59 + this.Spf.GetHashCode();
                
                if (this.Ltp != null)
                    hash = hash * 59 + this.Ltp.GetHashCode();
                
                if (this.Atb != null)
                    hash = hash * 59 + this.Atb.GetHashCode();
                
                if (this.Spl != null)
                    hash = hash * 59 + this.Spl.GetHashCode();
                
                if (this.Spn != null)
                    hash = hash * 59 + this.Spn.GetHashCode();
                
                if (this.Atl != null)
                    hash = hash * 59 + this.Atl.GetHashCode();
                
                if (this.Batl != null)
                    hash = hash * 59 + this.Batl.GetHashCode();
                
                if (this.Id != null)
                    hash = hash * 59 + this.Id.GetHashCode();
                
                if (this.Hc != null)
                    hash = hash * 59 + this.Hc.GetHashCode();
                
                if (this.Bdatb != null)
                    hash = hash * 59 + this.Bdatb.GetHashCode();
                
                return hash;
            }
        }

    }
}
