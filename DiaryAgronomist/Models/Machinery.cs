using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiaryAgronomist.Models
{
    [Table(name: "machinery", Schema = "public")]
    public class Machinery
    {
        [Column("id_machinery")]
        [Key]
        public int MachineryId { get; set; }

        [Column("name_machinery")]
        [Required]
        [StringLength(200, ErrorMessage = "Длина поля не должна превышать 200 символов")]
        public string MachineryName { get; set; }

        [Column("year_manufacture")]
        public int ManufactureYear { get; set; }

        public ICollection<HarvestingTechnique> HarvestingTechniques { get; set; } = new List<HarvestingTechnique>();
        public ICollection<Harvesting> Harvestings { get; set; } = new List<Harvesting>();
        public ICollection<SowingTechnique> SowingTechniques { get; set; } = new List<SowingTechnique>();
        public ICollection<Sowing> Sowings { get; set; } = new List<Sowing>();
    }
}
