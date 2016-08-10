using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatEpoch.DataAccess.POCO
{
    public class ProductGroup
    {

        public int Id { get; set; }
        public string GroupName { get; set; }
        public virtual IList<Product> Products { get; set; }
        public virtual IList<ProductTree> ProductTrees { get; set; }
        public virtual IList<SalesHistory> SalesHistories { get; set; }

    }
}
