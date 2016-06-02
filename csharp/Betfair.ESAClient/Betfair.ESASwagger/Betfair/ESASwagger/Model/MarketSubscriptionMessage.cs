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
    public partial class MarketSubscriptionMessage : RequestMessage,  IEquatable<MarketSubscriptionMessage>
    { 
    
        /// <summary>
        /// Initializes a new instance of the <see cref="MarketSubscriptionMessage" /> class.
        /// Initializes a new instance of the <see cref="MarketSubscriptionMessage" />class.
        /// </summary>
        /// <param name="Op">The operation type.</param>
        /// <param name="Id">Client generated unique id to link request with response (like json rpc).</param>
        /// <param name="SegmentationEnabled">Segmentation Enabled - allow the server to send large sets of data in segments, instead of a single block.</param>
        /// <param name="Clk">Token value delta (received in MarketChangeMessage) that should be passed to resume a subscription.</param>
        /// <param name="HeartbeatMs">Heartbeat Milliseconds - the heartbeat rate (looped back on initial image after validation: bounds are 500 to 30000).</param>
        /// <param name="InitialClk">Token value (received in initial MarketChangeMessage) that should be passed to resume a subscription.</param>
        /// <param name="MarketFilter">MarketFilter.</param>
        /// <param name="ConflateMs">Conflate Milliseconds - the conflation rate (looped back on initial image after validation: bounds are 0 to 120000).</param>
        /// <param name="MarketDataFilter">MarketDataFilter.</param>

        public MarketSubscriptionMessage(string Op = null, int? Id = null, bool? SegmentationEnabled = null, string Clk = null, long? HeartbeatMs = null, string InitialClk = null, MarketFilter MarketFilter = null, long? ConflateMs = null, MarketDataFilter MarketDataFilter = null)
        {
            this.Op = Op;
            this.Id = Id;
            this.SegmentationEnabled = SegmentationEnabled;
            this.Clk = Clk;
            this.HeartbeatMs = HeartbeatMs;
            this.InitialClk = InitialClk;
            this.MarketFilter = MarketFilter;
            this.ConflateMs = ConflateMs;
            this.MarketDataFilter = MarketDataFilter;
            
        }
        
    
        /// <summary>
        /// The operation type
        /// </summary>
        /// <value>The operation type</value>
        [DataMember(Name="op", EmitDefaultValue=false)]
        public string Op { get; set; }
    
        /// <summary>
        /// Client generated unique id to link request with response (like json rpc)
        /// </summary>
        /// <value>Client generated unique id to link request with response (like json rpc)</value>
        [DataMember(Name="id", EmitDefaultValue=false)]
        public int? Id { get; set; }
    
        /// <summary>
        /// Segmentation Enabled - allow the server to send large sets of data in segments, instead of a single block
        /// </summary>
        /// <value>Segmentation Enabled - allow the server to send large sets of data in segments, instead of a single block</value>
        [DataMember(Name="segmentationEnabled", EmitDefaultValue=false)]
        public bool? SegmentationEnabled { get; set; }
    
        /// <summary>
        /// Token value delta (received in MarketChangeMessage) that should be passed to resume a subscription
        /// </summary>
        /// <value>Token value delta (received in MarketChangeMessage) that should be passed to resume a subscription</value>
        [DataMember(Name="clk", EmitDefaultValue=false)]
        public string Clk { get; set; }
    
        /// <summary>
        /// Heartbeat Milliseconds - the heartbeat rate (looped back on initial image after validation: bounds are 500 to 30000)
        /// </summary>
        /// <value>Heartbeat Milliseconds - the heartbeat rate (looped back on initial image after validation: bounds are 500 to 30000)</value>
        [DataMember(Name="heartbeatMs", EmitDefaultValue=false)]
        public long? HeartbeatMs { get; set; }
    
        /// <summary>
        /// Token value (received in initial MarketChangeMessage) that should be passed to resume a subscription
        /// </summary>
        /// <value>Token value (received in initial MarketChangeMessage) that should be passed to resume a subscription</value>
        [DataMember(Name="initialClk", EmitDefaultValue=false)]
        public string InitialClk { get; set; }
    
        /// <summary>
        /// Gets or Sets MarketFilter
        /// </summary>
        [DataMember(Name="marketFilter", EmitDefaultValue=false)]
        public MarketFilter MarketFilter { get; set; }
    
        /// <summary>
        /// Conflate Milliseconds - the conflation rate (looped back on initial image after validation: bounds are 0 to 120000)
        /// </summary>
        /// <value>Conflate Milliseconds - the conflation rate (looped back on initial image after validation: bounds are 0 to 120000)</value>
        [DataMember(Name="conflateMs", EmitDefaultValue=false)]
        public long? ConflateMs { get; set; }
    
        /// <summary>
        /// Gets or Sets MarketDataFilter
        /// </summary>
        [DataMember(Name="marketDataFilter", EmitDefaultValue=false)]
        public MarketDataFilter MarketDataFilter { get; set; }
    
        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class MarketSubscriptionMessage {\n");
            sb.Append("  Op: ").Append(Op).Append("\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  SegmentationEnabled: ").Append(SegmentationEnabled).Append("\n");
            sb.Append("  Clk: ").Append(Clk).Append("\n");
            sb.Append("  HeartbeatMs: ").Append(HeartbeatMs).Append("\n");
            sb.Append("  InitialClk: ").Append(InitialClk).Append("\n");
            sb.Append("  MarketFilter: ").Append(MarketFilter).Append("\n");
            sb.Append("  ConflateMs: ").Append(ConflateMs).Append("\n");
            sb.Append("  MarketDataFilter: ").Append(MarketDataFilter).Append("\n");
            
            sb.Append("}\n");
            return sb.ToString();
        }
  
        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public  new string ToJson()
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
            return this.Equals(obj as MarketSubscriptionMessage);
        }

        /// <summary>
        /// Returns true if MarketSubscriptionMessage instances are equal
        /// </summary>
        /// <param name="other">Instance of MarketSubscriptionMessage to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(MarketSubscriptionMessage other)
        {
            // credit: http://stackoverflow.com/a/10454552/677735
            if (other == null)
                return false;

            return 
                (
                    this.Op == other.Op ||
                    this.Op != null &&
                    this.Op.Equals(other.Op)
                ) && 
                (
                    this.Id == other.Id ||
                    this.Id != null &&
                    this.Id.Equals(other.Id)
                ) && 
                (
                    this.SegmentationEnabled == other.SegmentationEnabled ||
                    this.SegmentationEnabled != null &&
                    this.SegmentationEnabled.Equals(other.SegmentationEnabled)
                ) && 
                (
                    this.Clk == other.Clk ||
                    this.Clk != null &&
                    this.Clk.Equals(other.Clk)
                ) && 
                (
                    this.HeartbeatMs == other.HeartbeatMs ||
                    this.HeartbeatMs != null &&
                    this.HeartbeatMs.Equals(other.HeartbeatMs)
                ) && 
                (
                    this.InitialClk == other.InitialClk ||
                    this.InitialClk != null &&
                    this.InitialClk.Equals(other.InitialClk)
                ) && 
                (
                    this.MarketFilter == other.MarketFilter ||
                    this.MarketFilter != null &&
                    this.MarketFilter.Equals(other.MarketFilter)
                ) && 
                (
                    this.ConflateMs == other.ConflateMs ||
                    this.ConflateMs != null &&
                    this.ConflateMs.Equals(other.ConflateMs)
                ) && 
                (
                    this.MarketDataFilter == other.MarketDataFilter ||
                    this.MarketDataFilter != null &&
                    this.MarketDataFilter.Equals(other.MarketDataFilter)
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
                
                if (this.Op != null)
                    hash = hash * 59 + this.Op.GetHashCode();
                
                if (this.Id != null)
                    hash = hash * 59 + this.Id.GetHashCode();
                
                if (this.SegmentationEnabled != null)
                    hash = hash * 59 + this.SegmentationEnabled.GetHashCode();
                
                if (this.Clk != null)
                    hash = hash * 59 + this.Clk.GetHashCode();
                
                if (this.HeartbeatMs != null)
                    hash = hash * 59 + this.HeartbeatMs.GetHashCode();
                
                if (this.InitialClk != null)
                    hash = hash * 59 + this.InitialClk.GetHashCode();
                
                if (this.MarketFilter != null)
                    hash = hash * 59 + this.MarketFilter.GetHashCode();
                
                if (this.ConflateMs != null)
                    hash = hash * 59 + this.ConflateMs.GetHashCode();
                
                if (this.MarketDataFilter != null)
                    hash = hash * 59 + this.MarketDataFilter.GetHashCode();
                
                return hash;
            }
        }

    }
}
