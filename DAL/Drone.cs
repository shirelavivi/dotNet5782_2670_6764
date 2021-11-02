using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;

namespace IDAL
{
    namespace DO
    {
        public struct Drone
        {

            public int id { get; set; }
            public string Model { get; set; } 
            public Weightcategories MaxWeight { get; set; } 
            public DroneStatuses status { get; set; }
            public double Battery { get; set; }
            public void Tostring()
            {
                Console.WriteLine("Drone ID:"+ this.id );
                Console.WriteLine(" Drone model:"+ this.Model);
                Console.WriteLine("Weight:"+ this.MaxWeight);
                Console.WriteLine("Occupancy status:"+ this.status);
                Console.WriteLine("Battery status:"+ this.Battery );
            }
        }

    }
}
