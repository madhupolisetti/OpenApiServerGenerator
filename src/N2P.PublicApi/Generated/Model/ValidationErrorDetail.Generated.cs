using System.Text.Json.Serialization;

namespace N2P.PublicApi.Generated.Model
{
    public partial class ValidationErrorDetail
    {
        [JsonPropertyName("field")]
        public string? Field { get; set; }
        [JsonPropertyName("message")]
        public string Message { get; set; }
        [JsonPropertyName("code")]
        public ValidationErrorCode? Code { get; set; }
    }
}
