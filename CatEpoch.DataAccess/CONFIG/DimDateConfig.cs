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
    public class DimDateConfig : EntityTypeConfiguration<DimDate>
    {
        public DimDateConfig()
        {
            HasKey(p => p.DateKey);
            Property(p => p.DateKey).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None).IsRequired();
        }
    }
}
