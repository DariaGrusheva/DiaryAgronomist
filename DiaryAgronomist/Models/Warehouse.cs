using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiaryAgronomist.Models
{
    [Table(name: "warehouses", Schema = "public")]
    public class Warehouse
    {
        [Column("id_warehouse")]
        [Key]
        public int WarehouseId { get; set; }

        [Column("date_shipment")]
        public DateTime ShipmentDate { get; set; }

        [Column("id_cereal")]
        [ForeignKey(nameof(Cereal))]
        public int CerealId { get; set; }
        public Cereal Cereal { get; set; }

        [Column("number_cereal")]
        public double NumberCereal { get; set; }

        [Column("id_employee")]
        [ForeignKey(nameof(Employee))]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

    }
}
