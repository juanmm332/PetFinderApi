using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PresentationLayer.Models
{
    [JsonUnmappedMemberHandling(JsonUnmappedMemberHandling.Disallow)]
    public sealed record CreateUsuarioRequest
    {
        [Required]
        public required string Nombre { get; init; }

        [Required]
        public required string Contraseña { get; init; }

        [Required]
        public required string Correo { get; init; }

        [Required]
        public required string Telefono { get; init; }
    }
}
