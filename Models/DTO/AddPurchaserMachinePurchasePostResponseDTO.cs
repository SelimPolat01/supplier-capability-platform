namespace SupplierCapabilitiesAndManagementSystem.Models.DTO
{
    public class AddPurchaserMachinePurchasePostResponseDTO
    {
        public string Message { get; set; } = null!;
        public bool IsSuccess { get; set; }
        public int? AddedMachinePurchaseId { get; set; }
    }
}
