using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace N2P.OpenApiServerGenerator.Model
{
    public class SourceGeneratorOptions
    {
        public const string ConfigurationFileDiscriminator = "N2POpenApiSeverGeneratorConfiguration";
        [Required] [NotNull] internal string SpecificationUrl { get; set; } = string.Empty;
    }
}
