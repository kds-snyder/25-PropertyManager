using PropertyManager.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyManager.Core.Domain
{
    public class Tenant
    {
        public int TenantId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Telephone { get; set; }
        public string EmailAddress { get; set; }

        public virtual ICollection<Lease> Leases { get; set; }

        public string FullName
        {
            get
            {
                return string.Format("{0} {1}", FirstName, LastName);
            }
        }

        public void Update(TenantModel modelTenant)
        {
            // Copy values from input object to Tenant tenant
            FirstName = modelTenant.FirstName;
            LastName = modelTenant.LastName;
            Telephone = modelTenant.Telephone;
            EmailAddress = modelTenant.EmailAddress;
        }
    }
}
