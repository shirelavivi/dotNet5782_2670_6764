using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    namespace DO
    {
       public struct Station
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public double Longitude { get; set; }
            public double Lattitude { get; set; }
            public int ChargeSlots { get; set; }
          public void Tostring()
            {
                Console.WriteLine(this.Id + "Station ID:");
                Console.WriteLine(this.Name + "The station name:");
                Console.WriteLine("Location point of the station:");
                Console.WriteLine("(" + this.Longitude + "," + this.Lattitude + ")");
                Console.WriteLine(this.ChargeSlots + "Several claim positions are vacant:");

            }
        }
    }
}
