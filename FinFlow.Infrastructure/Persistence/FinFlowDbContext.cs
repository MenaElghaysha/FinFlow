using FinFlow.Domain.Entities.Customers;
using FinFlow.Domain.Entities.Loans;
using Microsoft.EntityFrameworkCore;
using FinFlow.Application.Common.Interfaces;

namespace FinFlow.Infrastructure.Persistence;

public class FinFlowDbContext : DbContext, IFinFlowDbContext
{
    public FinFlowDbContext(DbContextOptions<FinFlowDbContext> options)
        : base(options)
    {
    }

    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<LoanApplication> LoanApplications => Set<LoanApplication>();
    public DbSet<Installment> Installments => Set<Installment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Customer
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.FullName).IsRequired().HasMaxLength(200);
            entity.Property(c => c.Email).IsRequired().HasMaxLength(200);
            entity.Property(c => c.NationalId).IsRequired().HasMaxLength(14);
            entity.Property(c => c.MonthlyIncome).HasPrecision(18, 2);
        });

        // LoanApplication
        modelBuilder.Entity<LoanApplication>(entity =>
        {
            entity.HasKey(l => l.Id);
            entity.Property(l => l.Amount).HasPrecision(18, 2);
            entity.Property(l => l.Purpose).IsRequired().HasMaxLength(500);
            entity.Property(l => l.Status).HasConversion<string>();  // enum stored as a string in DB

            entity.HasOne(l => l.Customer)
                  .WithMany(c => c.LoanApplications)
                  .HasForeignKey(l => l.CustomerId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // Installment
        modelBuilder.Entity<Installment>(entity =>
        {
            entity.HasKey(i => i.Id);
            entity.Property(i => i.Amount).HasPrecision(18, 2);

            entity.HasOne(i => i.LoanApplication)
                  .WithMany(l => l.Installments)
                  .HasForeignKey(i => i.LoanApplicationId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
    }
}