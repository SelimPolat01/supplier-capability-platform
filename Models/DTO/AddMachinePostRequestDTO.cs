using System.ComponentModel.DataAnnotations;

namespace TedarikciKabiliyetYonetimSistemi.Models.DTO
{
    public class AddMachinePostRequestDTO
    {
        [Required(ErrorMessage = "{0} is required.")]
        [Display(Name = "Machine Group")]
        public string MachineGroup { get; set; } = string.Empty;

        [Required(ErrorMessage = "{0} is required.")]
        [Display(Name = "Machine Type")]
        public string MachineType { get; set; } = string.Empty;

        [Required(ErrorMessage = "{0} is required.")]
        [Display(Name = "Brand / Model")]
        public string BrandAndModel { get; set; } = string.Empty;

        [Required(ErrorMessage = "{0} is required.")]
        [Display(Name = "Production Year")]
        [Range(1900, 2100, ErrorMessage = "Please enter a valid production year.")]
        public int ProductionYear { get; set; } = 2026;

        [Required(ErrorMessage = "{0} is required.")]
        [Display(Name = "Quantity")]
        public int Quantity { get; set; } = 1;

        [Display(Name = "Axis Count")]
        public int? AxisCount { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        [Display(Name = "Capacity / Dimensions")]
        public string CapacitySpecs { get; set; } = string.Empty;

        [Required(ErrorMessage = "{0} is required.")]
        [Display(Name = "Machine Image File")]
        public IFormFile? ImageFile { get; set; }
    }
}
