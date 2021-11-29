using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IBL
{
    namespace BO
    {
        public class DroneToList
        {
            int Idnumber { get; set; }
            string Model { get; set; }
            Weightcategories Weightcategories { get; set; }
            double ButerryStatus { get; set; }
            DroneStatuses DroneStatuses { get; set; }
            Location ThisLocation { get; set; }
            int PackageNumberTransferred { get; set; }
            public override string ToString()
            {
                return this.ToStringProperty();
            }
        }
    }
}
