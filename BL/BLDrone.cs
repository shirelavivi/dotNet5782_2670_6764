using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public partial class BL : IBL
        {

            public void UpdateDrone(int id, string model)
            {
                IDAL.DO.Drone drone= dl.GetDrone(id);
                drone.Model = model;
                dl.DelDrone(id);
                dl.AddDrone(drone); 
            }

        }
    }
}
