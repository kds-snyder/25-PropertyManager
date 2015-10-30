using PropertyManager.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyManager.Core.Domain
{
    public class Property
    {
        public int PropertyId { get; set; }

        public string Name { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }

        public virtual ICollection<Lease> Leases { get; set; }
        public virtual ICollection<Image> Images { get; set; }

        public void Update (PropertyModel modelProperty)
        {

            // Copy values from input object to Property property
            Name = modelProperty.Name;
            Address1 = modelProperty.Address1;
            Address2 = modelProperty.Address2;
            City = modelProperty.City;
            State = modelProperty.State;
            Zipcode = modelProperty.Zipcode;

        }
    }
}
