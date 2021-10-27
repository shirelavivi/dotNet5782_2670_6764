using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IDAL.DO;

namespace IDAL
{
    namespace DalObject
    {
       
        class DataSource
        {
            static Random rand = new Random(DateTime.Now.Millisecond);
            internal static List<Drone> drones = new List<Drone>();//רשימה של רחפנים
            internal static List<Station> Stations = new List<Station>();// רשימה של תחנות בסיס
            internal static List<Customer> customers = new List<Customer>();//רשימה של לקוחות
            internal static List<Parcel> packets = new List<Parcel>();// רשימה של חבילות
            internal class config
            { 
                static int CounterPackets = 0;//  מספר מזהה סידורי לחבילות שיעודכן כל פעם שנוצרת חבילה חדשה 
            }
           public static void Initialize ()
            {
                Station s=new Station();
                for (int i=0;i<3;i++)
                {
                   s.Id = rand.Next(); 
                   s.Name = "Station"+i ;
                   s.Longitude= rand.Next();
                   s.Lattitude = rand.Next();
                }
                Weightcategories s;
                s= (weightcategories)int.Parse(sd);
             switch(s)
                {
                    case weightcategories.easy:
                        break;
                    case weightcategories.middle:
                        break;
                    case weightcategories.middle:
                        break;
                    default:
                        break;


                }
                Console.ReadLine();



            }

        }
    }
}
