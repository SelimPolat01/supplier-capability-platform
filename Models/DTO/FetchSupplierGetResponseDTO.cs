namespace SupplierCapabilitiesAndManagementSystem.Models.DTO
{
    public class FetchSupplierGetResponseDTO
    {
        public SupplierDetailDTO Data { get; set; } = new();
        public string Message { get; set; } = string.Empty;
        public bool IsSuccess { get; set; }
    }
}
