using System.ComponentModel.DataAnnotations;

namespace PresentationLayer.Models
{
    public sealed record LoginRequest
    {
        [Required, EmailAddress]
        public required string Correo { get; init; }

        [Required]
        public required string Contraseña { get; init; }
    }
}
