using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TedarikciKabiliyetYonetimSistemi.Entities;

namespace TedarikciKabiliyetYonetimSistemi.Database
{
    public class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions) : base(dbContextOptions) { }

        public DbSet<Machine> Machines { get; set; }
        public DbSet<Certificate> Certificates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Machine>()
            //    .HasOne(machine => machine.AppUser)
            //    .WithMany(user => user.Machines)
            //    .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<Certificate>()
            //    .HasOne(certificate => certificate.AppUser)
            //    .WithMany(user => user.Certificates)
            //    .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Machine>()
                .HasIndex(machine => new { machine.AppUserId, machine.MachineGroup, machine.MachineType, machine.BrandAndModel, machine.ProductionYear, machine.CapacitySpecs }).
                IsUnique();

            modelBuilder.Entity<Certificate>()
                .HasIndex(certificate => new { certificate.AppUserId, certificate.Name, certificate.IssuedBy, certificate.IssueDate, certificate.ExpiryDate });

            base.OnModelCreating(modelBuilder);
        }
    }
}

