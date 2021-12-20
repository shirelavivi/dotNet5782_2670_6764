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
           public int IdDrone { get; set; }
            public string Model { get; set; }
            public Weightcategories Weightcategories { get; set; }
            public double ButerryStatus { get; set; }
            public DroneStatuses DroneStatuses { get; set; }
            public List <ParcelInTransfer> PackageInTransfer { get; set; }
            public Location ThisLocation { get; set; }
            public override string ToString()
            {
                return this.ToStringProperty();
            }
        }
    }
}
