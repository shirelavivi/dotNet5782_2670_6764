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
                Console.WriteLine(this.Id + "מזהה תחנה:");
                Console.WriteLine(this.Name + "שם התחנה:");
                Console.WriteLine("נקודת מיקום של התחנה:");
                Console.WriteLine("(" + this.Longitude + "," + this.Lattitude + ")");
                Console.WriteLine(this.ChargeSlots + "מספר עמדות הטענה פנויות:");

            }
        }
    }
}
