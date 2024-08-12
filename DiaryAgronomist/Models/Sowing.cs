using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiaryAgronomist.Models
{
    [Table(name: "sowing", Schema = "public")]
    public class Sowing
    {
        [Column("id_sowing")]
        [Key]
        public int SowingId { get; set; }

        [Column("date_sowing")]
        public DateTime SowingDate { get; set; }

        [Column("id_field")]
        [ForeignKey(nameof(FieldPlanting))]
        public int FieldId { get; set; }
        public FieldPlanting FieldPlanting { get; set; }

        [Column("id_cereal")]
        [ForeignKey(nameof(Cereal))]
        public int CerealId { get; set; }
        public Cereal Cereal { get; set; }

        [Column("number_cereal")]
        public double NumberCereal { get; set; }

        [Column("sown_area")]
        public double SownArea { get; set; }

        public ICollection<SowingEmployee> SowingEmployees { get; set; } = new List<SowingEmployee>();
        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
        public ICollection<SowingTechnique> SowingTechniques { get; set; } = new List<SowingTechnique>();
        public ICollection<Machinery> Machineries { get; set; } = new List<Machinery>();

    }
}
