using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace N2P.PublicApi.Generated.Model
{
    public partial class ValidationError : ErrorModel
    {
        [JsonPropertyName("details")]
        public List<ValidationErrorDetail> Details { get; set; } = new List<ValidationErrorDetail>();
    }
}
