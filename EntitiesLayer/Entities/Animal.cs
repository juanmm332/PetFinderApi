using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        public string Nombre { get; set; }

        public int IdUsuario { get; set; }

        public string Comentario { get; set; }

    }
}
