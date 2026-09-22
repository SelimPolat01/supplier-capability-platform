using SupplierCapabilitiesAndManagementSystem.Entities;
using SupplierCapabilitiesAndManagementSystem.Models.DTO;
using SupplierCapabilitiesAndManagementSystem.Repositories;

namespace SupplierCapabilitiesAndManagementSystem.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;

        public SupplierService(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public async Task<FetchAllSuppliersGetResponseDTO> FetchAllSuppliersAsync(string sortBy = "id_asc", int pageNumber = 1, int pageSize = 10, string? searchString = null)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 10;

            try
            {
                (List<AppUser> Data, int TotalCount) result = await _supplierRepository.FetchAllSuppliersAsync(sortBy, pageNumber, pageSize, searchString);
                int totalPages = (int)Math.Ceiling(result.TotalCount / (double)pageSize);
                var mappedData = result.Data.Select(supplier => supplier.ToSupplierDTO()).ToList();

                return new FetchAllSuppliersGetResponseDTO()
                {
                    Data = mappedData,
                    TotalCount = result.TotalCount,
                    TotalPages = totalPages,
                    CurrentPage = pageNumber,
                    Message = "Suppliers retrieved successfully.",
                    IsSuccess = true
                };

            }
            catch (Exception ex)
            {
                return new FetchAllSuppliersGetResponseDTO()
                {
                    Message = $"An error occurred while retrieving the suppliers: {ex.Message}",
                    IsSuccess = false
                };
            }
        }

        public async Task<FetchSupplierGetResponseDTO> FetchSupplierAsync(int supplierId)
        {
            try
            {
                AppUser? existingSupplier = await _supplierRepository.FetchSupplierAsync(supplierId);
                if (existingSupplier == null)
                {
                    return new FetchSupplierGetResponseDTO()
                    {
                        Message = "Supplier not found.",
                        IsSuccess = false
                    };
                }

                var mappedData = existingSupplier.ToSupplierDTO();

                return new FetchSupplierGetResponseDTO()
                {
                    Data = mappedData,
                    Message = "Supplier details retrieved successfully.",
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                return new FetchSupplierGetResponseDTO()
                {
                    Message = $"An error occurred while retrieving the supplier: {ex.Message}",
                    IsSuccess = false
                };
            }
        }
    }
}
