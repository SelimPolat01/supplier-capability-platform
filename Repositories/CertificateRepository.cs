using Microsoft.EntityFrameworkCore;
using TedarikciKabiliyetYonetimSistemi.Database;
using TedarikciKabiliyetYonetimSistemi.Entities;

namespace TedarikciKabiliyetYonetimSistemi.Repositories
{
    public class CertificateRepository : ICertificateRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CertificateRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Certificate> AddCertificateAsync(Certificate certificate)
        {
            await _dbContext.Certificates.AddAsync(certificate);
            await _dbContext.SaveChangesAsync();
            return certificate;
        }

        public async Task<(List<Certificate> Data, int TotalCount)> FetchAllCertificatesAsync(int userId, int pageNumber, int pageSize, string sortBy, string? searchString = null)
        {
            var query = _dbContext.Certificates
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
                "name_asc" => query.OrderBy(certificate => certificate.Name),
                "name_desc" => query.OrderByDescending(certificate => certificate.Name),
                "issued-by_asc" => query.OrderBy(certificate => certificate.IssuedBy),
                "issued-by_desc" => query.OrderByDescending(certificate => certificate.IssuedBy),
                "issue-date_asc" => query.OrderBy(certificate => certificate.IssueDate),
                "issue-date_desc" => query.OrderByDescending(certificate => certificate.IssueDate),
                "expiry-date_asc" => query.OrderBy(certificate => certificate.ExpiryDate),
                "expiry-date_desc" => query.OrderByDescending(certificate => certificate.ExpiryDate),
                "added_asc" => query.OrderBy(certificate => certificate.CreatedAt),
                "added_desc" => query.OrderByDescending(certificate => certificate.CreatedAt),
                _ => query.OrderBy(certificate => certificate.Id)
            };

            var result = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (result, totalCertificateCount);
        }

        public async Task<Certificate?> FetchCertificateAsync(int userId, int certificateId)
        {
            Certificate? existingCertificate = await _dbContext.Certificates.AsNoTracking().FirstOrDefaultAsync(c => c.Id == certificateId && c.AppUserId == userId);

            if (existingCertificate == null)
            {
                return null;
            }

            return existingCertificate;
        }

        public async Task<Certificate?> EditCertificateAsync(Certificate certificate, int userId)
        {
            Certificate? existingCertificacte = await _dbContext.Certificates.FirstOrDefaultAsync(c => c.Id == certificate.Id && c.AppUserId == userId);

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
            int deletedRows = await _dbContext.Certificates.
                Where(certificate => certificate.Id == certificateId && certificate.AppUserId == userId).
                ExecuteDeleteAsync();

            return deletedRows > 0;
        }
    }
}
