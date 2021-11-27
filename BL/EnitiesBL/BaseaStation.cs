using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IBL
{
    namespace BL
    {
        class BaseaStation
        {
            int Idnumber;
            string NameStation;
            Location LocationOfStation;
            int ChargingAvailable;
            List<DroneInCharging> DroneInCharging;

        }
    }
}
