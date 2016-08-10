using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatEpoch.DataAccess.POCO
{
    public class PromoDetail
    {
        public int promoDetailId { get; set; }
        public int PromoDefId { get; set; }
        public PromoDef PromoDef { get; set; }
        public string ProductId { get; set; }
        public Product Product { get; set; }

    }
}
