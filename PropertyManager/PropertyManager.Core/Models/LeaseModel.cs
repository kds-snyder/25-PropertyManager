using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyManager.Core.Domain;
using PropertyManager.Core.Constant;

namespace PropertyManager.Core.Models
{
    public class LeaseModel
    {
        public int LeaseId { get; set; }

        public DateTime CreatedDate { get; set; }

        public int PropertyId { get; set; }
        public int TenantId { get; set; }

        public DateTime StartDate { get; set; }
        public Nullable<DateTime> EndDate { get; set; }

        public Decimal Rent { get; set; }      

        public Constants.RentPeriod LeaseType { get; set; }

        public PropertyModel Property { get; set; }
        public TenantModel Tenant { get; set; }

        public string RentalPeriod { get; set; }

    }
}
