using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatEpoch.DataAccess.POCO
{
    public class Period
    {
        public int PeriodId { get; set; }
        public string PeriodName { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Active { get; set; }

        public virtual IList<Promo> Promos { get; set; }
    }
}
