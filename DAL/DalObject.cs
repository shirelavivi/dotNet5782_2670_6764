using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IDAL.DO;

namespace IDAL
{
    namespace DalObject
    {
        class DataSource
        {
            internal List<Drone> drones = new List<Drone>();//רשימה של רחפנים
            internal List<DroneCharge> droneCharges = new List<DroneCharge>();// רשימה של תחנות בסיס
            internal List<Customer> customers = new List<Customer>();//רשימה של לקוחות
            internal List<Parcel> packets = new List<Parcel>();// רשימה של חבילות
            internal class config
            { 
                static int IdPackets = 0;//  מספר מזהה סידורי לחבילות שיעודכן כל פעם שנוצרת חבילה חדשה 
            }
           public static void Initialize ()
            {

            }

        }
    }
}
