using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable enable

namespace EntitiesLayer
{
    public class Aviso
    {
        public Aviso()
        {
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }

        public int IdAnimal { get; set; }

        public string? Comentario { get; set; }

        public AvisoEstado Estado { get; set; }

        public DateTime Fecha { get; set; }

        public bool Eliminado { get; set; }

        [Required]
        public required string UltimaUbicacionVista { get; set; }

        public Animal? Animal { get; set; }

        public ICollection<Comentario> Comentarios { get; set; } = new List<Comentario>();

    }
}
