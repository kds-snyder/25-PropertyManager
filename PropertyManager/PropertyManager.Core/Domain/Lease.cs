using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyManager.Core.Domain
{
    public class Lease
    {
        public int LeaseId { get; set; }

        public DateTime CreatedDate { get; set; }

        public int PropertyId { get; set; }
        public int TenantId { get; set; }

        public DateTime StartDate { get; set; }
        public Nullable<DateTime> EndDate { get; set; }

        public Decimal Rent { get; set; }

        public enum RentPeriod
        {
            Daily,
            Weekly,
            Monthly,
            Yearly
        };

        public  RentPeriod LeaseType { get; set; }
        
        public virtual Property Property { get; set; }

        public virtual Tenant Tenant { get; set; }
    }
}
