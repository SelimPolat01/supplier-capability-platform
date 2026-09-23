using SupplierCapabilitiesAndManagementSystem.Entities;

namespace SupplierCapabilitiesAndManagementSystem.Repositories
{
    public interface ISupplierHumanResourceRepository
    {
        public Task<SupplierHumanResource?> FetchSupplierHumanResourcesAsync(int userId);
        public Task<SupplierHumanResource> EditSupplierHumanResourcesAsync(SupplierHumanResource supplierHumanResource, int userId);
    }
}
