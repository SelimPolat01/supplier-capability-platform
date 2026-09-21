using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SupplierCapabilitiesAndManagementSystem.Entities;

namespace SupplierCapabilitiesAndManagementSystem.Database
{
    public class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions) : base(dbContextOptions) { }

        public DbSet<SupplierMachine> SupplierMachines { get; set; }
        public DbSet<SupplierCertificate> SupplierCertificates { get; set; }
        public DbSet<SupplierHumanResource> SupplierHumanResources { get; set; }
        public DbSet<MachinePurchase> PurchaserMachinePurchases { get; set; }

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

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SupplierMachine>()
                .HasIndex(machine => new { machine.AppUserId, machine.MachineGroup, machine.MachineType, machine.BrandAndModel, machine.ProductionYear, machine.CapacitySpecs }).
                IsUnique();

            modelBuilder.Entity<SupplierCertificate>()
                .HasIndex(certificate => new { certificate.AppUserId, certificate.Name, certificate.IssuedBy, certificate.IssueDate, certificate.ExpiryDate });

            modelBuilder.Entity<MachinePurchase>()
                .HasOne(machinePurchase => machinePurchase.Purchaser)
                .WithMany(purchaser => purchaser.PurchaserMachinePurchases)
                .HasForeignKey(machinePurchase => machinePurchase.PurchaserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MachinePurchase>()
                .HasOne(machinePurchase => machinePurchase.SupplierMachine)
                .WithMany()
                .HasForeignKey(machinePurchase => machinePurchase.SupplierMachineId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}

