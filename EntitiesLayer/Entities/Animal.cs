using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable enable

namespace EntitiesLayer
{
    public class Animal
    {
        public Animal()
        {
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }

        [Required]
        public required string Nombre { get; set; }

        [Required]
        public required string Foto { get; set; }

        public int IdUsuario { get; set; }

        public string? Comentario { get; set; }

        public Usuario? Usuario { get; set; }

        public ICollection<Aviso> Avisos { get; set; } = new List<Aviso>();

    }
}
