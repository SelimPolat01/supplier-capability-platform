using Microsoft.EntityFrameworkCore;
using SupplierCapabilitiesAndManagementSystem.Database;
using SupplierCapabilitiesAndManagementSystem.Entities;

namespace SupplierCapabilitiesAndManagementSystem.Repositories
{
    public class SupplierCertificateRepository : ISupplierCertificateRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public SupplierCertificateRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<(List<SupplierCertificate> Data, int TotalCount)> FetchAllSuppliersCertificatesAsync(int pageNumber, int pageSize, string sortBy, string? searchString = null)
        {
            var query = _dbContext.SupplierCertificates.AsNoTracking();

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(supplierCertificate =>
                supplierCertificate.Name.Contains(searchString) ||
                supplierCertificate.IssuedBy.Contains(searchString));
            }

            int totalSupplierCertificateCount = await query.CountAsync();

            query = sortBy switch
            {
                "id_asc" => query.OrderBy(certificate => certificate.Id),
                "id_desc" => query.OrderByDescending(certificate => certificate.Id),
                "name_asc" => query.OrderBy(certificate => certificate.Name).ThenBy(c => c.Id),
                "name_desc" => query.OrderByDescending(certificate => certificate.Name).ThenByDescending(c => c.Id),
                "issued-by_asc" => query.OrderBy(certificate => certificate.IssuedBy).ThenBy(c => c.Id),
                "issued-by_desc" => query.OrderByDescending(certificate => certificate.IssuedBy).ThenByDescending(c => c.Id),
                "issue-date_asc" => query.OrderBy(certificate => certificate.IssueDate).ThenBy(c => c.Id),
                "issue-date_desc" => query.OrderByDescending(certificate => certificate.IssueDate).ThenByDescending(c => c.Id),
                "expiry-date_asc" => query.OrderBy(certificate => certificate.ExpiryDate).ThenBy(c => c.Id),
                "expiry-date_desc" => query.OrderByDescending(certificate => certificate.ExpiryDate).ThenByDescending(c => c.Id),
                "added_asc" => query.OrderBy(certificate => certificate.CreatedAt).ThenBy(c => c.Id),
                "added_desc" => query.OrderByDescending(certificate => certificate.CreatedAt).ThenByDescending(c => c.Id),
                _ => query.OrderBy(certificate => certificate.Id)
            };

            var result = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (result, totalSupplierCertificateCount);
        }

        public async Task<(List<SupplierCertificate> Data, int TotalCount)> FetchAllCertificatesAsync(int userId, int pageNumber, int pageSize, string sortBy, string? searchString = null)
        {
            var query = _dbContext.SupplierCertificates
                .AsNoTracking()
                .Where(certificate => certificate.AppUserId == userId);

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(certificate =>
                certificate.Name.Contains(searchString) ||
                certificate.IssuedBy.Contains(searchString));
            }
            ;

            int totalCertificateCount = await query.CountAsync();

            query = sortBy switch
            {
                "id_asc" => query.OrderBy(certificate => certificate.Id),
                "id_desc" => query.OrderByDescending(certificate => certificate.Id),
                "name_asc" => query.OrderBy(certificate => certificate.Name).ThenBy(c => c.Id),
                "name_desc" => query.OrderByDescending(certificate => certificate.Name).ThenByDescending(c => c.Id),
                "issued-by_asc" => query.OrderBy(certificate => certificate.IssuedBy).ThenBy(c => c.Id),
                "issued-by_desc" => query.OrderByDescending(certificate => certificate.IssuedBy).ThenByDescending(c => c.Id),
                "issue-date_asc" => query.OrderBy(certificate => certificate.IssueDate).ThenBy(c => c.Id),
                "issue-date_desc" => query.OrderByDescending(certificate => certificate.IssueDate).ThenByDescending(c => c.Id),
                "expiry-date_asc" => query.OrderBy(certificate => certificate.ExpiryDate).ThenBy(c => c.Id),
                "expiry-date_desc" => query.OrderByDescending(certificate => certificate.ExpiryDate).ThenByDescending(c => c.Id),
                "added_asc" => query.OrderBy(certificate => certificate.CreatedAt).ThenBy(c => c.Id),
                "added_desc" => query.OrderByDescending(certificate => certificate.CreatedAt).ThenByDescending(c => c.Id),
                _ => query.OrderBy(certificate => certificate.Id)
            };

            var result = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (result, totalCertificateCount);
        }

        public async Task<SupplierCertificate?> FetchCertificateAsync(int userId, int certificateId)
        {
            SupplierCertificate? existingCertificate = await _dbContext.SupplierCertificates
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == certificateId && c.AppUserId == userId);

            if (existingCertificate == null)
            {
                return null;
            }

            return existingCertificate;
        }

        public async Task<SupplierCertificate> AddCertificateAsync(SupplierCertificate certificate)
        {
            await _dbContext.SupplierCertificates.AddAsync(certificate);
            await _dbContext.SaveChangesAsync();

            return certificate;
        }

        public async Task<SupplierCertificate?> EditCertificateAsync(SupplierCertificate certificate, int userId)
        {
            SupplierCertificate? existingCertificacte = await _dbContext.SupplierCertificates.FirstOrDefaultAsync(c => c.Id == certificate.Id && c.AppUserId == userId);

            if (existingCertificacte == null)
            {
                return null;
            }

            existingCertificacte.Name = certificate.Name;
            existingCertificacte.IssuedBy = certificate.IssuedBy;
            existingCertificacte.IssueDate = certificate.IssueDate;
            existingCertificacte.ExpiryDate = certificate.ExpiryDate;

            await _dbContext.SaveChangesAsync();

            return existingCertificacte;
        }

        public async Task<bool> RemoveCertificateAsync(int userId, int certificateId)
        {
            int deletedRows = await _dbContext.SupplierCertificates.
                Where(certificate => certificate.Id == certificateId && certificate.AppUserId == userId).
                ExecuteDeleteAsync();

            return deletedRows > 0;
        }
    }
}
