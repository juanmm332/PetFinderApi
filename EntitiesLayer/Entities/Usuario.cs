using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntitiesLayer
{
    public class Usuario
    {
        public Usuario()
        {
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Contraseña { get; set; }

        public string Correo { get; set; }

        public string Telefono { get; set; }

    }
}
