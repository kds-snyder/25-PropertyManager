using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyManager.Core.Constant;
using PropertyManager.Core.Models;

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
        public  Constants.RentPeriod LeaseType { get; set; }
        
        public virtual Property Property { get; set; }

        public virtual Tenant Tenant { get; set; }

        public void Update(LeaseModel modelLease)
        {
            // If adding new lease, set created date
            if (modelLease.LeaseId == 0)
            {
                CreatedDate = DateTime.Now;
            }

            // Copy values from input object to Lease lease           
            PropertyId = modelLease.PropertyId;
            TenantId = modelLease.TenantId;
            StartDate = modelLease.StartDate;
            EndDate = modelLease.EndDate;
            Rent = modelLease.Rent;
            LeaseType = modelLease.LeaseType;           
        }
    }
}
