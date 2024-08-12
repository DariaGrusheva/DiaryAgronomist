using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiaryAgronomist.Models
{
    [Table(name: "fields_planting", Schema = "public")]

    public class FieldPlanting
    {
        [Column("id_field")]
        [Key]
        public int FieldId { get; set; }

        [Column("field_area")]
        public double FieldArea { get; set; }

        [Column("field_address")]
        [Required]
        [StringLength(200, ErrorMessage = "Длина поля не должна превышать 200 символов")]
        public string FieldAddress { get; set; }

        public ICollection<Sowing> Sowings { get; set; } = new List<Sowing>();
        public ICollection<Harvesting> Harvestings { get; set; } = new List<Harvesting>();
    }
}
