using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiaryAgronomist.Models
{
    [Table(name: "consumption_fuel", Schema = "public")]
    public class ConsumptionFuel
    {
        [Column("id_consumption")]
        [Key]
        public int ConsumptionId { get; set; }

        [Column("date_consumption")]
        public DateTime DateConsumption {  get; set; }

        [Column("id_fuel")]
        [ForeignKey(nameof(Fuel))]
        public int FuelId { get; set; }
        public Fuel Fuel { get; set; }

        [Column("fuel_volume")]
        public double FuelVolume { get; set; }

        [Column("id_employee")]
        [ForeignKey(nameof(Employee))]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
