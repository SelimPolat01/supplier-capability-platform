namespace SupplierCapabilitiesAndManagementSystem.Models.DTO
{
    public class FetchSupplierAllCertificatesGetResponseDTO
    {
        public List<SupplierCertificateDTO> Data { get; set; } = new();
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public string Message { get; set; } = string.Empty;
        public bool IsSuccess { get; set; }
    }
}
