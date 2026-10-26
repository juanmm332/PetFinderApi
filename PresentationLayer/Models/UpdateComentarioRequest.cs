using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PresentationLayer.Models
{
    [JsonUnmappedMemberHandling(JsonUnmappedMemberHandling.Disallow)]
    public sealed record UpdateComentarioRequest
    {
        [Required]
        public required string Texto { get; init; }
    }
}
