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
    public class ProductTreeConfig : EntityTypeConfiguration<ProductTree>
    {
        public ProductTreeConfig()
        {
            HasKey(k => k.OrgNode);
            Property(p => p.OrgLevel)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
            Property(p => p.ParentId).IsOptional();
        }

    }
}
