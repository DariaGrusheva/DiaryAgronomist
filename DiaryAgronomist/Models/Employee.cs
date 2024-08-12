using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiaryAgronomist.Models
{
    [Table(name: "employee", Schema = "public")]
    public class Employee
    {
        [Column("id_employee")]
        [Key]
        public int EmployeeId { get; set; }

        [Column("surname_employee")]
        [Required]
        [StringLength(100, ErrorMessage = "Длина поля не должна превышать 100 символов")]
        public string Surname { get; set; }

        [Column("firstname_employee")]
        [Required]
        [StringLength(100, ErrorMessage = "Длина поля не должна превышать 100 символов")]
        public string Firstname { get; set; }

        [Column("patronymic_employee")]
        [StringLength(200, ErrorMessage = "Длина поля не должна превышать 200 символов")]
        public string? Patronymic { get; set; }

        [Column("phone_number")]
        [Required]
        [StringLength(50, ErrorMessage = "Длина поля не должна превышать 50 символов")]
        public string PhoneNumberEmployee { get; set; }

        [Column("specialization_employee")]
        [Required]
        [StringLength(300, ErrorMessage = "Длина поля не должна превышать 300 символов")]
        public string Specialization { get; set; }

        [Column("date_employment")]
        public DateTime EmploymentDate { get; set; }

        [Column("date_dismissal")]
        public DateTime DismissalDate { get; set; }

        [NotMapped]
        public string FullName
        {
            get
            {
                return $"{Surname} {Firstname} {Patronymic}";
            }
        }

        public ICollection<Warehouse> Warehouses { get; set; } = new List<Warehouse>();
        public ICollection<ReceptionFuel> ReceptionFuels { get; set; } = new List<ReceptionFuel>();
        public ICollection<ConsumptionFuel> ConsumptionFuels { get; set; } = new List<ConsumptionFuel>();
        public ICollection<SowingEmployee> SowingEmployees { get; set;} = new List<SowingEmployee>();
        public ICollection<Sowing> Sowings { get; set; } = new List<Sowing>();
        //public ICollection<SowingTechnique> SowingTechniques { get; set; } = new List<SowingTechnique>();
        public ICollection<HarvestingEmployee> HarvestingEmployees { get; set; }= new List<HarvestingEmployee>();
        public ICollection<Harvesting> Harvestings { get; set; } = new List<Harvesting>();
        

    }
}
