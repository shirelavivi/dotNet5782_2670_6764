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
                Console.WriteLine(this.id + "מזהה רחפן:");
                Console.WriteLine(this.Model + "מוד הרחפן:");
                Console.WriteLine(this.MaxWeight + "משקל:");
                Console.WriteLine(this.status+"מצב תפוסה:");
                Console.WriteLine( this.Battery +"מצב סוללה:");
            }
        }

    }
}
