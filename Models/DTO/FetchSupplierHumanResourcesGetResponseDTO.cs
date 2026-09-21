namespace SupplierCapabilitiesAndManagementSystem.Models.DTO
{
    public class FetchSupplierHumanResourcesGetResponseDTO
    {
        public SupplierHumanResourcesDTO? Data { get; set; }
        public string Message { get; set; } = string.Empty;
        public bool IsSuccess { get; set; }
    }
}
