using System.ComponentModel.DataAnnotations.Schema;

namespace DiaryAgronomist.Models
{
    [Table(name: "harvesting_employee", Schema ="public")]
    public class HarvestingEmployee
    {
        [Column("id_harvesting")]
        [ForeignKey(nameof(Harvesting))]
        public int HarvestingId { get; set; }
        public Harvesting Harvesting { get; set; }

        [Column("id_employee")]
        [ForeignKey(nameof(Employee))]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
