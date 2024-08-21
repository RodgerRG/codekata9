using Code.Kata._9.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Code.Kata._9.Data;

public class ApiDbContext : DbContext
{
    #region DB Set Declarations
        public DbSet<Checkout> Checkouts { get; set; }
        public DbSet<CheckoutItem> CheckoutItems { get; set; }
        public DbSet<PricingCatalogue> PricingCatalogues { get; set; }
        public DbSet<PricingInfo> PricingInfos { get; set; }
        public DbSet<PricingRule> PricingRules { get; set; }
        public DbSet<SalesItem> SalesItems { get; set; }
    #endregion
    
    #region Model Configuration
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
    #endregion
}