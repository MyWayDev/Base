using CatEpoch.DataAccess.POCO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatEpoch.DataAccess.CONFIG
{
    public class ProductConfig : EntityTypeConfiguration<Product>
    {
        public ProductConfig()
        {
            HasKey(p => p.Id);
            HasRequired(g => g.ProductGroup)
                .WithMany(p => p.Products)
                .HasForeignKey(g => g.GroupId);
            Property(p => p.Id).HasMaxLength(10).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None).IsRequired();
            HasMany(p => p.Promos).WithOptional(p => p.Product).WillCascadeOnDelete(true);
            HasMany(p => p.PromoDetails).WithOptional(p => p.Product).WillCascadeOnDelete(true);




        }
    }
}
