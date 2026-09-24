using System.ComponentModel.DataAnnotations;

namespace SupplierCapabilitiesAndManagementSystem.Models.DTO
{
    public class SupplierDTO
    {
        public List<SupplierMachineDTO> Machines { get; set; } = new();
        public List<SupplierCertificateDTO> Certificates { get; set; } = new();
        public List<SupplierHumanResourceDTO> HumanResources { get; set; } = new();
        public int Id { get; set; }

        [Display(Name = "First Name")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Last Name")]
        public string Surname { get; set; } = string.Empty;

        [Display(Name = "Company Name")]
        public string CompanyName { get; set; } = string.Empty;

        [Display(Name = "Email Address")]
        public string Email { get; set; } = string.Empty;

        [Display(Name = "Registration Date")]
        public DateTime CreatedAt { get; set; }
    }
}
