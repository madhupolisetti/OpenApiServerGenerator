using System;
using System.Text.Json.Serialization;

namespace N2P.PublicApi.Generated.Model
{
    public partial class ErrorModel
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonPropertyName("message")]
        public string Message { get; set; }
        [JsonPropertyName("statusCode")]
        public int StatusCode { get; set; }
        [JsonPropertyName("traceId")]
        public string TraceId { get; set; }
    }
}
