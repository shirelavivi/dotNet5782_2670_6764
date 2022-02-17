using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DO;
//***//


namespace Dal
{
   internal static class DataSource
    {
        static Random Rand = new Random(DateTime.Now.Millisecond);
        internal static List<Drone> drones = new List<Drone>();
        internal static List<Station> Stations = new List<Station>();
        internal static List<Customer> customers = new List<Customer>();
        internal static List<Parcel> packets = new List<Parcel>();
        internal static List<DroneCharge> DronesCharge = new List<DroneCharge>();
        internal static List<User> Users = new List<User>();
        /****/
        static string droneChargingPath = @"droneChargingPath.xml";
        ///*למחוק אחכ זמני*/
        static string userPath = @"userPathXml.xml";
        static string customerPath = @"customerPathXml.xml";
        static string stationPath = @"stationPathXml.xml";
        static string dronePath = @"dronePathXml.xml";
        static string parcelPath = @"parcelPathXml.xml";

        static DataSource()
        { 
            Initialize();
            //**//
            XMLTools.SaveListToXMLSerializer(customers, customerPath);
            XMLTools.SaveListToXMLSerializer(drones, dronePath);
            XMLTools.SaveListToXMLSerializer(packets, parcelPath);
            XMLTools.SaveListToXMLSerializer(DronesCharge, droneChargingPath);
            XMLTools.SaveListToXMLSerializer(Stations, stationPath);
            XMLTools.SaveListToXMLSerializer(Users, userPath);
        }

        internal class config
        {
            /// <summary>
            /// A serial number for packages that will be updated each time a new package is created
            /// </summary>
            public static int CounterPackets = 10;
            public static double Free = 0.01;
            public static double Light = 0.04;
            public static double Medium = 0.08;
            public static double Heavy = 0.1;
            public static int ChargingRate = 25;

            public static double[] returnArrBattery()
            {
                double[] arr = new double[4];
                arr[0] = Free;
                arr[1] = Light;
                arr[2] = Medium;
                arr[3] = Heavy;
                return arr;
            }
            public static int runCounterPackets()
            {
                CounterPackets++;
                return CounterPackets;
            }
        }
      public static void Initialize()
        {
            int t;
            Random rnd = new Random();
            for (int i = 1; i <= 3; i++)
            {
                Station s = new Station();
                s.Id = i;
                s.Name = "Station" + i;
                s.Longitude = Rand.Next(0, 100) / 10;
                s.Lattitude = Rand.Next(0, 100) / 10;
                s.ChargeSlots = i + 20;
                Stations.Add(s);
            }
            for (int i = 1; i <= 7; i++)
            {
                Drone d = new Drone();
                d.id = i;
                d.Model = "drone" + i;
                t = Rand.Next(0, 3);
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
                drones.Add(d);

            }
            for (int i = 1; i <= 11; i++)
            {
                Customer c = new Customer();
                c.Id = i;
                c.Name = "customer" + i;
                c.Phone = "05" + Rand.Next(4) + Rand.Next(9) + Rand.Next(9) + Rand.Next(10) + Rand.Next(10) + Rand.Next(10) + Rand.Next(10) + Rand.Next(10);
                c.Longitude = Rand.Next(0, 100) / 10;
                c.Lattitude = Rand.Next(0, 100) / 10;
                customers.Add(c);

            }
            for (int i = 1; i < 11; i++)
            {
                Parcel a = new Parcel();
                a.Id =i;
                a.SenderId = Rand.Next(1, 12);
                a.TargetId = Rand.Next(1, 12);
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
                        a.Priority = Priorities.Standard;
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
                a.Requested = null;
                a.Scheduled = null;
                a.PickedUp = null;
                a.Delivered = null;
                a.DroneId = 0;

                packets.Add(a);
            }
            config.CounterPackets = 12;
            for (int i = 1; i <= 7; i++)
            {
                DroneCharge d = new DroneCharge();
                d.Droneld =i;
                d.StationId = Rand.Next(1, 4);
                DronesCharge.Add(d);
            }
            for (int i = 1; i <= 11; i++)
            {
                User c = new User();
                c.PassWord =Rand.Next(10000000,999999999);
                c.Name = "user" + i;
                Users.Add(c);

            }
        }
    }
}



