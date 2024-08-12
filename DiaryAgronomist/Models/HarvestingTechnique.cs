using System.ComponentModel.DataAnnotations.Schema;

namespace DiaryAgronomist.Models
{
    [Table(name: "harvesting_technique", Schema ="public")]
    public class HarvestingTechnique
    {
        [Column("id_harvesting")]
        [ForeignKey(nameof(Harvesting))]
        public int HarvestingId { get; set; }
        public Harvesting Harvesting { get; set; }

        [Column("id_machinery")]
        [ForeignKey(nameof(Machinery))]
        public int MachineryId { get; set; }
        public Machinery Machinery { get; set; }
    }
}
