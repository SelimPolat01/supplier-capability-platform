using SupplierCapabilitiesAndManagementSystem.Entities;
using SupplierCapabilitiesAndManagementSystem.Models.DTO;
using SupplierCapabilitiesAndManagementSystem.Repositories;

namespace SupplierCapabilitiesAndManagementSystem.Services
{
    public class SupplierHumanResourceService : ISupplierHumanResourceService
    {
        private readonly ISupplierHumanResourceRepository _supplierHumanResourceRepository;

        public SupplierHumanResourceService(ISupplierHumanResourceRepository supplierHumanResourceRepository)
        {
            _supplierHumanResourceRepository = supplierHumanResourceRepository;
        }

        public async Task<EditSupplierHumanResourcePatchResponseDTO> EditHumanResourcesAsync(EditSupplierHumanResourcePatchRequestDTO addSupplierHumanRespourcePostRequestDTO, int userId)
        {
            try
            {
                SupplierHumanResource supplierHumanResource = addSupplierHumanRespourcePostRequestDTO.ToSupplierHumanResource(userId);
                SupplierHumanResource addedSupplierHumanResource = await _supplierHumanResourceRepository.EditHumanResourcesAsync(supplierHumanResource, userId);

                return addedSupplierHumanResource.ToEditSupplierHumanResourcePatchResponseDTO(message: "Supplier human resources saved successfully.", isSuccess: true);
            }
            catch (Exception ex)
            {
                return new EditSupplierHumanResourcePatchResponseDTO
                {
                    IsSuccess = false,
                    Message = $"An error occurred: {ex.Message}"
                };
            }
        }

        public async Task<FetchSupplierHumanResourcesGetResponseDTO> FetchSupplierHumanResourcesAsync(int userId)
        {
            try
            {
                SupplierHumanResource? supplierHumanResource = await _supplierHumanResourceRepository.FetchSupplierHumanResourcesAsync(userId);

                if (supplierHumanResource == null) return new FetchSupplierHumanResourcesGetResponseDTO()
                {
                    Data = null,
                    Message = "No human resources data found for the user.",
                    IsSuccess = false,
                };

                return supplierHumanResource.ToFetchSupplierHumanResourcesGetResponseDTO("Supplier human resources fetched successfully.", true);
            }
            catch (Exception ex)
            {
                return new FetchSupplierHumanResourcesGetResponseDTO()
                {
                    Data = null,
                    Message = $"An error occurred: {ex.Message}",
                    IsSuccess = false
                };
            }
        }
    }
}
