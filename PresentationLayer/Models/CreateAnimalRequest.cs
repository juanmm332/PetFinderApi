using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PresentationLayer.Models
{
    [JsonUnmappedMemberHandling(JsonUnmappedMemberHandling.Disallow)]
    public sealed record CreateAnimalRequest
    {
        [Required]
        public required string Nombre { get; init; }

        [Required]
        public required string Foto { get; init; }

        public string? Comentario { get; init; }

    }
}
