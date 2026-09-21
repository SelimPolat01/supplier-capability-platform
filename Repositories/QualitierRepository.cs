using Microsoft.EntityFrameworkCore;
using SupplierCapabilitiesAndManagementSystem.Database;
using SupplierCapabilitiesAndManagementSystem.Entities;

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
    }
}