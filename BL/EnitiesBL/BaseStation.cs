using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IBL
{
    namespace BO
    {
       public class BaseStation
        {
            int idnumber { get; set; }
            string nameStation { get; set; }
            Location locationOfStation { get; set; }
            int chargingAvailable { get; set; }
            List<DroneInCharging> droneInCharging { get; set; }
            public override string ToString()
            {
                return this.ToStringProperty();
            }

        }
    }
}
