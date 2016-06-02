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
    public partial class AllRequestTypesExample :  IEquatable<AllRequestTypesExample>
    { 
    
        /// <summary>
        /// Gets or Sets OpTypes
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum OpTypesEnum {
            
            [EnumMember(Value = "heartbeat")]
            Heartbeat,
            
            [EnumMember(Value = "authentication")]
            Authentication,
            
            [EnumMember(Value = "marketSubscription")]
            Marketsubscription,
            
            [EnumMember(Value = "orderSubscription")]
            Ordersubscription
        }

    
        /// <summary>
        /// Gets or Sets OpTypes
        /// </summary>
        [DataMember(Name="opTypes", EmitDefaultValue=false)]
        public OpTypesEnum? OpTypes { get; set; }
    
        /// <summary>
        /// Initializes a new instance of the <see cref="AllRequestTypesExample" /> class.
        /// Initializes a new instance of the <see cref="AllRequestTypesExample" />class.
        /// </summary>
        /// <param name="OpTypes">OpTypes.</param>
        /// <param name="Heartbeat">Heartbeat.</param>
        /// <param name="OrderSubscriptionMessage">OrderSubscriptionMessage.</param>
        /// <param name="MarketSubscription">MarketSubscription.</param>
        /// <param name="Authentication">Authentication.</param>

        public AllRequestTypesExample(OpTypesEnum? OpTypes = null, HeartbeatMessage Heartbeat = null, OrderSubscriptionMessage OrderSubscriptionMessage = null, MarketSubscriptionMessage MarketSubscription = null, AuthenticationMessage Authentication = null)
        {
            this.OpTypes = OpTypes;
            this.Heartbeat = Heartbeat;
            this.OrderSubscriptionMessage = OrderSubscriptionMessage;
            this.MarketSubscription = MarketSubscription;
            this.Authentication = Authentication;
            
        }
        
    
        /// <summary>
        /// Gets or Sets Heartbeat
        /// </summary>
        [DataMember(Name="heartbeat", EmitDefaultValue=false)]
        public HeartbeatMessage Heartbeat { get; set; }
    
        /// <summary>
        /// Gets or Sets OrderSubscriptionMessage
        /// </summary>
        [DataMember(Name="orderSubscriptionMessage", EmitDefaultValue=false)]
        public OrderSubscriptionMessage OrderSubscriptionMessage { get; set; }
    
        /// <summary>
        /// Gets or Sets MarketSubscription
        /// </summary>
        [DataMember(Name="marketSubscription", EmitDefaultValue=false)]
        public MarketSubscriptionMessage MarketSubscription { get; set; }
    
        /// <summary>
        /// Gets or Sets Authentication
        /// </summary>
        [DataMember(Name="authentication", EmitDefaultValue=false)]
        public AuthenticationMessage Authentication { get; set; }
    
        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class AllRequestTypesExample {\n");
            sb.Append("  OpTypes: ").Append(OpTypes).Append("\n");
            sb.Append("  Heartbeat: ").Append(Heartbeat).Append("\n");
            sb.Append("  OrderSubscriptionMessage: ").Append(OrderSubscriptionMessage).Append("\n");
            sb.Append("  MarketSubscription: ").Append(MarketSubscription).Append("\n");
            sb.Append("  Authentication: ").Append(Authentication).Append("\n");
            
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
            return this.Equals(obj as AllRequestTypesExample);
        }

        /// <summary>
        /// Returns true if AllRequestTypesExample instances are equal
        /// </summary>
        /// <param name="other">Instance of AllRequestTypesExample to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(AllRequestTypesExample other)
        {
            // credit: http://stackoverflow.com/a/10454552/677735
            if (other == null)
                return false;

            return 
                (
                    this.OpTypes == other.OpTypes ||
                    this.OpTypes != null &&
                    this.OpTypes.Equals(other.OpTypes)
                ) && 
                (
                    this.Heartbeat == other.Heartbeat ||
                    this.Heartbeat != null &&
                    this.Heartbeat.Equals(other.Heartbeat)
                ) && 
                (
                    this.OrderSubscriptionMessage == other.OrderSubscriptionMessage ||
                    this.OrderSubscriptionMessage != null &&
                    this.OrderSubscriptionMessage.Equals(other.OrderSubscriptionMessage)
                ) && 
                (
                    this.MarketSubscription == other.MarketSubscription ||
                    this.MarketSubscription != null &&
                    this.MarketSubscription.Equals(other.MarketSubscription)
                ) && 
                (
                    this.Authentication == other.Authentication ||
                    this.Authentication != null &&
                    this.Authentication.Equals(other.Authentication)
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
                
                if (this.OpTypes != null)
                    hash = hash * 59 + this.OpTypes.GetHashCode();
                
                if (this.Heartbeat != null)
                    hash = hash * 59 + this.Heartbeat.GetHashCode();
                
                if (this.OrderSubscriptionMessage != null)
                    hash = hash * 59 + this.OrderSubscriptionMessage.GetHashCode();
                
                if (this.MarketSubscription != null)
                    hash = hash * 59 + this.MarketSubscription.GetHashCode();
                
                if (this.Authentication != null)
                    hash = hash * 59 + this.Authentication.GetHashCode();
                
                return hash;
            }
        }

    }
}
