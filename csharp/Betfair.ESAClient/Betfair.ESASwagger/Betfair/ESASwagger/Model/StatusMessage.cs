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
    public partial class StatusMessage : ResponseMessage,  IEquatable<StatusMessage>
    { 
    
        /// <summary>
        /// The type of error in case of a failure
        /// </summary>
        /// <value>The type of error in case of a failure</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum ErrorCodeEnum {
            
            [EnumMember(Value = "NO_APP_KEY")]
            NoAppKey,
            
            [EnumMember(Value = "INVALID_APP_KEY")]
            InvalidAppKey,
            
            [EnumMember(Value = "NO_SESSION")]
            NoSession,
            
            [EnumMember(Value = "INVALID_SESSION_INFORMATION")]
            InvalidSessionInformation,
            
            [EnumMember(Value = "NOT_AUTHORIZED")]
            NotAuthorized,
            
            [EnumMember(Value = "INVALID_INPUT")]
            InvalidInput,
            
            [EnumMember(Value = "INVALID_CLOCK")]
            InvalidClock,
            
            [EnumMember(Value = "UNEXPECTED_ERROR")]
            UnexpectedError,
            
            [EnumMember(Value = "TIMEOUT")]
            Timeout,
            
            [EnumMember(Value = "SUBSCRIPTION_LIMIT_EXCEEDED")]
            SubscriptionLimitExceeded,
            
            [EnumMember(Value = "INVALID_REQUEST")]
            InvalidRequest,
            
            [EnumMember(Value = "CONNECTION_FAILED")]
            ConnectionFailed,
            
            [EnumMember(Value = "MAX_CONNECTION_LIMIT_EXCEEDED")]
            MaxConnectionLimitExceeded
        }

    
        /// <summary>
        /// The status of the last request
        /// </summary>
        /// <value>The status of the last request</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum StatusCodeEnum {
            
            [EnumMember(Value = "SUCCESS")]
            Success,
            
            [EnumMember(Value = "FAILURE")]
            Failure
        }

    
        /// <summary>
        /// The type of error in case of a failure
        /// </summary>
        /// <value>The type of error in case of a failure</value>
        [DataMember(Name="errorCode", EmitDefaultValue=false)]
        public ErrorCodeEnum? ErrorCode { get; set; }
    
        /// <summary>
        /// The status of the last request
        /// </summary>
        /// <value>The status of the last request</value>
        [DataMember(Name="statusCode", EmitDefaultValue=false)]
        public StatusCodeEnum? StatusCode { get; set; }
    
        /// <summary>
        /// Initializes a new instance of the <see cref="StatusMessage" /> class.
        /// Initializes a new instance of the <see cref="StatusMessage" />class.
        /// </summary>
        /// <param name="Op">The operation type.</param>
        /// <param name="Id">Client generated unique id to link request with response (like json rpc).</param>
        /// <param name="ErrorMessage">Additional message in case of a failure.</param>
        /// <param name="ErrorCode">The type of error in case of a failure.</param>
        /// <param name="ConnectionId">The connection id.</param>
        /// <param name="ConnectionClosed">Is the connection now closed.</param>
        /// <param name="StatusCode">The status of the last request.</param>

        public StatusMessage(string Op = null, int? Id = null, string ErrorMessage = null, ErrorCodeEnum? ErrorCode = null, string ConnectionId = null, bool? ConnectionClosed = null, StatusCodeEnum? StatusCode = null)
        {
            this.Op = Op;
            this.Id = Id;
            this.ErrorMessage = ErrorMessage;
            this.ErrorCode = ErrorCode;
            this.ConnectionId = ConnectionId;
            this.ConnectionClosed = ConnectionClosed;
            this.StatusCode = StatusCode;
            
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
        /// Additional message in case of a failure
        /// </summary>
        /// <value>Additional message in case of a failure</value>
        [DataMember(Name="errorMessage", EmitDefaultValue=false)]
        public string ErrorMessage { get; set; }
    
        /// <summary>
        /// The connection id
        /// </summary>
        /// <value>The connection id</value>
        [DataMember(Name="connectionId", EmitDefaultValue=false)]
        public string ConnectionId { get; set; }
    
        /// <summary>
        /// Is the connection now closed
        /// </summary>
        /// <value>Is the connection now closed</value>
        [DataMember(Name="connectionClosed", EmitDefaultValue=false)]
        public bool? ConnectionClosed { get; set; }
    
        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class StatusMessage {\n");
            sb.Append("  Op: ").Append(Op).Append("\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  ErrorMessage: ").Append(ErrorMessage).Append("\n");
            sb.Append("  ErrorCode: ").Append(ErrorCode).Append("\n");
            sb.Append("  ConnectionId: ").Append(ConnectionId).Append("\n");
            sb.Append("  ConnectionClosed: ").Append(ConnectionClosed).Append("\n");
            sb.Append("  StatusCode: ").Append(StatusCode).Append("\n");
            
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
            return this.Equals(obj as StatusMessage);
        }

        /// <summary>
        /// Returns true if StatusMessage instances are equal
        /// </summary>
        /// <param name="other">Instance of StatusMessage to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(StatusMessage other)
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
                    this.ErrorMessage == other.ErrorMessage ||
                    this.ErrorMessage != null &&
                    this.ErrorMessage.Equals(other.ErrorMessage)
                ) && 
                (
                    this.ErrorCode == other.ErrorCode ||
                    this.ErrorCode != null &&
                    this.ErrorCode.Equals(other.ErrorCode)
                ) && 
                (
                    this.ConnectionId == other.ConnectionId ||
                    this.ConnectionId != null &&
                    this.ConnectionId.Equals(other.ConnectionId)
                ) && 
                (
                    this.ConnectionClosed == other.ConnectionClosed ||
                    this.ConnectionClosed != null &&
                    this.ConnectionClosed.Equals(other.ConnectionClosed)
                ) && 
                (
                    this.StatusCode == other.StatusCode ||
                    this.StatusCode != null &&
                    this.StatusCode.Equals(other.StatusCode)
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
                
                if (this.ErrorMessage != null)
                    hash = hash * 59 + this.ErrorMessage.GetHashCode();
                
                if (this.ErrorCode != null)
                    hash = hash * 59 + this.ErrorCode.GetHashCode();
                
                if (this.ConnectionId != null)
                    hash = hash * 59 + this.ConnectionId.GetHashCode();
                
                if (this.ConnectionClosed != null)
                    hash = hash * 59 + this.ConnectionClosed.GetHashCode();
                
                if (this.StatusCode != null)
                    hash = hash * 59 + this.StatusCode.GetHashCode();
                
                return hash;
            }
        }

    }
}
