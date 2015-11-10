using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyManager.Core.Models
{
    public class DashboardModel
    {
        public int LeaseCount { get; set; }
        public int PropertyCount { get; set; }
        public int TenantCount { get; set; }
        public decimal TotalMonthlyIncome { get; set; }

        public IEnumerable<LeaseModel> ExpiringLeases { get; set; }
    }
}
