using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace N2P.PublicApi.Generated.Model
{
    [GeneratedCode("N2PApiServerGenerator", "1.0.0")]
    public partial class AvatarModel
    {
        [JsonPropertyName("userId")]
        public int UserId { get; set; }

        [JsonPropertyName("avatarImages")]
        public List<AvatarImageModel> AvatarImages { get; set; } = new List<AvatarImageModel>();
    }
}
