using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiaryAgronomist.Models
{
    [Table(name: "harvesting", Schema = "public")]
    public class Harvesting
    {
        [Column("id_harvesting")]
        [Key]
        public int HarvestingId { get; set; }
       
        [Column("date_harvesting")]
        public DateTime HarvestingDate { get; set;}

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

        public ICollection<HarvestingEmployee> HarvestingEmployees { get; set; } = new List<HarvestingEmployee>();
        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
        public ICollection<HarvestingTechnique> HarvestingTechniques { get; set; } = new List<HarvestingTechnique>();
        public ICollection<Machinery> Machineries { get; set; } = new List<Machinery>();
    }
}
