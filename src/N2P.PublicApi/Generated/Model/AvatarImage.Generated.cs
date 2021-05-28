using System.CodeDom.Compiler;
using System.Text.Json.Serialization;

namespace N2P.PublicApi.Generated.Model
{
    [GeneratedCode("N2PApiServerGenerator", "1.0.0")]
    public partial class AvatarImageModel
    {
        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("size")]
        public string Size { get; set; }
    }
}
