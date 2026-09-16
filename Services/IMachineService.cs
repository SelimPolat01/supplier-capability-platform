using TedarikciKabiliyetYonetimSistemi.Entities;
using TedarikciKabiliyetYonetimSistemi.Models.DTO;

namespace TedarikciKabiliyetYonetimSistemi.Services
{
    public interface IMachineService
    {
        public Task<AddMachinePostResponseDTO> AddMachineAsync(int userId, AddMachinePostRequestDTO addMachinePostRequestDTO);

        public Task<FetchAllMachinesGetResponseDTO> FetchAllMachinesAsync(int userId, int pageNumber = 1, int pageSize = 10, string sortBy = "id_asc", string? searchString = null);

        public Task<Machine?> FetchMachineAsync(int userId, int machineId);

        public Task<EditMachinePatchResponseDTO> EditMachineAsync(EditMachinePatchRequestDTO editMachinePatchRequestDTO, int userId);

        public Task<RemoveMachineDeleteResponseDTO> RemoveMachineAsync(int userId, int machineId);
    }
}
