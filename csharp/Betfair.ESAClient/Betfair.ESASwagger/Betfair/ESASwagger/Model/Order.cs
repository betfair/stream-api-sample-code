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
    public partial class Order :  IEquatable<Order>
    { 
    
        /// <summary>
        /// Side - the side of the order
        /// </summary>
        /// <value>Side - the side of the order</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum SideEnum {
            
            [EnumMember(Value = "B")]
            B,
            
            [EnumMember(Value = "L")]
            L
        }

    
        /// <summary>
        /// Persistence Type - whether the order will persist at in play or not (L = LAPSE, P = PERSIST, MOC = Market On Close)
        /// </summary>
        /// <value>Persistence Type - whether the order will persist at in play or not (L = LAPSE, P = PERSIST, MOC = Market On Close)</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum PtEnum {
            
            [EnumMember(Value = "L")]
            L,
            
            [EnumMember(Value = "P")]
            P,
            
            [EnumMember(Value = "MOC")]
            Moc
        }

    
        /// <summary>
        /// Order Type - the type of the order (L = LIMIT, MOC = MARKET_ON_CLOSE, LOC = LIMIT_ON_CLOSE)
        /// </summary>
        /// <value>Order Type - the type of the order (L = LIMIT, MOC = MARKET_ON_CLOSE, LOC = LIMIT_ON_CLOSE)</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum OtEnum {
            
            [EnumMember(Value = "L")]
            L,
            
            [EnumMember(Value = "LOC")]
            Loc,
            
            [EnumMember(Value = "MOC")]
            Moc
        }

    
        /// <summary>
        /// Status - the status of the order (E = EXECUTABLE, EC = EXECUTION_COMPLETE)
        /// </summary>
        /// <value>Status - the status of the order (E = EXECUTABLE, EC = EXECUTION_COMPLETE)</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum StatusEnum {
            
            [EnumMember(Value = "E")]
            E,
            
            [EnumMember(Value = "EC")]
            Ec
        }

    
        /// <summary>
        /// Side - the side of the order
        /// </summary>
        /// <value>Side - the side of the order</value>
        [DataMember(Name="side", EmitDefaultValue=false)]
        public SideEnum? Side { get; set; }
    
        /// <summary>
        /// Persistence Type - whether the order will persist at in play or not (L = LAPSE, P = PERSIST, MOC = Market On Close)
        /// </summary>
        /// <value>Persistence Type - whether the order will persist at in play or not (L = LAPSE, P = PERSIST, MOC = Market On Close)</value>
        [DataMember(Name="pt", EmitDefaultValue=false)]
        public PtEnum? Pt { get; set; }
    
        /// <summary>
        /// Order Type - the type of the order (L = LIMIT, MOC = MARKET_ON_CLOSE, LOC = LIMIT_ON_CLOSE)
        /// </summary>
        /// <value>Order Type - the type of the order (L = LIMIT, MOC = MARKET_ON_CLOSE, LOC = LIMIT_ON_CLOSE)</value>
        [DataMember(Name="ot", EmitDefaultValue=false)]
        public OtEnum? Ot { get; set; }
    
        /// <summary>
        /// Status - the status of the order (E = EXECUTABLE, EC = EXECUTION_COMPLETE)
        /// </summary>
        /// <value>Status - the status of the order (E = EXECUTABLE, EC = EXECUTION_COMPLETE)</value>
        [DataMember(Name="status", EmitDefaultValue=false)]
        public StatusEnum? Status { get; set; }
    
        /// <summary>
        /// Initializes a new instance of the <see cref="Order" /> class.
        /// Initializes a new instance of the <see cref="Order" />class.
        /// </summary>
        /// <param name="Side">Side - the side of the order.</param>
        /// <param name="Sv">Size Voided - the amount of the order that has been voided.</param>
        /// <param name="Pt">Persistence Type - whether the order will persist at in play or not (L = LAPSE, P = PERSIST, MOC = Market On Close).</param>
        /// <param name="Ot">Order Type - the type of the order (L = LIMIT, MOC = MARKET_ON_CLOSE, LOC = LIMIT_ON_CLOSE).</param>
        /// <param name="P">Price - the original placed price of the order.</param>
        /// <param name="Sc">Size Cancelled - the amount of the order that has been cancelled.</param>
        /// <param name="Rc">Regulator Code - the regulator of the order.</param>
        /// <param name="S">Size - the original placed size of the order.</param>
        /// <param name="Pd">Placed Date - the date the order was placed.</param>
        /// <param name="Rac">Regulator Auth Code - the auth code returned by the regulator.</param>
        /// <param name="Md">Matched Date - the date the order was matched (null if the order is not matched).</param>
        /// <param name="Sl">Size Lapsed - the amount of the order that has been lapsed.</param>
        /// <param name="Avp">Average Price Matched - the average price the order was matched at (null if the order is not matched.</param>
        /// <param name="Sm">Size Matched - the amount of the order that has been matched.</param>
        /// <param name="Id">Bet Id - the id of the order.</param>
        /// <param name="Bsp">BSP Liability - the BSP liability of the order (null if the order is not a BSP order).</param>
        /// <param name="Status">Status - the status of the order (E = EXECUTABLE, EC = EXECUTION_COMPLETE).</param>
        /// <param name="Sr">Size Remaining - the amount of the order that is remaining unmatched.</param>

        public Order(SideEnum? Side = null, double? Sv = null, PtEnum? Pt = null, OtEnum? Ot = null, double? P = null, double? Sc = null, string Rc = null, double? S = null, long? Pd = null, string Rac = null, long? Md = null, double? Sl = null, double? Avp = null, double? Sm = null, string Id = null, double? Bsp = null, StatusEnum? Status = null, double? Sr = null)
        {
            this.Side = Side;
            this.Sv = Sv;
            this.Pt = Pt;
            this.Ot = Ot;
            this.P = P;
            this.Sc = Sc;
            this.Rc = Rc;
            this.S = S;
            this.Pd = Pd;
            this.Rac = Rac;
            this.Md = Md;
            this.Sl = Sl;
            this.Avp = Avp;
            this.Sm = Sm;
            this.Id = Id;
            this.Bsp = Bsp;
            this.Status = Status;
            this.Sr = Sr;
            
        }
        
    
        /// <summary>
        /// Size Voided - the amount of the order that has been voided
        /// </summary>
        /// <value>Size Voided - the amount of the order that has been voided</value>
        [DataMember(Name="sv", EmitDefaultValue=false)]
        public double? Sv { get; set; }
    
        /// <summary>
        /// Price - the original placed price of the order
        /// </summary>
        /// <value>Price - the original placed price of the order</value>
        [DataMember(Name="p", EmitDefaultValue=false)]
        public double? P { get; set; }
    
        /// <summary>
        /// Size Cancelled - the amount of the order that has been cancelled
        /// </summary>
        /// <value>Size Cancelled - the amount of the order that has been cancelled</value>
        [DataMember(Name="sc", EmitDefaultValue=false)]
        public double? Sc { get; set; }
    
        /// <summary>
        /// Regulator Code - the regulator of the order
        /// </summary>
        /// <value>Regulator Code - the regulator of the order</value>
        [DataMember(Name="rc", EmitDefaultValue=false)]
        public string Rc { get; set; }
    
        /// <summary>
        /// Size - the original placed size of the order
        /// </summary>
        /// <value>Size - the original placed size of the order</value>
        [DataMember(Name="s", EmitDefaultValue=false)]
        public double? S { get; set; }
    
        /// <summary>
        /// Placed Date - the date the order was placed
        /// </summary>
        /// <value>Placed Date - the date the order was placed</value>
        [DataMember(Name="pd", EmitDefaultValue=false)]
        public long? Pd { get; set; }
    
        /// <summary>
        /// Regulator Auth Code - the auth code returned by the regulator
        /// </summary>
        /// <value>Regulator Auth Code - the auth code returned by the regulator</value>
        [DataMember(Name="rac", EmitDefaultValue=false)]
        public string Rac { get; set; }
    
        /// <summary>
        /// Matched Date - the date the order was matched (null if the order is not matched)
        /// </summary>
        /// <value>Matched Date - the date the order was matched (null if the order is not matched)</value>
        [DataMember(Name="md", EmitDefaultValue=false)]
        public long? Md { get; set; }
    
        /// <summary>
        /// Size Lapsed - the amount of the order that has been lapsed
        /// </summary>
        /// <value>Size Lapsed - the amount of the order that has been lapsed</value>
        [DataMember(Name="sl", EmitDefaultValue=false)]
        public double? Sl { get; set; }
    
        /// <summary>
        /// Average Price Matched - the average price the order was matched at (null if the order is not matched
        /// </summary>
        /// <value>Average Price Matched - the average price the order was matched at (null if the order is not matched</value>
        [DataMember(Name="avp", EmitDefaultValue=false)]
        public double? Avp { get; set; }
    
        /// <summary>
        /// Size Matched - the amount of the order that has been matched
        /// </summary>
        /// <value>Size Matched - the amount of the order that has been matched</value>
        [DataMember(Name="sm", EmitDefaultValue=false)]
        public double? Sm { get; set; }
    
        /// <summary>
        /// Bet Id - the id of the order
        /// </summary>
        /// <value>Bet Id - the id of the order</value>
        [DataMember(Name="id", EmitDefaultValue=false)]
        public string Id { get; set; }
    
        /// <summary>
        /// BSP Liability - the BSP liability of the order (null if the order is not a BSP order)
        /// </summary>
        /// <value>BSP Liability - the BSP liability of the order (null if the order is not a BSP order)</value>
        [DataMember(Name="bsp", EmitDefaultValue=false)]
        public double? Bsp { get; set; }
    
        /// <summary>
        /// Size Remaining - the amount of the order that is remaining unmatched
        /// </summary>
        /// <value>Size Remaining - the amount of the order that is remaining unmatched</value>
        [DataMember(Name="sr", EmitDefaultValue=false)]
        public double? Sr { get; set; }
    
        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class Order {\n");
            sb.Append("  Side: ").Append(Side).Append("\n");
            sb.Append("  Sv: ").Append(Sv).Append("\n");
            sb.Append("  Pt: ").Append(Pt).Append("\n");
            sb.Append("  Ot: ").Append(Ot).Append("\n");
            sb.Append("  P: ").Append(P).Append("\n");
            sb.Append("  Sc: ").Append(Sc).Append("\n");
            sb.Append("  Rc: ").Append(Rc).Append("\n");
            sb.Append("  S: ").Append(S).Append("\n");
            sb.Append("  Pd: ").Append(Pd).Append("\n");
            sb.Append("  Rac: ").Append(Rac).Append("\n");
            sb.Append("  Md: ").Append(Md).Append("\n");
            sb.Append("  Sl: ").Append(Sl).Append("\n");
            sb.Append("  Avp: ").Append(Avp).Append("\n");
            sb.Append("  Sm: ").Append(Sm).Append("\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  Bsp: ").Append(Bsp).Append("\n");
            sb.Append("  Status: ").Append(Status).Append("\n");
            sb.Append("  Sr: ").Append(Sr).Append("\n");
            
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
            return this.Equals(obj as Order);
        }

        /// <summary>
        /// Returns true if Order instances are equal
        /// </summary>
        /// <param name="other">Instance of Order to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(Order other)
        {
            // credit: http://stackoverflow.com/a/10454552/677735
            if (other == null)
                return false;

            return 
                (
                    this.Side == other.Side ||
                    this.Side != null &&
                    this.Side.Equals(other.Side)
                ) && 
                (
                    this.Sv == other.Sv ||
                    this.Sv != null &&
                    this.Sv.Equals(other.Sv)
                ) && 
                (
                    this.Pt == other.Pt ||
                    this.Pt != null &&
                    this.Pt.Equals(other.Pt)
                ) && 
                (
                    this.Ot == other.Ot ||
                    this.Ot != null &&
                    this.Ot.Equals(other.Ot)
                ) && 
                (
                    this.P == other.P ||
                    this.P != null &&
                    this.P.Equals(other.P)
                ) && 
                (
                    this.Sc == other.Sc ||
                    this.Sc != null &&
                    this.Sc.Equals(other.Sc)
                ) && 
                (
                    this.Rc == other.Rc ||
                    this.Rc != null &&
                    this.Rc.Equals(other.Rc)
                ) && 
                (
                    this.S == other.S ||
                    this.S != null &&
                    this.S.Equals(other.S)
                ) && 
                (
                    this.Pd == other.Pd ||
                    this.Pd != null &&
                    this.Pd.Equals(other.Pd)
                ) && 
                (
                    this.Rac == other.Rac ||
                    this.Rac != null &&
                    this.Rac.Equals(other.Rac)
                ) && 
                (
                    this.Md == other.Md ||
                    this.Md != null &&
                    this.Md.Equals(other.Md)
                ) && 
                (
                    this.Sl == other.Sl ||
                    this.Sl != null &&
                    this.Sl.Equals(other.Sl)
                ) && 
                (
                    this.Avp == other.Avp ||
                    this.Avp != null &&
                    this.Avp.Equals(other.Avp)
                ) && 
                (
                    this.Sm == other.Sm ||
                    this.Sm != null &&
                    this.Sm.Equals(other.Sm)
                ) && 
                (
                    this.Id == other.Id ||
                    this.Id != null &&
                    this.Id.Equals(other.Id)
                ) && 
                (
                    this.Bsp == other.Bsp ||
                    this.Bsp != null &&
                    this.Bsp.Equals(other.Bsp)
                ) && 
                (
                    this.Status == other.Status ||
                    this.Status != null &&
                    this.Status.Equals(other.Status)
                ) && 
                (
                    this.Sr == other.Sr ||
                    this.Sr != null &&
                    this.Sr.Equals(other.Sr)
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
                
                if (this.Side != null)
                    hash = hash * 59 + this.Side.GetHashCode();
                
                if (this.Sv != null)
                    hash = hash * 59 + this.Sv.GetHashCode();
                
                if (this.Pt != null)
                    hash = hash * 59 + this.Pt.GetHashCode();
                
                if (this.Ot != null)
                    hash = hash * 59 + this.Ot.GetHashCode();
                
                if (this.P != null)
                    hash = hash * 59 + this.P.GetHashCode();
                
                if (this.Sc != null)
                    hash = hash * 59 + this.Sc.GetHashCode();
                
                if (this.Rc != null)
                    hash = hash * 59 + this.Rc.GetHashCode();
                
                if (this.S != null)
                    hash = hash * 59 + this.S.GetHashCode();
                
                if (this.Pd != null)
                    hash = hash * 59 + this.Pd.GetHashCode();
                
                if (this.Rac != null)
                    hash = hash * 59 + this.Rac.GetHashCode();
                
                if (this.Md != null)
                    hash = hash * 59 + this.Md.GetHashCode();
                
                if (this.Sl != null)
                    hash = hash * 59 + this.Sl.GetHashCode();
                
                if (this.Avp != null)
                    hash = hash * 59 + this.Avp.GetHashCode();
                
                if (this.Sm != null)
                    hash = hash * 59 + this.Sm.GetHashCode();
                
                if (this.Id != null)
                    hash = hash * 59 + this.Id.GetHashCode();
                
                if (this.Bsp != null)
                    hash = hash * 59 + this.Bsp.GetHashCode();
                
                if (this.Status != null)
                    hash = hash * 59 + this.Status.GetHashCode();
                
                if (this.Sr != null)
                    hash = hash * 59 + this.Sr.GetHashCode();
                
                return hash;
            }
        }

    }
}
