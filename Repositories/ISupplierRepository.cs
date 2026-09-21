using SupplierCapabilitiesAndManagementSystem.Entities;

namespace SupplierCapabilitiesAndManagementSystem.Repositories
{
    public interface ISupplierRepository
    {
        public Task<(List<AppUser> Data, int TotalCount)> FetchAllSuppliersAsync(string sortBy, int pageNumber, int pageSize, string? searchString = null);

        public Task<AppUser?> FetchSupplierAsync(int supplierId);
    }
}
