namespace SupplierCapabilitiesAndManagementSystem.Models.DTO
{
    public class FetchAllQualitiersGetResponseDTO
    {
        public List<QualitierDTO> Data { get; set; } = new();
        public string Message { get; set; } = string.Empty;
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public bool IsSuccess { get; set; }
    }
}
