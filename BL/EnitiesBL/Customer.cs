using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class Customer
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Phone { get; set; }
            public Location location { get; set; }
            public List<ParcelAtCustomer> parcelfromCustomer { get; set; }
            public List<ParcelAtCustomer> parcelsToCustomer { get; set; }
            public override string ToString()
            {
                return this.ToStringProperty();
            }
        }
    }
}

