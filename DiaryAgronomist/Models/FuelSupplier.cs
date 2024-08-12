using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiaryAgronomist.Models
{
    [Table(name: "fuel_supplier", Schema ="public")]
    public class FuelSupplier
    {
        [Column("id_supplier")]
        [Key]
        public int SupplierId { get; set; }

        [Column("name_organization")]
        [Required]
        [StringLength(300, ErrorMessage = "Длина поля не должна превышать 300 символов")]
        public string NameOrganization { get; set; }

        [Column("address_organization")]
        [Required]
        [StringLength(500, ErrorMessage = "Длина поля не должна превышать 500 символов")]
        public string AddressOrganization { get; set; }

        [Column("phone_number")]
        [Required]
        [StringLength(50, ErrorMessage = "Длина поля не должна превышать 50 символов")]
        public string PhoneNumberOrganization { get; set; }

        public ICollection<ReceptionFuel> receptionFuels { get; set; } = new List<ReceptionFuel>();
    }
}
