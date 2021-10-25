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
            
            int id { get; set; }
            string Model { get; set; }
            public enum weightCategories { easy, middle, weighty }
            weightCategories MaxWeight { get; set; }
            public enum DroneStatuses { available, maintenance,transport }
             DroneStatuses status { get; set; }
            double Battery { get; set; }
        }

    }
}
