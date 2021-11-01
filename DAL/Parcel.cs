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
            public int Senderld { get; set; }
            public int Targetid { get; set; } 
            public Weightcategories Weight { get; set; }
            public Priorities Priority { get; set; }
            public DateTime Requested  { get; set; }
            public int Droneld { get; set; }
            public DateTime Scheduled { get; set; }
            public DateTime PickedUp { get; set; }
            public DateTime Delivered { get; set; }
            public void Tostring()
            {
                Console.WriteLine(this.Id + "מזהה חבילה:");
                Console.WriteLine(this.Senderld + "שם השולח:");
                Console.WriteLine(this.Targetid+"שם המקבל:");
                Console.WriteLine(this.Weight+"משקל חבילה");
                Console.WriteLine(this.Priority +"עדיפות הזמנה");
                Console.WriteLine(this.Requested + "זמן יצירת חבילה");
                Console.WriteLine(this.Droneld + "רחפן מבצע");
                Console.WriteLine(this.Scheduled + "זמן שיוך");
                Console.WriteLine(this.PickedUp + "זמן איסוף");
                Console.WriteLine(this.Delivered +"זמן אספקת חבילה");

            }
        }

    }
}
