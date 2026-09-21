using SupplierCapabilitiesAndManagementSystem.Entities;

namespace SupplierCapabilitiesAndManagementSystem.Repositories
{
    public interface IQualitierRepository
    {
        public Task<(List<AppUser> Data, int TotalCount)> FetchAllQualitiersAsync(string sortBy, int pageNumber, int pageSize, string? searchString = null);
    }
}
