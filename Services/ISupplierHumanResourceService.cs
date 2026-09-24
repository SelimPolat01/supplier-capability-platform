using SupplierCapabilitiesAndManagementSystem.Models.DTO;

namespace SupplierCapabilitiesAndManagementSystem.Services
{
    public interface ISupplierHumanResourceService
    {
        public Task<EditSupplierHumanResourcePatchResponseDTO> EditHumanResourcesAsync(EditSupplierHumanResourcePatchRequestDTO addSupplierHumanRespourcePostRequestDTO, int userId);

        public Task<FetchSupplierHumanResourceGetResponseDTO> FetchSupplierHumanResourcesAsync(int userId);
    }
}
