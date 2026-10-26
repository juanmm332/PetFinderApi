using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable enable

namespace EntitiesLayer
{
    public class Comentario
    {
        public Comentario()
        {
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }

        public int IdUsuario { get; set; }

        public int IdAviso { get; set; }

        public bool Eliminado { get; set; }

        [Required]
        public required string Texto { get; set; }

        public DateTime Fecha { get; set; }

        public Usuario? Usuario { get; set; }

        public Aviso? Aviso { get; set; }

    }
}
