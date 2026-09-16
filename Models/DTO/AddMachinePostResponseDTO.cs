namespace TedarikciKabiliyetYonetimSistemi.Models.DTO
{
    public class AddMachinePostResponseDTO
    {
        public string Message { get; set; } = null!;
        public bool IsSuccess { get; set; }
        public int? AddedMachineId { get; set; }
    }
}
