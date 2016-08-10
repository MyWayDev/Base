using CatEpoch.DataAccess.POCO;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatEpoch.DataAccess.CONFIG
{
    public class SalesHistoryConfig : EntityTypeConfiguration<SalesHistory>
    {
        public SalesHistoryConfig()
        {
            HasKey(p => new
            {
                p.ProductId,
                p.Month,
                p.Year

            });
            HasRequired(g => g.Product)
                .WithMany(p => p.SalesHistories)
                .HasForeignKey(g => g.ProductId);
            HasRequired(g => g.ProductGroup)
             .WithMany(p => p.SalesHistories)
             .HasForeignKey(g => g.GroupId);
          
            HasRequired(g => g.Promo)
             .WithMany(p => p.SalesHistories)
             .HasForeignKey(g => g.PromoId);
        }
    }
}
