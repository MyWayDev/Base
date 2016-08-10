using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatEpoch.DataAccess.POCO
{
    public class Promo
    {
        public int PromoId { get; set; }
        public string promoName { get; set; }

        // PromoDefinition One to One.. 
        public virtual PromoDef PromoDef { get; set; }
        // Product Relationship One to Many...
        public string ProductId { get; set; }
        public virtual Product Product { get; set; }
        public PromoGrade PromoGrade { get; set; }
        public virtual IList<Period> Periods { get; set; }
        public virtual IList<SalesHistory> SalesHistories { get; set; }
    }
    public enum PromoGrade
    {
        A = 1,
        B = 2,
        C = 3
    }
}
