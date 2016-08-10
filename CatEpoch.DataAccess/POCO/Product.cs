using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatEpoch.DataAccess.POCO
{
    public class Product
    {
        public Product()
        {
            ProductGroup = new ProductGroup();
        }
        public string Id { get; set; }
        public string ProductName { get; set; }
        public double BasePrice { get; set; }
        public bool Active { get; set; }
        public bool Discontnuied { get; set; }

        // ProductGroup Relationship One to Many.
        public int GroupId { get; set; }
        public virtual ProductGroup ProductGroup { get; set; }
        // Promo Relationship Many to One...     
        public virtual IList<Promo> Promos { get; set; }
        public virtual IList<PromoDetail> PromoDetails { get; set; }
        public virtual IList<SalesHistory> SalesHistories { get; set; }
    }
}
