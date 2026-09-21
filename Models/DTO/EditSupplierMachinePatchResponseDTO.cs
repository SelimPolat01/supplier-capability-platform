namespace SupplierCapabilitiesAndManagementSystem.Models.DTO
{
    public class EditSupplierMachinePatchResponseDTO
    {
        public string Message { get; set; } = null!;
        public bool IsSuccess { get; set; }
        public int? EditedMachineId { get; set; }
    }
}
