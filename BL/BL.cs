using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL;

namespace IBL
{
    namespace BO
    {
       
       
        public partial class BL :IBL
        {
            internal static List<DroneToList> drones_bl = new List<DroneToList>();
            DalObject.DalObject dl = new DalObject.DalObject();
            internal static double Free ;
            internal static double Light ;
            internal static double Medium;
            internal static double Heavy ;
            public BL()
            {
                
                Free = dl.batteryArr()[0];
                Light=dl.batteryArr()[1];
                Medium = dl.batteryArr()[2];
                Heavy = dl.batteryArr()[3];
                foreach(IDAL.DO.Drone item in  dl.GetALLDrones())
                {

                }
            }
        }
    }
}