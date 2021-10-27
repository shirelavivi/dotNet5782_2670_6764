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
                Drone d = new Drone();
                for (int i=0;i<3;i++)
                {
                   s.Id = rand.Next(); 
                   s.Name = "Station"+i ;
                   s.Longitude= rand.Next();
                   s.Lattitude = rand.Next();
                   s.ChargeSlots = i + 3;
                    Stations.Add(s);
                }
                for (int i = 0; i < 7; i++)
                {
                    int x = rand.Next(3);
                    d.id = rand.Next();
                    d.Model = "drone" + i;
                    switch (x)
                    {
                        case 0:
                            d.MaxWeight = Weightcategories.easy;
                            break;
                        case 1:
                            d.MaxWeight = Weightcategories.middle;
                            break;
                        case 2:
                            d.MaxWeight = Weightcategories.weighty;
                            break;       
                    }
                    
                    
                }


            }

        }
    }
}
