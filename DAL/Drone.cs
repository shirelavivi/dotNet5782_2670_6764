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
                Console.WriteLine(this.id + "Drone ID:");
                Console.WriteLine(this.Model + " Drone model:");
                Console.WriteLine(this.MaxWeight + "Weight:");
                Console.WriteLine(this.status+ "Occupancy status:");
                Console.WriteLine( this.Battery + "Battery status:");
            }
        }

    }
}
