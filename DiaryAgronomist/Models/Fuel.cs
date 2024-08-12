using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiaryAgronomist.Models
{
    [Table(name: "fuel", Schema = "public")]
    public class Fuel
    {
        [Column("id_fuel")]
        [Key]
        public int FuelId { get; set; }

        [Column("name_fuel")]
        [Required]
        [StringLength(300, ErrorMessage = "Длина поля не должна превышать 300 символов")]
        public string FuelName { get; set; }

        public ICollection<ReceptionFuel> ReceptionFuels { get; set; } = new List<ReceptionFuel>();
        public ICollection<ConsumptionFuel> ConsumptionFuels { get; set; } = new List<ConsumptionFuel>();
    }
}
