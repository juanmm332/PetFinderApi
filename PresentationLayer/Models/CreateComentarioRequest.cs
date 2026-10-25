using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PresentationLayer.Models
{
    [JsonUnmappedMemberHandling(JsonUnmappedMemberHandling.Disallow)]
    public sealed record CreateComentarioRequest
    {
        [Required]
        public required string Texto { get; init; }

        [Range(1, int.MaxValue)]
        public required int IdAviso { get; init; }

        [Range(1, int.MaxValue)]
        public required int IdUsuario { get; init; }
    }
}
