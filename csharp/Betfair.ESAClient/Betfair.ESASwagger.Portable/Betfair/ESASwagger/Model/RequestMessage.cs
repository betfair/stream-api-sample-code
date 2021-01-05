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
    public partial class RequestMessage :  IEquatable<RequestMessage>
    { 
    
        /// <summary>
        /// Initializes a new instance of the <see cref="RequestMessage" /> class.
        /// Initializes a new instance of the <see cref="RequestMessage" />class.
        /// </summary>
        /// <param name="Op">The operation type.</param>
        /// <param name="Id">Client generated unique id to link request with response (like json rpc).</param>

        public RequestMessage(string Op = null, int? Id = null)
        {
            this.Op = Op;
            this.Id = Id;
            
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
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class RequestMessage {\n");
            sb.Append("  Op: ").Append(Op).Append("\n");
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
            return this.Equals(obj as RequestMessage);
        }

        /// <summary>
        /// Returns true if RequestMessage instances are equal
        /// </summary>
        /// <param name="other">Instance of RequestMessage to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(RequestMessage other)
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
                
                return hash;
            }
        }

    }
}
