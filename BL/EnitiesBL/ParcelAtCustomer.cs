using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class ParcelAtCustomer
        {
            int Idnumber { get; set; }
            Weightcategories Weightcategories { get; set; }
            Priorities Priorities { get; set; }
            PacketStatuses PacketStatuses { get; set; }
            CustomerAtParcels CustomerInPackage { get; set; }
            public override string ToString()
            {
                return this.ToStringProperty();
            }
        }
    }
}
