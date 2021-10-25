using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    namespace Do
    {
        class Drone
        {

            public int id { get; set; }
            public string Model { get; set; }
            
            public weightCategories MaxWeight { get; set; }
            
            public DroneStatuses status { get; set; }
            public double Battery { get; set; }
        }

    }
}
