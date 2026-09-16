using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TedarikciKabiliyetYonetimSistemi.Models.DTO;

namespace TedarikciKabiliyetYonetimSistemi.Entities
{
    public class Machine
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int AppUserId { get; set; }

        [ForeignKey("AppUserId")]
        public virtual AppUser AppUser { get; set; } = null!;

        [Required(ErrorMessage = "{0} is required.")]
        [Display(Name = "Machine Group", Prompt = "e.g., CNC Machining")]
        public string MachineGroup { get; set; } = string.Empty;

        [Required(ErrorMessage = "{0} is required.")]
        [Display(Name = "Machine Type", Prompt = "e.g., CNC Lathe")]
        public string MachineType { get; set; } = string.Empty;

        [Required(ErrorMessage = "{0} is required.")]
        [Display(Name = "Brand / Model", Prompt = "e.g., DMG Mori CTX 310")]
        public string BrandAndModel { get; set; } = string.Empty;

        [Required(ErrorMessage = "{0} is required.")]
        [Display(Name = "Production Year")]
        [Range(1900, 2100, ErrorMessage = "Please enter a valid production year.")]
        public int ProductionYear { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        [Display(Name = "Quantity")]
        public int Quantity { get; set; } = 1;

        [Display(Name = "Axis Count", Prompt = "e.g., 3, 4, or 5")]
        public int? AxisCount { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        [Display(Name = "Capacity / Dimensions", Prompt = "Enter dimension and tonnage")]
        public string CapacitySpecs { get; set; } = string.Empty;

        [Required(ErrorMessage = "{0} is required.")]
        [Display(Name = "Machine Image File")]
        public string ImageUrl { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

    public static class MachineExtensions
    {
        public static Machine ToMachine(this AddMachinePostRequestDTO addMachinePostRequestDTO, int userId, string imageUrl)
        {
            return new Machine()
            {
                AppUserId = userId,
                MachineGroup = addMachinePostRequestDTO.MachineGroup,
                MachineType = addMachinePostRequestDTO.MachineType,
                BrandAndModel = addMachinePostRequestDTO.BrandAndModel,
                AxisCount = addMachinePostRequestDTO.AxisCount,
                ProductionYear = addMachinePostRequestDTO.ProductionYear,
                Quantity = addMachinePostRequestDTO.Quantity,
                CapacitySpecs = addMachinePostRequestDTO.CapacitySpecs,
                ImageUrl = imageUrl,
                CreatedAt = DateTime.UtcNow,
            };
        }

        public static AddMachinePostResponseDTO ToAddMachinePostResponseDTO(this Machine machine, string message, bool isSuccess)
        {
            return new AddMachinePostResponseDTO()
            {
                Message = message,
                IsSuccess = isSuccess,
                AddedMachineId = machine.Id,
            };
        }

        public static Machine ToMachine(this EditMachinePatchRequestDTO editMachinePatchRequestDTO, int userId)
        {
            return new Machine()
            {
                Id = editMachinePatchRequestDTO.Id,
                AppUserId = userId,
                MachineGroup = editMachinePatchRequestDTO.MachineGroup,
                MachineType = editMachinePatchRequestDTO.MachineType,
                ProductionYear = editMachinePatchRequestDTO.ProductionYear,
                Quantity = editMachinePatchRequestDTO.Quantity,
                BrandAndModel = editMachinePatchRequestDTO.BrandAndModel,
                CapacitySpecs = editMachinePatchRequestDTO.CapacitySpecs,
                AxisCount = editMachinePatchRequestDTO.AxisCount,
            };
        }
    }
}