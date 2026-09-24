using Microsoft.EntityFrameworkCore;
using SupplierCapabilitiesAndManagementSystem.Database;
using SupplierCapabilitiesAndManagementSystem.Entities;

namespace SupplierCapabilitiesAndManagementSystem.Repositories
{
    public class PurchaserRepository : IPurchaserRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public PurchaserRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<(List<AppUser> Data, int TotalCount)> FetchAllPurchasersAsync(string sortBy, int pageNumber, int pageSize, string? searchString = null)
        {
            var purchaserRoleId = await _dbContext.Roles
                .AsNoTracking()
                .Where(role => role.Name == "Purchaser")
                .Select(role => role.Id)
                .FirstOrDefaultAsync();

            if (purchaserRoleId == 0) return (new List<AppUser>(), 0);

            var query = _dbContext.Users
                .Include(user => user.PurchaserMachinePurchases)
                .ThenInclude(machinePurchase => machinePurchase.SupplierMachine)
                .AsNoTracking()
                .Where(user => _dbContext.UserRoles.Any(role => role.RoleId == purchaserRoleId && role.UserId == user.Id))
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(user =>
                user.Name.Contains(searchString) ||
                user.Surname.Contains(searchString));
            }

            int totalPurchaserCount = await query.CountAsync();

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
                "score_asc" => query.OrderBy(user => user.QualityScore).ThenBy(c => c.Id),
                "score_desc" => query.OrderByDescending(user => user.QualityScore).ThenByDescending(c => c.Id),
                "added_asc" => query.OrderBy(user => user.CreatedAt).ThenBy(c => c.Id),
                "added_desc" => query.OrderByDescending(user => user.CreatedAt).ThenByDescending(c => c.Id),
                _ => query.OrderBy(user => user.Id)
            };

            var result = await query.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (result, totalPurchaserCount);
        }

        public async Task<AppUser?> FetchPurchaserAsync(int purchaserId)
        {
            return await _dbContext.Users
                .Include(user => user.Qualitier)
                .Include(user => user.PurchaserMachinePurchases)
                .ThenInclude(purchase => purchase.SupplierMachine)
                .ThenInclude(supplierMachine => supplierMachine.AppUser)
                .AsNoTracking()
                .FirstOrDefaultAsync(user => user.Id == purchaserId);
        }

        public async Task<(List<PurchaserMachinePurchase> Data, int TotalCount)> FetchPurchasersAllPurchaseAsync(int purchaserId, string sortBy, int pageNumber, int pageSize, string? searchString = null)
        {
            var query = _dbContext.PurchaserMachinePurchases
           .Include(machinePurchase => machinePurchase.Purchaser)
           .Include(machinePurchase => machinePurchase.SupplierMachine)
           .ThenInclude(supplierMachine => supplierMachine.AppUser)
           .AsNoTracking()
           .Where(machinePurchase => machinePurchase.PurchaserId == purchaserId);

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(machinePurchase =>
                    machinePurchase.SupplierMachine.MachineGroup.Contains(searchString) ||
                    machinePurchase.SupplierMachine.BrandAndModel.Contains(searchString) ||
                    machinePurchase.SupplierMachine.MachineType.Contains(searchString) ||
                    machinePurchase.SupplierMachine.CapacitySpecs.Contains(searchString));
            }

            int totalMachinePurchaseCount = await query.CountAsync();

            query = sortBy switch
            {
                "id_asc" => query.OrderBy(mp => mp.Id),
                "id_desc" => query.OrderByDescending(mp => mp.Id),
                "supplier-id_asc" => query.OrderBy(mp => mp.SupplierMachine.AppUserId).ThenBy(mp => mp.Id),
                "supplier-id_desc" => query.OrderByDescending(mp => mp.SupplierMachine.AppUserId).ThenByDescending(mp => mp.Id),
                "cost_asc" => query.OrderBy(mp => mp.TotalPurchasedPrice).ThenBy(m => m.Id),
                "cost_desc" => query.OrderByDescending(mp => mp.TotalPurchasedPrice).ThenByDescending(m => m.Id),
                "score_asc" => query.OrderBy(mp => mp.QualityScore).ThenBy(m => m.Id),
                "score_desc" => query.OrderByDescending(mp => mp.QualityScore).ThenByDescending(m => m.Id),
                "added_asc" => query.OrderBy(mp => mp.PurchaseDate).ThenBy(m => m.Id),
                "added_desc" => query.OrderByDescending(mp => mp.PurchaseDate).ThenByDescending(m => m.Id),
                "group_asc" => query.OrderBy(mp => mp.SupplierMachine.MachineGroup).ThenBy(m => m.Id),
                "group_desc" => query.OrderByDescending(mp => mp.SupplierMachine.MachineGroup).ThenByDescending(m => m.Id),
                "type_asc" => query.OrderBy(mp => mp.SupplierMachine.MachineType).ThenBy(m => m.Id),
                "type_desc" => query.OrderByDescending(mp => mp.SupplierMachine.MachineType).ThenByDescending(m => m.Id),
                "brand-model_asc" => query.OrderBy(mp => mp.SupplierMachine.BrandAndModel).ThenBy(m => m.Id),
                "brand-model_desc" => query.OrderByDescending(mp => mp.SupplierMachine.BrandAndModel).ThenByDescending(m => m.Id),
                "production-year_asc" => query.OrderBy(mp => mp.SupplierMachine.ProductionYear).ThenBy(m => m.Id),
                "production-year_desc" => query.OrderByDescending(mp => mp.SupplierMachine.ProductionYear).ThenByDescending(m => m.Id),
                "quantity_asc" => query.OrderBy(mp => mp.Quantity).ThenBy(m => m.Id),
                "quantity_desc" => query.OrderByDescending(mp => mp.Quantity).ThenByDescending(m => m.Id),
                "axis-count_asc" => query.OrderBy(mp => mp.SupplierMachine.AxisCount).ThenBy(m => m.Id),
                "axis-count_desc" => query.OrderByDescending(mp => mp.SupplierMachine.AxisCount).ThenByDescending(m => m.Id),
                "capacity_asc" => query.OrderBy(mp => mp.SupplierMachine.CapacitySpecs).ThenBy(m => m.Id),
                "capacity_desc" => query.OrderByDescending(mp => mp.SupplierMachine.CapacitySpecs).ThenByDescending(m => m.Id),
                _ => query.OrderBy(mp => mp.Id)
            };

            var result = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (result, totalMachinePurchaseCount);
        }

        public async Task<PurchaserMachinePurchase?> FetchPurchaserPurchaseAsync(int purchaseId)
        {
            return await _dbContext.PurchaserMachinePurchases
                .Include(machinePurchase => machinePurchase.Qualitier)
                .Include(machinePurchase => machinePurchase.Purchaser)
                .Include(machinePurchase => machinePurchase.SupplierMachine)
                .ThenInclude(supplierMachine => supplierMachine.AppUser)
                .AsNoTracking()
                .FirstOrDefaultAsync(machinePurchase => machinePurchase.Id == purchaseId);
        }

        public async Task<(int PurchaseId, string ErrorMessage)> AddPurchaserMachinePurchaseAsync(int purchaserId, int machineId, int quantity = 1)
        {
            var purchaserRoleId = await _dbContext.Roles.
                AsNoTracking().
                Where(role => role.Name == "Purchaser")
                .Select(role => role.Id)
                .FirstOrDefaultAsync();

            bool isPurchaser = await _dbContext.UserRoles
                .AnyAsync(userRole => userRole.RoleId == purchaserRoleId && userRole.UserId == purchaserId);

            if (!isPurchaser) return (0, "Unauthorized: You do not have purchaser permissions.");

            var machine = await _dbContext.SupplierMachines.
                FirstOrDefaultAsync(supplierMachine => supplierMachine.Id == machineId);

            if (machine == null) return (0, "Machine not found.");
            if (machine.Quantity == 0) return (0, "This machine is currently out of stock.");
            if (machine.Quantity < quantity) return (0, $"Insufficient stock. Only {machine.Quantity} items left.");

            var newPurchase = new PurchaserMachinePurchase()
            {
                PurchaserId = purchaserId,
                SupplierMachineId = machineId,
                Quantity = quantity,
                PurchasedPrice = machine.Price,
                TotalPurchasedPrice = machine.Price * quantity,
                PurchaseDate = DateTime.UtcNow,
            };

            machine.Quantity -= quantity;

            await _dbContext.PurchaserMachinePurchases.AddAsync(newPurchase);
            var savedAsync = await _dbContext.SaveChangesAsync();

            return savedAsync > 0 ? (newPurchase.Id, string.Empty) : (0, "An error occurred while saving to the database.");
        }
    }
}
