namespace SupplierCapabilitiesAndManagementSystem.Models.DTO
{
    public class AddSupplierMachinePostResponseDTO
    {
        public string Message { get; set; } = null!;
        public bool IsSuccess { get; set; }
        public int? AddedMachineId { get; set; }
    }
}
