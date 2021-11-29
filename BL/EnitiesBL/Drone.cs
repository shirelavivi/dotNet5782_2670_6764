using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IBL
{
    namespace BO
    {
        public class Drone
        {
            int IdDrone { get; set; }
            string Modle { get; set; }
            Weightcategories Weightcategories { get; set; }
            double ButerryStatus { get; set; }
            DroneStatuses DroneStatuses { get; set; }
            ParcelInTransfer PackageInTransfer { get; set; }
            Location ThisLocation { get; set; }
            public override string ToString()
            {
                return this.ToStringProperty();
            }
        }
    }
}
