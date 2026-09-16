using System.ComponentModel.DataAnnotations;

namespace TedarikciKabiliyetYonetimSistemi.Models.DTO
{
    public class EditMachinePatchRequestDTO
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Machine Group is required.")]
        [Display(Name = "Machine Group")]
        public string MachineGroup { get; set; } = string.Empty;

        [Required(ErrorMessage = "Machine Type is required.")]
        [Display(Name = "Machine Type")]
        public string MachineType { get; set; } = string.Empty;

        [Required(ErrorMessage = "Brand / Model is required.")]
        [Display(Name = "Brand / Model")]
        public string BrandAndModel { get; set; } = string.Empty;

        [Required(ErrorMessage = "Production Year is required.")]
        [Display(Name = "Production Year")]
        public int ProductionYear { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        public int Quantity { get; set; }

        [Display(Name = "Axis Count")]
        public int? AxisCount { get; set; }

        [Required(ErrorMessage = "Capacity and dimensions are required.")]
        [Display(Name = "Capacity / Specs")]
        public string CapacitySpecs { get; set; } = string.Empty;
    }
}