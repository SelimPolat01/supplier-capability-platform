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
        public DbSet<PurchaserMachinePurchase> PurchaserMachinePurchases { get; set; }

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

            modelBuilder.Entity<SupplierMachine>()
                .Property(supplierMachine => supplierMachine.QualityScore)
                .HasConversion<string>();

            modelBuilder.Entity<SupplierCertificate>()
                .Property(supplierCertificate => supplierCertificate.QualityScore)
                .HasConversion<string>();

            modelBuilder.Entity<SupplierHumanResource>()
                .Property(supplierHumanResource => supplierHumanResource.QualityScore)
                .HasConversion<string>();

            modelBuilder.Entity<AppUser>()
                .Property(supplier => supplier.QualityScore)
                .HasConversion<string>();

            modelBuilder.Entity<PurchaserMachinePurchase>()
                .Property(purchaserMachinePurchase => purchaserMachinePurchase.QualityScore)
                .HasConversion<string>();

            modelBuilder.Entity<SupplierMachine>()
                .HasOne(supplierMachine => supplierMachine.AppUser)
                .WithMany(user => user.SupplierMachines)
                .HasForeignKey(supplierMachine => supplierMachine.AppUserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SupplierCertificate>()
                .HasOne(supplierCertificate => supplierCertificate.AppUser)
                .WithMany(user => user.SupplierCertificates)
                .HasForeignKey(supplierCertificate => supplierCertificate.AppUserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SupplierHumanResource>()
                .HasOne(supplierHumanResource => supplierHumanResource.User)
                .WithMany(user => user.SupplierHumanResources)
                .HasForeignKey(supplierHumanResource => supplierHumanResource.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SupplierMachine>()
                .HasOne(supplierMachine => supplierMachine.Qualitier)
                .WithMany(qualitier => qualitier.QualitierSupplierMachineScores)
                .HasForeignKey(supplierMachine => supplierMachine.QualitierId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SupplierCertificate>()
                .HasOne(supplierCertificate => supplierCertificate.Qualitier)
                .WithMany(qualitier => qualitier.QualitierSupplierCertificateScores)
                .HasForeignKey(supplierCertificate => supplierCertificate.QualitierId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SupplierHumanResource>()
                .HasOne(supplierHumanResource => supplierHumanResource.Qualitier)
                .WithMany(qualitier => qualitier.QualitierSupplierHumanResourceScores)
                .HasForeignKey(supplierHumanResource => supplierHumanResource.QualitierId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AppUser>()
                .HasOne(supplier => supplier.Qualitier)
                .WithMany()
                .HasForeignKey(supplier => supplier.QualitierId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PurchaserMachinePurchase>()
                .HasOne(purchaserMachinePurchase => purchaserMachinePurchase.Qualitier)
                .WithMany(qualitier => qualitier.QualitierPurchaserPurchaseScores)
                .HasForeignKey(purchaserMachinePurchase => purchaserMachinePurchase.QualitierId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PurchaserMachinePurchase>()
                .HasOne(purchaserMachinePurchase => purchaserMachinePurchase.Purchaser)
                .WithMany(purchaser => purchaser.PurchaserMachinePurchases)
                .HasForeignKey(purchaserMachinePurchase => purchaserMachinePurchase.PurchaserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PurchaserMachinePurchase>()
                .HasOne(purchaserMachinePurchase => purchaserMachinePurchase.SupplierMachine)
                .WithMany()
                .HasForeignKey(purchaserMachinePurchase => purchaserMachinePurchase.SupplierMachineId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}

