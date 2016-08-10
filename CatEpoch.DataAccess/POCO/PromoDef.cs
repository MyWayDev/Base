using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatEpoch.DataAccess.POCO
{
    public class PromoDef
    {
        public int PromoDefId { get; set; }

        public double PromoPrice { get; set; }
        public int PromoId { get; set; }
        public Promo Promo { get; set; }
        public virtual IList<PromoDetail> PromoDetails { get; set; }




    }

    public enum PromoType
    {
        Discount = 0,
        Free = 1,



    }
     
}
