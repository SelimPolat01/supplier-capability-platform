using Microsoft.EntityFrameworkCore;
using SupplierCapabilitiesAndManagementSystem.Database;
using SupplierCapabilitiesAndManagementSystem.Entities;

namespace SupplierCapabilitiesAndManagementSystem.Repositories
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public SupplierRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<(List<AppUser> Data, int TotalCount)> FetchAllSuppliersAsync(string sortBy, int pageNumber, int pageSize, string? searchString = null)
        {
            var supplierRoleId = await _dbContext.Roles
                .AsNoTracking()
                .Where(role => role.Name == "Supplier")
                .Select(role => role.Id)
                .FirstOrDefaultAsync();

            if (supplierRoleId == 0) return (new List<AppUser>(), 0);

            var query = _dbContext.Users
                .AsNoTracking()
                .Where(u => _dbContext.UserRoles.Any(ur => ur.UserId == u.Id && ur.RoleId == supplierRoleId))
                .Include(u => u.SupplierCertificates)
                .Include(u => u.SupplierMachines)
                .Include(u => u.SupplierHumanResources)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(supplier =>
                    (supplier.Name != null && supplier.Name.Contains(searchString)) ||
                    (supplier.Surname != null && supplier.Surname.Contains(searchString)) ||
                    (supplier.CompanyName != null && supplier.CompanyName.Contains(searchString)) ||
                    (supplier.Email != null && supplier.Email.Contains(searchString)) ||
                    supplier.SupplierCertificates.Any(c => c.Name != null && c.Name.Contains(searchString)) ||
                    supplier.SupplierMachines.Any(m => m.BrandAndModel != null && m.BrandAndModel.Contains(searchString))
                );
            }

            int totalSupplierCount = await query.CountAsync();

            query = sortBy switch
            {
                "id_asc" => query.OrderBy(u => u.Id),
                "id_desc" => query.OrderByDescending(u => u.Id),
                "name_asc" => query.OrderBy(u => u.Name),
                "name_desc" => query.OrderByDescending(u => u.Name),
                "surname_asc" => query.OrderBy(u => u.Surname),
                "surname_desc" => query.OrderByDescending(u => u.Surname),
                "company-name_asc" => query.OrderBy(u => u.CompanyName),
                "company-name_desc" => query.OrderByDescending(u => u.CompanyName),
                "email_asc" => query.OrderBy(u => u.Email),
                "email_desc" => query.OrderByDescending(u => u.Email),
                "certificates_asc" => query.OrderBy(u => u.SupplierCertificates.Count),
                "certificates_desc" => query.OrderByDescending(u => u.SupplierCertificates.Count),
                "machines_asc" => query.OrderBy(u => u.SupplierMachines.Count),
                "machines_desc" => query.OrderByDescending(u => u.SupplierMachines.Count),
                "human-resources_asc" => query.OrderBy(u => u.SupplierHumanResources.Count),
                "human-resources_desc" => query.OrderByDescending(u => u.SupplierHumanResources.Count),
                "added_asc" => query.OrderBy(u => u.CreatedAt),
                "added_desc" => query.OrderByDescending(u => u.CreatedAt),
                _ => query.OrderBy(u => u.Id)
            };

            var suppliers = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (suppliers, totalSupplierCount);
        }

        public async Task<AppUser?> FetchSupplierAsync(int supplierId)
        {
            return await _dbContext.Users
                .AsNoTracking()
                .AsSplitQuery()
                .Include(user => user.SupplierMachines)
                .Include(user => user.SupplierCertificates)
                .Include(user => user.SupplierHumanResources)
                .FirstOrDefaultAsync(user => user.Id == supplierId);
        }
    }
}