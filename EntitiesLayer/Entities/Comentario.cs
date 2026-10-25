using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        public string Texto { get; set; }

        public DateTime Fecha { get; set; }

    }
}
