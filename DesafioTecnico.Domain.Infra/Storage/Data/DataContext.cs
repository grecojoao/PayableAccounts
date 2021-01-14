using DesafioTecnico.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DesafioTecnico.Domain.Infra.Storage.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) =>
            Database.EnsureCreated();

        public DbSet<PayableAccount> PayableAccounts { get; set; }
        public DbSet<PaidAccount> PaidAccounts { get; set; }
        public DbSet<PaymentRule> PaymentRules { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) =>
            modelBuilder.Entity<PaymentRule>().HasData(
                new PaymentRule(delayedDays: 0, finePercentage: 2, finePercentageInterestPerDay: 0.1m),
                new PaymentRule(delayedDays: 4, finePercentage: 3, finePercentageInterestPerDay: 0.2m),
                new PaymentRule(delayedDays: 6, finePercentage: 5, finePercentageInterestPerDay: 0.3m));
    }
}