using SupplierCapabilitiesAndManagementSystem.Entities;

namespace SupplierCapabilitiesAndManagementSystem.Repositories
{
    public interface ISupplierHumanResourceRepository
    {
        public Task<SupplierHumanResource> EditHumanResourcesAsync(SupplierHumanResource supplierHumanResource, int userId);

        public Task<SupplierHumanResource?> FetchSupplierHumanResourcesAsync(int userId);
    }
}
