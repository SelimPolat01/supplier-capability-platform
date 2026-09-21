using SupplierCapabilitiesAndManagementSystem.Entities;
using SupplierCapabilitiesAndManagementSystem.Models.DTO;

namespace SupplierCapabilitiesAndManagementSystem.Services
{
    public interface ISupplierMachineService
    {
        public Task<FetchAllSuppliersMachinesGetResponseDTO> FetchAllSupplierMachinesAsync(int pageNumber = 1, int pageSize = 10, string sortBy = "id_asc", string? searchString = null);


        public Task<FetchSupplierAllMachinesGetResponseDTO> FetchAllMachinesAsync(int userId, int pageNumber = 1, int pageSize = 10, string sortBy = "id_asc", string? searchString = null);

        public Task<SupplierMachine?> FetchMachineAsync(int userId, int machineId);

        public Task<AddSupplierMachinePostResponseDTO> AddMachineAsync(int userId, AddSupplierMachinePostRequestDTO addMachinePostRequestDTO);

        public Task<EditSupplierMachinePatchResponseDTO> EditMachineAsync(EditSupplierMachinePatchRequestDTO editMachinePatchRequestDTO, int userId);

        public Task<RemoveSupplierMachineDeleteResponseDTO> RemoveMachineAsync(int userId, int machineId);
    }
}
