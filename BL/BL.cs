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
           public BL()
            {

            }
        }
    }
}