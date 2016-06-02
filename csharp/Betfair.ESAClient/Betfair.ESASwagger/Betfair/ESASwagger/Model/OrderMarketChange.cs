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
    public partial class OrderMarketChange :  IEquatable<OrderMarketChange>
    { 
    
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderMarketChange" /> class.
        /// Initializes a new instance of the <see cref="OrderMarketChange" />class.
        /// </summary>
        /// <param name="AccountId">AccountId.</param>
        /// <param name="Orc">Order Changes - a list of changes to orders on a selection.</param>
        /// <param name="Closed">Closed.</param>
        /// <param name="Id">Market Id - the id of the market the order is on.</param>

        public OrderMarketChange(long? AccountId = null, List<OrderRunnerChange> Orc = null, bool? Closed = null, string Id = null)
        {
            this.AccountId = AccountId;
            this.Orc = Orc;
            this.Closed = Closed;
            this.Id = Id;
            
        }
        
    
        /// <summary>
        /// Gets or Sets AccountId
        /// </summary>
        [DataMember(Name="accountId", EmitDefaultValue=false)]
        public long? AccountId { get; set; }
    
        /// <summary>
        /// Order Changes - a list of changes to orders on a selection
        /// </summary>
        /// <value>Order Changes - a list of changes to orders on a selection</value>
        [DataMember(Name="orc", EmitDefaultValue=false)]
        public List<OrderRunnerChange> Orc { get; set; }
    
        /// <summary>
        /// Gets or Sets Closed
        /// </summary>
        [DataMember(Name="closed", EmitDefaultValue=false)]
        public bool? Closed { get; set; }
    
        /// <summary>
        /// Market Id - the id of the market the order is on
        /// </summary>
        /// <value>Market Id - the id of the market the order is on</value>
        [DataMember(Name="id", EmitDefaultValue=false)]
        public string Id { get; set; }
    
        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class OrderMarketChange {\n");
            sb.Append("  AccountId: ").Append(AccountId).Append("\n");
            sb.Append("  Orc: ").Append(Orc).Append("\n");
            sb.Append("  Closed: ").Append(Closed).Append("\n");
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
            return this.Equals(obj as OrderMarketChange);
        }

        /// <summary>
        /// Returns true if OrderMarketChange instances are equal
        /// </summary>
        /// <param name="other">Instance of OrderMarketChange to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(OrderMarketChange other)
        {
            // credit: http://stackoverflow.com/a/10454552/677735
            if (other == null)
                return false;

            return 
                (
                    this.AccountId == other.AccountId ||
                    this.AccountId != null &&
                    this.AccountId.Equals(other.AccountId)
                ) && 
                (
                    this.Orc == other.Orc ||
                    this.Orc != null &&
                    this.Orc.SequenceEqual(other.Orc)
                ) && 
                (
                    this.Closed == other.Closed ||
                    this.Closed != null &&
                    this.Closed.Equals(other.Closed)
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
                
                if (this.AccountId != null)
                    hash = hash * 59 + this.AccountId.GetHashCode();
                
                if (this.Orc != null)
                    hash = hash * 59 + this.Orc.GetHashCode();
                
                if (this.Closed != null)
                    hash = hash * 59 + this.Closed.GetHashCode();
                
                if (this.Id != null)
                    hash = hash * 59 + this.Id.GetHashCode();
                
                return hash;
            }
        }

    }
}
