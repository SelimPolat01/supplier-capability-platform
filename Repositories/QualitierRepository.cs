using Microsoft.EntityFrameworkCore;
using SupplierCapabilitiesAndManagementSystem.Database;
using SupplierCapabilitiesAndManagementSystem.Entities;
using SupplierCapabilitiesAndManagementSystem.Enums;

namespace SupplierCapabilitiesAndManagementSystem.Repositories
{
    public class QualitierRepository : IQualitierRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public QualitierRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<(List<AppUser> Data, int TotalCount)> FetchAllQualitiersAsync(string sortBy, int pageNumber, int pageSize, string? searchString = null)
        {
            var qualitierRoleId = await _dbContext.Roles
                .AsNoTracking()
                .Where(role => role.Name == "Qualitier")
                .Select(role => role.Id)
                .FirstOrDefaultAsync();

            if (qualitierRoleId == 0) return (new List<AppUser>(), 0);

            var query = _dbContext.Users.
                AsNoTracking()
                .Where(user => _dbContext.UserRoles.Any(role => role.RoleId == qualitierRoleId && role.UserId == user.Id))
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(user =>
                user.Name.Contains(searchString) ||
                user.Surname.Contains(searchString));
            }

            int totalQualitierCount = await query.CountAsync();

            query = sortBy switch
            {
                "id_asc" => query.OrderBy(user => user.Id),
                "id_desc" => query.OrderByDescending(user => user.Id),
                "name_asc" => query.OrderBy(user => user.Name).ThenBy(c => c.Id),
                "name_desc" => query.OrderByDescending(user => user.Name).ThenByDescending(c => c.Id),
                "surname_asc" => query.OrderBy(user => user.Surname).ThenBy(c => c.Id),
                "surname_desc" => query.OrderByDescending(user => user.Surname).ThenByDescending(c => c.Id),
                "email_asc" => query.OrderBy(user => user.Email).ThenBy(c => c.Id),
                "email_desc" => query.OrderByDescending(user => user.Email).ThenByDescending(c => c.Id),
                "added_asc" => query.OrderBy(user => user.CreatedAt).ThenBy(c => c.Id),
                "added_desc" => query.OrderByDescending(user => user.CreatedAt).ThenByDescending(c => c.Id),
                _ => query.OrderBy(user => user.Id)
            };

