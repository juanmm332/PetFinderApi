using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntitiesLayer
{
    public class TossedCoin
    {
        public TossedCoin()
        {
            Value = Helpers.GenerateValue();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Value { get; set; }

        
    }
}
