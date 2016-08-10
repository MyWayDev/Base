using CatEpoch.DataAccess.POCO;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatEpoch.DataAccess.CONFIG
{
    public class PromoConfig : EntityTypeConfiguration<Promo>
    {
        //Unable to determine the principal end of an association between the types 
        // 'CatEpoch.DataAccess.POCO.PromoDef' and 'CatEpoch.DataAccess.POCO.Promo'. 
        //The principal end of this association
        //must be explicitly configured using either the relationship fluent API or data annotations.
        public PromoConfig()
        {
            Map(p => p.ToTable("Promos"));
            HasKey(k => k.PromoId);
            HasRequired(p => p.PromoDef)
                .WithRequiredPrincipal(p => p.Promo)
                .WillCascadeOnDelete(true);
            HasMany(p => p.Periods).WithMany(p => p.Promos);


        }
    }
}
