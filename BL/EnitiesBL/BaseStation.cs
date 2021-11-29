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
           public int Idnumber { get; set; }
            public string NameStation { get; set; }
            public Location locationOfStation { get; set; }
            public int ChargingAvailable { get; set; }
            public List<DroneInCharging> droneInCharging { get; set; }
            public override string ToString()
            {
                return this.ToStringProperty();
            }

        }
    }
}
