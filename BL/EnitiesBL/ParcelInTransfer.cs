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
            public int IdPacket { get; set; }
            public bool PackageMode { get; set; }
            public Priorities Priorities { get; set; }
            public Weightcategories Weightcategories { get; set; }
            public CustomerAtParcels CustomerInPackageSender { get; set; }
            public CustomerAtParcels CustomerInPackageGeting { get; set; }
            public Location CollectionLocation { get; set; }
            public Location DeliveryLocation { get; set; }
            public double TransportDistance { get; set; }
            public override string ToString()
            {
                return this.ToStringProperty();
            }

        }

    }
}