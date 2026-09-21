using SupplierCapabilitiesAndManagementSystem.Entities;
using SupplierCapabilitiesAndManagementSystem.Models.DTO;
using SupplierCapabilitiesAndManagementSystem.Repositories;

namespace SupplierCapabilitiesAndManagementSystem.Services
{
    public class QualitierService : IQualitierService
    {
        private readonly IQualitierRepository _qualitierRepository;

        public QualitierService(IQualitierRepository qualitierRepository)
        {
            _qualitierRepository = qualitierRepository;
        }

        public async Task<FetchAllQualitiersGetResponseDTO> FetchAllQualitiersAsync(string sortBy = "id_asc", int pageNumber = 1, int pageSize = 10, string? searchString = null)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 10;

            try
            {
                (List<AppUser> Data, int TotalCount) result = await _qualitierRepository.FetchAllQualitiersAsync(sortBy, pageNumber, pageSize, searchString);
                int totalPages = (int)Math.Ceiling(result.TotalCount / (double)pageSize);
                var mappedData = result.Data.Select(qualitier => qualitier.ToQualitierDTO()).ToList();

                return new FetchAllQualitiersGetResponseDTO()
                {
                    Data = mappedData,
                    TotalPages = totalPages,
                    TotalCount = result.TotalCount,
                    CurrentPage = pageNumber,
                    Message = "Qualitiers retrieved successfully.",
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                return new FetchAllQualitiersGetResponseDTO()
                {
                    Message = $"An error occurred while retrieving the qualitiers: {ex.Message}",
                    IsSuccess = false
                };
            }
        }
    }
}
