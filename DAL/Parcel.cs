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
                Console.WriteLine(this.Id + "ParcelID:");
                Console.WriteLine(this.SenderId + "Sender name:");
                Console.WriteLine(this.TargetId+ "Name of the receiver:");
                Console.WriteLine(this.Weight+ "Parcel weight:");
                Console.WriteLine(this.Priority + "Order priority:");
                Console.WriteLine(this.Requested + "Package creation time:");
                Console.WriteLine(this.DroneId + "Skimmer operation:");
                Console.WriteLine(this.Scheduled + "Assignment time:");
                Console.WriteLine(this.PickedUp + "Collection time:");
                Console.WriteLine(this.Delivered + "Parcel delivery time:");

            }
        }

    }
}
