using System.ComponentModel.DataAnnotations.Schema;

namespace DiaryAgronomist.Models
{
    [Table(name: "sowing_technique", Schema = "public")]
    public class SowingTechnique
    {
        [Column("id_sowing")]
        [ForeignKey(nameof(Sowing))]
        public int SowingId { get; set; }
        public Sowing Sowing { get; set; }

        [Column("id_machinery")]
        [ForeignKey(nameof(Machinery))]
        public int MachineryId { get; set; }
        public Machinery Machinery { get; set; }
    }
}
