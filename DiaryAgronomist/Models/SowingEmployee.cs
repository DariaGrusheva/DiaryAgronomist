using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiaryAgronomist.Models
{
    [Table(name: "sowing_employee", Schema = "public")]
    public class SowingEmployee
    {
        [Column("id_sowing")]
        [ForeignKey(nameof(Sowing))]
        public int SowingId { get; set; }
        public Sowing Sowing { get; set; }

        [Column("id_employee")]
        [ForeignKey(nameof(Employee))]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
