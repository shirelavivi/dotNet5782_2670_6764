using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    namespace DO
    {
        public struct Parcel 
        {
            /// <summary>
            /// Parcel serial number
            /// </summary>
            public int Id { get; set; }
            public int SenderId { get; set; }
            public int TargetId { get; set; } 
            public Weightcategories Weight { get; set; }
            public Priorities Priority { get; set; }
            public DateTime Requested  { get; set; }
            public int DroneId { get; set; }
            public DateTime Scheduled { get; set; }
            public DateTime PickedUp { get; set; }
            public DateTime Delivered { get; set; }
            public void Tostring()
            {
                Console.WriteLine("ParcelID:"+ this.Id);
                Console.WriteLine("Sender name:"+ this.SenderId );
                Console.WriteLine("Name of the receiver:"+this.TargetId);
                Console.WriteLine("Parcel weight:"+ this.Weight);
                Console.WriteLine("Order priority:"+ this.Priority);
                Console.WriteLine("Package creation time:"+ this.Requested);
                Console.WriteLine("Drone operation:"+ this.DroneId );
                Console.WriteLine("Assignment time:"+ this.Scheduled );
                Console.WriteLine("Collection time:"+ this.PickedUp);
                Console.WriteLine("Parcel delivery time:"+this.Delivered);

            }
        }

    }
}
