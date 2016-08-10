using CatEpoch.DataAccess.CONFIG;
using CatEpoch.DataAccess.POCO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatEpoch.DataAccess
{
    public class DBC : DbContext
    {
        public DBC()
            : base("Name=CatPlan")
        {
            // Configuration.LazyLoadingEnabled = false;

        }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductGroup> ProductGroups { get; set; }
        public virtual DbSet<ProductTree> ProductTrees { get; set; }
        public virtual DbSet<Promo> Promos { get; set; }
        public virtual DbSet<PromoDef> PromoDefs { get; set; }
        public virtual DbSet<PromoDetail> PromoDetails { get; set; }
        public virtual DbSet<Period> Periods { get; set; }
        public virtual DbSet<DimDate> DimDates { get; set; }
        public virtual DbSet<SalesHistory> SalesHistories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ProductConfig());
            modelBuilder.Configurations.Add(new ProductGroupConfig());
            modelBuilder.Configurations.Add(new ProductTreeConfig());
            modelBuilder.Configurations.Add(new PromoConfig());
            modelBuilder.Configurations.Add(new PromoDefConfig());
            modelBuilder.Configurations.Add(new PromoDetailConfig());
            modelBuilder.Configurations.Add(new DimDateConfig());
            modelBuilder.Configurations.Add(new SalesHistoryConfig());
            modelBuilder.Entity<ProductGroup>().MapToStoredProcedures(s => s.Insert(i => i.HasName("AddGroup")));
            modelBuilder.Entity<DimDate>().MapToStoredProcedures(s => s.Insert(i => i.HasName("FillDimDate")));
            base.OnModelCreating(modelBuilder);

        }
    }
}
