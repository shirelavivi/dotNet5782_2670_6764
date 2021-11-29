using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class ParcelInTransfer
        {
            int IdPacket { get; set; }
            bool PackageMode { get; set; }
            Priorities Priorities { get; set; }
            Weightcategories Weightcategories { get; set; }
            CustomerAtParcels CustomerInPackageSender { get; set; }
            CustomerAtParcels CustomerInPackageGeting { get; set; }
            Location CollectionLocation { get; set; }
            Location DeliveryLocation { get; set; }
            double TransportDistance { get; set; }
            public override string ToString()
            {
                return this.ToStringProperty();
            }

        }

    }
}