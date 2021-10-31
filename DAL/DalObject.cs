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
            static Random Rand = new Random(DateTime.Now.Millisecond);
            internal static List<Drone> drones = new List<Drone>();//רשימה של רחפנים
            internal static List<Station> Stations = new List<Station>();// רשימה של תחנות בסיס
            internal static List<Customer> customers = new List<Customer>();//רשימה של לקוחות
            internal static List<Parcel> packets = new List<Parcel>();// רשימה של חבילות
            internal class config
            {
                static int CounterPackets = 0;//  מספר מזהה סידורי לחבילות שיעודכן כל פעם שנוצרת חבילה חדשה 

                public static void Initialize()
                {

                    int t;

                    Station s = new Station();
                    Drone d = new Drone();
                    Customer c = new Customer();
                    Parcel a = new Parcel();
                    for (int i = 0; i < 3; i++)//מילוי 2 תחנות בסיס
                    {
                        s.Id = Rand.Next();
                        s.Name = "Station" + i;
                        s.Longitude = Rand.Next();
                        s.Lattitude = Rand.Next();
                        s.ChargeSlots = i + 3;
                        Stations.Add(s);
                    }

                    for (int i = 0; i < 7; i++)//מילוי שישה רחפנים
                    {
                        d.id = Rand.Next();
                        t = Rand.Next(3);
                        switch (t)
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
                        t = Rand.Next(3);
                        switch (t)
                        {
                            case 0:
                                d.status = DroneStatuses.available;
                                break;
                            case 1:
                                d.status = DroneStatuses.maintenance;
                                break;
                            case 2:
                                d.status = DroneStatuses.transport;
                                break;

                        }
                        d.Battery = Rand.Next();
                    }
                    for (int i = 0; i < 11; i++)//מילוי 10 לקוחות
                    {
                        c.Id = Rand.Next();
                        c.Name = "customer" + i;
                        c.Phone = "05" + Rand.Next(4) + Rand.Next(9) + Rand.Next(9) + Rand.Next(10) + Rand.Next(10) + Rand.Next(10) + Rand.Next(10) + Rand.Next(10);
                        c.Longitude = Rand.Next();
                        c.Lattitude = Rand.Next();
                    }

                    for (int i = 0; i < 11; i++)//מילוי 10 חבילות
                    {
                        a.Id = CounterPackets;
                        a.Senderld = Rand.Next();
                        a.Targetid = Rand.Next();
                        t = Rand.Next(3);
                        switch (t)
                        {
                            case 0:
                                a.Weight = Weightcategories.easy;
                                break;
                            case 1:
                                a.Weight = Weightcategories.middle;
                                break;
                            case 2:
                                a.Weight = Weightcategories.weighty;
                                break;

                        }

                        t = Rand.Next(3);
                        switch (t)
                        {
                            case 0:
                                a.Priority = Priorities.Normal;
                                break;
                            case 1:
                                a.Priority = Priorities.fast;
                                break;
                            case 2:
                                a.Priority = Priorities.emergency;
                                break;
                            default:
                                break;
                        }
                        a.Requested = DateTime.Now;
                        a.Scheduled = DateTime.Now;
                        a.PickedUp = DateTime.Now;
                        a.Delivered = DateTime.Now;
                        a.Droneld = 0;
                        CounterPackets++;
                    }

                }


            }

            public class DalObject
            {



                public void add(Station s)///הוספת תחנת בסיס
                {
                    Stations.Add(s);
                }
                public void add(Drone d)/// הוספת רחפן
                {
                    drones.Add(d);
                }
                public void add(Customer c)///הוספת לקוח חדש לרשימת הלקוחות
                {
                    customers.Add(c);
                }
                public void add(Parcel p)///הוספת חבילה חדשה 
                {
                    packets.Add(p);
                }
                public void ConnectParcelToDron(Parcel p)///שייוך חבילה לרחפן
                {
                    List<Drone> run = drones;
                    

                    for (int i = 0; i < run.Count(); i++)
                    {
                        if (run[i].status == DroneStatuses.available)///האם הרחפן פנוי
                        {
                            if (run[i].MaxWeight == p.Weight)
                            {
                                p.Droneld = run[i].id;
                                p.Scheduled = DateTime.Now;///שינוי זמן שיוך
                                return;
                            }
                        }
                    }


                }

                public void collection(Parcel p)///איסוף חבילה על י

                {
                    List<Drone> run = drones;
                    Drone temp = new Drone();

                    for (int i = 0; i < run.Count(); i++)
                    {
                        if(run[i].id==p.Droneld)
                        {
                            temp.id = run[i].id;
                            temp.MaxWeight = run[i].MaxWeight;
                            temp.Model = run[i].Model;
                            temp.status = DroneStatuses.transport;
                            temp.Battery = run[i].Battery;
                            run.Remove(run[i]);///מחיקת הרחפן הישן 
                            run.Add(temp);///הוספת הרחפן  כאשר השדה של מצב הרחפן מעודכן ל" משלוח" מ
                            p.PickedUp = DateTime.Now;///שינוי זמן איסוף
                            return;

                        }

                    }
                   
                }
                public void PackageDalvery(Parcel p)
                {
                    List<Drone> run = drones;
                    Drone temp = new Drone();
                    for (int i = 0; i < run.Count(); i++)
                    {
                        if (run[i].id == p.Droneld)
                        {
                            temp.id = run[i].id;
                            temp.MaxWeight = run[i].MaxWeight;
                            temp.Model = run[i].Model;
                            temp.status = DroneStatuses.available;
                            temp.Battery = run[i].Battery;
                            run.Remove(run[i]);///מחיקת הרחפן הישן 
                            run.Add(temp);
                            p.Delivered = DateTime.Now;///שינוי זמן קבלת החבילה עי הלקח

                        }
                    }


                }
                public void ShowThis(object o)
                {
                    o.ToString();
                }

            }

        }

     }
}


