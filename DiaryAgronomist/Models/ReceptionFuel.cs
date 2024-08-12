using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiaryAgronomist.Models
{
    [Table(name: "reception_fuel", Schema = "public")]
    public class ReceptionFuel
    {
        [Column("id_reception")]
        [Key]
        public int ReceptionId { get; set; }

        [Column("date_reception")]
        public DateTime DateReception { get; set; }

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

        [Column("id_supplier")]
        [ForeignKey(nameof(FuelSupplier))]
        public int SupplierId { get; set; }
        public FuelSupplier FuelSupplier { get; set; }
        
    }
}
