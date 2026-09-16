using TedarikciKabiliyetYonetimSistemi.Entities;

namespace TedarikciKabiliyetYonetimSistemi.Models.DTO
{
    public class FetchAllMachinesGetResponseDTO
    {
        public List<Machine> Data { get; set; } = new();
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public string Message { get; set; } = string.Empty;
        public bool IsSuccess { get; set; }
    }
}
