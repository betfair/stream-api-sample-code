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
    public partial class AuthenticationMessage : RequestMessage,  IEquatable<AuthenticationMessage>
    { 
    
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationMessage" /> class.
        /// Initializes a new instance of the <see cref="AuthenticationMessage" />class.
        /// </summary>
        /// <param name="Op">The operation type.</param>
        /// <param name="Id">Client generated unique id to link request with response (like json rpc).</param>
        /// <param name="Session">Session.</param>
        /// <param name="AppKey">AppKey.</param>

        public AuthenticationMessage(string Op = null, int? Id = null, string Session = null, string AppKey = null)
        {
            this.Op = Op;
            this.Id = Id;
            this.Session = Session;
            this.AppKey = AppKey;
            
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
        /// Gets or Sets Session
        /// </summary>
        [DataMember(Name="session", EmitDefaultValue=false)]
        public string Session { get; set; }
    
        /// <summary>
        /// Gets or Sets AppKey
        /// </summary>
        [DataMember(Name="appKey", EmitDefaultValue=false)]
        public string AppKey { get; set; }
    
        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class AuthenticationMessage {\n");
            sb.Append("  Op: ").Append(Op).Append("\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  Session: ").Append(Session).Append("\n");
            sb.Append("  AppKey: ").Append(AppKey).Append("\n");
            
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
            return this.Equals(obj as AuthenticationMessage);
        }

        /// <summary>
        /// Returns true if AuthenticationMessage instances are equal
        /// </summary>
        /// <param name="other">Instance of AuthenticationMessage to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(AuthenticationMessage other)
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
                    this.Session == other.Session ||
                    this.Session != null &&
                    this.Session.Equals(other.Session)
                ) && 
                (
                    this.AppKey == other.AppKey ||
                    this.AppKey != null &&
                    this.AppKey.Equals(other.AppKey)
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
                
                if (this.Session != null)
                    hash = hash * 59 + this.Session.GetHashCode();
                
                if (this.AppKey != null)
                    hash = hash * 59 + this.AppKey.GetHashCode();
                
                return hash;
            }
        }

    }
}
