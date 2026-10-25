using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using EntitiesLayer;

namespace PresentationLayer.Models
{
    [JsonUnmappedMemberHandling(JsonUnmappedMemberHandling.Disallow)]
    public sealed record CreateAvisoRequest
    {
        [Range(1, int.MaxValue)]
        public required int IdAnimal { get; init; }

        [EnumDataType(typeof(AvisoEstado))]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public required AvisoEstado Estado { get; init; }

        [Required]
        public required string UltimaUbicacionVista { get; init; }

        public string? Comentario { get; init; }
    }
}
