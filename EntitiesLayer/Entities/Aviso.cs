using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        public string Comentario { get; set; }

        public int Estado { get; set; }

        public DateTime Fecha { get; set; }

        public string Ubicacion { get; set; }

    }
}
