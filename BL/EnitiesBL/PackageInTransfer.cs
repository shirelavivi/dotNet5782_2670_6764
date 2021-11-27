using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BL.EnitiesBL
    {
        class PackageInTransfer
        {
            int IdPacket;
            bool PackageMode;
            Priorities Priorities;
            Weightcategories Weightcategories;
            CustomerInPackage CustomerInPackageSender;
            CustomerInPackage CustomerInPackageGeting;
            Location CollectionLocation;
            Location DeliveryLocation;
            double TransportDistance;

        }

    }
}