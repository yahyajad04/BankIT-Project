using BankingSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BankingSystem.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountType> AccountTypes { get; set; }
        public DbSet<CustomerProfile> CustomerProfiles { get; set; }
        public DbSet<EmployeeProfile> EmployeeProfiles { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<Transactions> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = "admin-role-id",
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Id = "customer-role-id",
                    Name = "Customer",
                    NormalizedName = "CUSTOMER"
                },
                new IdentityRole
                {
                    Id = "employee-role-id",
                    Name = "Employee",
                    NormalizedName = "EMPLOYEE"
                },

            };
            modelBuilder.Entity<IdentityRole>().HasData(roles);

            modelBuilder.Entity<AppUser>()
                .HasOne(x => x.CustomerProfile)
                .WithOne(w => w.AppUser)
                .HasForeignKey<CustomerProfile>(f => f.AppUserId)
                .IsRequired(false);

            modelBuilder.Entity<AppUser>()
            .HasOne(x => x.EmployeeProfile)
            .WithOne(w => w.AppUser)
            .HasForeignKey<EmployeeProfile>(f => f.AppUserId)
            .IsRequired(false);

            modelBuilder.Entity<Account>()
                .HasMany(t => t.SentTransactions)
                .WithOne(a => a.FromAccount)
                .HasForeignKey(f => f.FromAccountId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Account>()
                .HasMany(t => t.RecievedTransactions)
                .WithOne(a => a.ToAccount)
                .HasForeignKey(f => f.ToAccountId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Account>()
                .Property(p => p.Balance)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Loan>()
                .Property(p => p.MonthlyRepay)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Loan>()
                .Property(p => p.RemaintoPay)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Transactions>()
            .Property(p => p.Amount)
            .HasPrecision(18, 2);

            modelBuilder.Entity<Loan>()
            .Property(p => p.InterestRate)
            .HasPrecision(18, 2);

            modelBuilder.Entity<Loan>()
            .Property(p => p.Amount)
            .HasPrecision(18, 2);

            modelBuilder.HasSequence<long>("AccountNumbers")
                .StartsAt(10000000)     
                .IncrementsBy(1);

            modelBuilder.Entity<Account>(entity =>
            {
                entity.Property(a => a.AccountNumber)
                      .HasDefaultValueSql("NEXT VALUE FOR AccountNumbers")
                      .IsRequired(); 

                entity.HasIndex(a => a.AccountNumber)
                      .IsUnique();
            });
        }   
    }
}