            var result = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (result, totalQualitierCount);
        }

        public async Task<AppUser?> FetchQualitierAsync(int userId, int qualitierId)
        {
            var adminRoleId = await _dbContext.Roles
                .AsNoTracking()
                .Where(role => role.Name == "Admin")
                .Select(role => role.Id)
                .FirstOrDefaultAsync();

            if (adminRoleId == 0) return null;

            var isAdmin = await _dbContext.UserRoles
                .AsNoTracking()
                .AnyAsync(role => role.RoleId == adminRoleId && role.UserId == userId);

            if (!isAdmin) return null;

            return await _dbContext.Users
                .AsNoTracking()
                .AsSplitQuery()
                .Include(qualitier => qualitier.QualitierSupplierScores)
                .Include(qualitier => qualitier.QualitierPurchaserPurchaseScores)
                .ThenInclude(qualitierPurchaserPurchaserScore => qualitierPurchaserPurchaserScore.SupplierMachine)
                .Include(qualitier => qualitier.QualitierSupplierCertificateScores)
                .Include(qualitier => qualitier.QualitierSupplierMachineScores)
                .Include(qualitier => qualitier.QualitierSupplierHumanResourceScores)
                .FirstOrDefaultAsync(user => user.Id == qualitierId);
        }

        public async Task<bool> EditSupplierMachineQualityAsync(int userId, int machineId, QualityScore? qualityScore)
        {
            var qualitierRoleId = await _dbContext.Roles
                .AsNoTracking()
                .Where(role => role.Name == "Qualitier")
                .Select(role => role.Id)
                .FirstOrDefaultAsync();

            if (qualitierRoleId == 0) return false;

            var hasQualitierRole = await _dbContext.UserRoles
                .AnyAsync(userRoles => userRoles.UserId == userId && userRoles.RoleId == qualitierRoleId);

            if (!hasQualitierRole) return false;

            var machine = await _dbContext.SupplierMachines.FirstOrDefaultAsync(supplierMachine => supplierMachine.Id == machineId);

            if (machine == null) return false;

            machine.QualityScore = qualityScore;
            machine.QualitierId = userId;
            machine.LastScoreUpdate = DateTime.UtcNow;

            var result = await _dbContext.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> EditSupplierCertificateQualityAsync(int userId, int certificateId, QualityScore? qualityScore)
        {
            var qualitierRoleId = await _dbContext.Roles
                .AsNoTracking()
                .Where(role => role.Name == "Qualitier")
                .Select(role => role.Id)
                .FirstOrDefaultAsync();

            if (qualitierRoleId == 0) return false;

            var hasQualitierRole = await _dbContext.UserRoles
                .AnyAsync(userRoles => userRoles.UserId == userId && userRoles.RoleId == qualitierRoleId);

            if (!hasQualitierRole) return false;

            var certificate = await _dbContext.SupplierCertificates.FirstOrDefaultAsync(supplierCertificate => supplierCertificate.Id == certificateId);

            if (certificate == null) return false;

            certificate.QualityScore = qualityScore;
            certificate.QualitierId = userId;
            certificate.LastScoreUpdate = DateTime.UtcNow;

            var result = await _dbContext.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> EditSupplierHumanResourceQualityAsync(int userId, int humanResourceId, QualityScore? qualityScore)
        {
            var qualitierRoleId = await _dbContext.Roles
                .AsNoTracking()
                .Where(role => role.Name == "Qualitier")
                .Select(role => role.Id)
                .FirstOrDefaultAsync();

            if (qualitierRoleId == 0) return false;

            var hasQualitierRole = await _dbContext.UserRoles
                .AnyAsync(userRoles => userRoles.UserId == userId && userRoles.RoleId == qualitierRoleId);

            if (!hasQualitierRole) return false;

            var humanResource = await _dbContext.SupplierHumanResources.FirstOrDefaultAsync(supplierHumanResource => supplierHumanResource.Id == humanResourceId);

            if (humanResource == null) return false;

            humanResource.QualityScore = qualityScore;
            humanResource.QualitierId = userId;
            humanResource.LastScoreUpdate = DateTime.UtcNow;

            var result = await _dbContext.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> EditSupplierQualityAsync(int userId, int supplierId, QualityScore? qualityScore)
        {
            var qualitierRoleId = await _dbContext.Roles
                .AsNoTracking()
                .Where(role => role.Name == "Qualitier")
                .Select(role => role.Id)
                .FirstOrDefaultAsync();

            if (qualitierRoleId == 0) return false;

            var hasQualitierRole = await _dbContext.UserRoles
                .AnyAsync(userRoles => userRoles.UserId == userId && userRoles.RoleId == qualitierRoleId);

            if (!hasQualitierRole) return false;

            var supplier = await _dbContext.Users.FirstOrDefaultAsync(user => user.Id == supplierId);

            if (supplier == null) return false;

            supplier.QualityScore = qualityScore;
            supplier.QualitierId = userId;
            supplier.LastScoreUpdate = DateTime.UtcNow;

            var result = await _dbContext.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> EditPurchaserQualityAsync(int userId, int purchaserId, QualityScore? qualityScore)
        {
            var qualitierRoleId = await _dbContext.Roles
                .AsNoTracking()
                .Where(role => role.Name == "Qualitier")
                .Select(role => role.Id)
                .FirstOrDefaultAsync();

            if (qualitierRoleId == 0) return false;

            var hasQualitierRole = await _dbContext.UserRoles
                .AnyAsync(userRoles => userRoles.UserId == userId && userRoles.RoleId == qualitierRoleId);

            if (!hasQualitierRole) return false;

            var purchaser = await _dbContext.Users.FirstOrDefaultAsync(user => user.Id == purchaserId);

            if (purchaser == null) return false;

            purchaser.QualityScore = qualityScore;
            purchaser.QualitierId = userId;
            purchaser.LastScoreUpdate = DateTime.UtcNow;

            var result = await _dbContext.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> EditPurchaserPurchaseQualityAsync(int userId, int purchaseId, QualityScore? qualityScore)
        {
            var qualitierRoleId = await _dbContext.Roles
                .AsNoTracking()
                .Where(role => role.Name == "Qualitier")
                .Select(role => role.Id)
                .FirstOrDefaultAsync();

            if (qualitierRoleId == 0) return false;

            var hasQualitierRole = await _dbContext.UserRoles
                .AnyAsync(userRoles => userRoles.UserId == userId && userRoles.RoleId == qualitierRoleId);

            if (!hasQualitierRole) return false;

            var machinePurchase = await _dbContext.PurchaserMachinePurchases.FirstOrDefaultAsync(machinePurchase => machinePurchase.Id == purchaseId);

            if (machinePurchase == null) return false;

            machinePurchase.QualityScore = qualityScore;
            machinePurchase.QualitierId = userId;
            machinePurchase.LastScoreUpdate = DateTime.UtcNow;

            var result = await _dbContext.SaveChangesAsync();

            return result > 0;
        }
    }
}