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
                Console.WriteLine("Station ID:"+ this.Id );
                Console.WriteLine("The station name:"+ this.Name );
                Console.WriteLine("Location point of the station:");
                Console.WriteLine("(" + this.Longitude + "," + this.Lattitude + ")");
                Console.WriteLine("Several claim positions are vacant:"+ this.ChargeSlots);

            }
        }
    }
}
