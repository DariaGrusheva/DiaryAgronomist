using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiaryAgronomist.Models
{
    [Table(name: "cereal", Schema = "public")]
    public class Cereal
    {
        [Column("id_cereal")]
        [Key]
        public int CerealId { get; set; }

        [Column("name_cereal")]
        [Required]
        [StringLength(50, ErrorMessage = "Длина поля не должна превышать 50 символов")]
        public string CerealName { get; set; }

        public ICollection<Sowing> Sowings { get; set; } = new List<Sowing>();
        public ICollection<Harvesting> Harvestings { get; set; } = new List<Harvesting>();
        public ICollection<Warehouse> Warehouses { get; set; } = new List<Warehouse>();
    }
}
