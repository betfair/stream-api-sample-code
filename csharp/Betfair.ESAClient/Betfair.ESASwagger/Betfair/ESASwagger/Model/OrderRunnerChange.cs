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
    public partial class OrderRunnerChange :  IEquatable<OrderRunnerChange>
    { 
    
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderRunnerChange" /> class.
        /// Initializes a new instance of the <see cref="OrderRunnerChange" />class.
        /// </summary>
        /// <param name="Mb">Matched Backs - matched amounts by distinct matched price on the Back side for this runner (selection).</param>
        /// <param name="Uo">Unmatched Orders - orders on this runner (selection) that are not fully matched.</param>
        /// <param name="Id">Selection Id - the id of the runner (selection).</param>
        /// <param name="Hc">Handicap - the handicap of the runner (selection) (null if not applicable).</param>
        /// <param name="FullImage">FullImage.</param>
        /// <param name="Ml">Matched Lays - matched amounts by distinct matched price on the Lay side for this runner (selection).</param>

        public OrderRunnerChange(List<List<double?>> Mb = null, List<Order> Uo = null, long? Id = null, double? Hc = null, bool? FullImage = null, List<List<double?>> Ml = null)
        {
            this.Mb = Mb;
            this.Uo = Uo;
            this.Id = Id;
            this.Hc = Hc;
            this.FullImage = FullImage;
            this.Ml = Ml;
            
        }
        
    
        /// <summary>
        /// Matched Backs - matched amounts by distinct matched price on the Back side for this runner (selection)
        /// </summary>
        /// <value>Matched Backs - matched amounts by distinct matched price on the Back side for this runner (selection)</value>
        [DataMember(Name="mb", EmitDefaultValue=false)]
        public List<List<double?>> Mb { get; set; }
    
        /// <summary>
        /// Unmatched Orders - orders on this runner (selection) that are not fully matched
        /// </summary>
        /// <value>Unmatched Orders - orders on this runner (selection) that are not fully matched</value>
        [DataMember(Name="uo", EmitDefaultValue=false)]
        public List<Order> Uo { get; set; }
    
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
        /// Gets or Sets FullImage
        /// </summary>
        [DataMember(Name="fullImage", EmitDefaultValue=false)]
        public bool? FullImage { get; set; }
    
        /// <summary>
        /// Matched Lays - matched amounts by distinct matched price on the Lay side for this runner (selection)
        /// </summary>
        /// <value>Matched Lays - matched amounts by distinct matched price on the Lay side for this runner (selection)</value>
        [DataMember(Name="ml", EmitDefaultValue=false)]
        public List<List<double?>> Ml { get; set; }
    
        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class OrderRunnerChange {\n");
            sb.Append("  Mb: ").Append(Mb).Append("\n");
            sb.Append("  Uo: ").Append(Uo).Append("\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  Hc: ").Append(Hc).Append("\n");
            sb.Append("  FullImage: ").Append(FullImage).Append("\n");
            sb.Append("  Ml: ").Append(Ml).Append("\n");
            
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
            return this.Equals(obj as OrderRunnerChange);
        }

        /// <summary>
        /// Returns true if OrderRunnerChange instances are equal
        /// </summary>
        /// <param name="other">Instance of OrderRunnerChange to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(OrderRunnerChange other)
        {
            // credit: http://stackoverflow.com/a/10454552/677735
            if (other == null)
                return false;

            return 
                (
                    this.Mb == other.Mb ||
                    this.Mb != null &&
                    this.Mb.SequenceEqual(other.Mb)
                ) && 
                (
                    this.Uo == other.Uo ||
                    this.Uo != null &&
                    this.Uo.SequenceEqual(other.Uo)
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
                    this.FullImage == other.FullImage ||
                    this.FullImage != null &&
                    this.FullImage.Equals(other.FullImage)
                ) && 
                (
                    this.Ml == other.Ml ||
                    this.Ml != null &&
                    this.Ml.SequenceEqual(other.Ml)
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
                
                if (this.Mb != null)
                    hash = hash * 59 + this.Mb.GetHashCode();
                
                if (this.Uo != null)
                    hash = hash * 59 + this.Uo.GetHashCode();
                
                if (this.Id != null)
                    hash = hash * 59 + this.Id.GetHashCode();
                
                if (this.Hc != null)
                    hash = hash * 59 + this.Hc.GetHashCode();
                
                if (this.FullImage != null)
                    hash = hash * 59 + this.FullImage.GetHashCode();
                
                if (this.Ml != null)
                    hash = hash * 59 + this.Ml.GetHashCode();
                
                return hash;
            }
        }

    }
}
