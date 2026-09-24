using Microsoft.EntityFrameworkCore;
using SupplierCapabilitiesAndManagementSystem.Database;
using SupplierCapabilitiesAndManagementSystem.Entities;

namespace SupplierCapabilitiesAndManagementSystem.Repositories
{
    public class SupplierHumanResourceRepository : ISupplierHumanResourceRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public SupplierHumanResourceRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<SupplierHumanResource?> FetchSupplierHumanResourcesAsync(int userId)
        {
            return await _dbContext.SupplierHumanResources
                .Include(supplierHumanResource => supplierHumanResource.Qualitier)
                .AsNoTracking()
                .FirstOrDefaultAsync(humanResource => humanResource.UserId == userId);
        }

        public async Task<SupplierHumanResource> EditSupplierHumanResourcesAsync(SupplierHumanResource supplierHumanResource, int userId)
        {
            var existingResource = await _dbContext.SupplierHumanResources
                .FirstOrDefaultAsync(humanResources => humanResources.UserId == userId);

            if (existingResource == null)
            {
                supplierHumanResource.UserId = userId;
                await _dbContext.SupplierHumanResources.AddAsync(supplierHumanResource);
                await _dbContext.SaveChangesAsync();

                return supplierHumanResource;
            }

            existingResource.TotalEmployeeCount = supplierHumanResource.TotalEmployeeCount;
            existingResource.WhiteCollarCount = supplierHumanResource.WhiteCollarCount;
            existingResource.BlueCollarCount = supplierHumanResource.BlueCollarCount;
            existingResource.EngineerCount = supplierHumanResource.EngineerCount;
            existingResource.QualityControlStaffCount = supplierHumanResource.QualityControlStaffCount;
            existingResource.RndStaffCount = supplierHumanResource.RndStaffCount;
            existingResource.CertifiedOperatorCount = supplierHumanResource.CertifiedOperatorCount;
            existingResource.ShiftCount = supplierHumanResource.ShiftCount;
            existingResource.WorkingDaysPerWeek = supplierHumanResource.WorkingDaysPerWeek;
            existingResource.HasLaborUnion = supplierHumanResource.HasLaborUnion;
            existingResource.EmployeeTurnoverRate = supplierHumanResource.EmployeeTurnoverRate;
            existingResource.LastUpdatedDate = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync();

            return existingResource;
        }
    }
}
