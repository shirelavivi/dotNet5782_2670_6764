using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IDAL.DO;

namespace IDAL
{
    public class DataSource
    {
        static Random Rand = new Random(DateTime.Now.Millisecond);
        internal static List<Drone> drones = new List<Drone>();
        internal static List<Station> Stations = new List<Station>();
        internal static List<Customer> customers = new List<Customer>();
        internal static List<Parcel> packets = new List<Parcel>();
        internal static List<DroneCharge> DronesCharge = new List<DroneCharge>();

        internal class config
        {
            /// <summary>
            /// A serial number for packages that will be updated each time a new package is created
            /// </summary>
            static int CounterPackets = 0;
            public static int runCounterPackets()
            {
                CounterPackets++;
                return CounterPackets;
            }
            public static void Initialize()
            {

                int t;

                Station s = new Station();
                Drone d = new Drone();
                Customer c = new Customer();
                Parcel a = new Parcel();
                for (int i = 0; i < 3; i++)
                {
                    s.Id = Rand.Next();
                    s.Name = "Station" + i;
                    s.Longitude = Rand.Next();
                    s.Lattitude = Rand.Next();
                    s.ChargeSlots = i + 3;
                    Stations.Add(s);
                }

                for (int i = 0; i < 7; i++)
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
                    drones.Add(d);
                }
                for (int i = 0; i < 11; i++)
                {
                    c.Id = Rand.Next();
                    c.Name = "customer" + i;
                    c.Phone = "05" + Rand.Next(4) + Rand.Next(9) + Rand.Next(9) + Rand.Next(10) + Rand.Next(10) + Rand.Next(10) + Rand.Next(10) + Rand.Next(10);
                    c.Longitude = Rand.Next();
                    c.Lattitude = Rand.Next();
                    customers.Add(c);
                }

                for (int i = 0; i < 11; i++)
                {
                    a.Id = CounterPackets;
                    a.SenderId = Rand.Next();
                    a.TargetId = Rand.Next();
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
                    a.Requested = DateTime.Now;
                    a.Scheduled = DateTime.Now;
                    a.PickedUp = DateTime.Now;
                    a.Delivered = DateTime.Now;
                    a.DroneId = 0;
                    CounterPackets++;
                    packets.Add(a);
                }

            }







        }

    }
    namespace DalObject
    {
        public class DalObject
        {

            public DalObject()
            {
                DataSource.config.Initialize();
            }
            public static int runNumber()
            {

                return DataSource.config.runCounterPackets();
            }
            /// <summary>
            /// Add station to stations list
            /// </summary>
            /// <param name="s"></param>
            public static void add(Station s)
            {
                IDAL.DataSource.Stations.Add(s);
            }
            public static void add(Drone d)
            {
                IDAL.DataSource.drones.Add(d);
            }
            public static void add(Customer c)
            {
                IDAL.DataSource.customers.Add(c);
            }
            public static void add(Parcel p)
            {
                IDAL.DataSource.packets.Add(p);

            }
            public static void ConnectParcelToDron(int ParcelId, int DronId)
            {
                List<Drone> runOfDrone = IDAL.DataSource.drones;
                List<Parcel> runOfParcel = IDAL.DataSource.packets;
                Parcel p = new Parcel();
                Drone d = new Drone();

                for (int i = 0; i < runOfParcel.Count(); i++)
                {
                    if (runOfParcel[i].Id == ParcelId)
                    {
                        p = runOfParcel[i];
                    }
                }
                for (int i = 0; i < runOfParcel.Count(); i++)
                {
                    if (runOfDrone[i].id == DronId)
                    {
                        d = runOfDrone[i];
                    }
                }

                if (d.status == DroneStatuses.available)
                {
                    if (d.MaxWeight == p.Weight)
                    {
                        p.DroneId = d.id;
                        p.Scheduled = DateTime.Now;
                        return;
                    }
                }


            }
            /// <summary>
            /// If the package ID number matches the drone's ID number then it will collect the package,
            /// The drone's status changes for transpot and we will delete the old drone from the list
            /// </summary>
            /// <param name="p"></param>
            public static void collection(int ParcelId)

            {
                Parcel p = new Parcel();
                List<Parcel> runOfParcel = IDAL.DataSource.packets;
                for (int i = 0; i < runOfParcel.Count(); i++)
                {
                    if (runOfParcel[i].Id == ParcelId)
                    {
                        p = runOfParcel[i];
                    }
                }
                List<Drone> run = IDAL.DataSource.drones;
                Drone temp = new Drone();

                for (int i = 0; i < run.Count(); i++)
                {
                    if (run[i].id == p.DroneId)
                    {
                        temp.id = run[i].id;
                        temp.MaxWeight = run[i].MaxWeight;
                        temp.Model = run[i].Model;
                        temp.status = DroneStatuses.transport;
                        temp.Battery = run[i].Battery;
                        run.Remove(run[i]);
                        run.Add(temp);
                        p.PickedUp = DateTime.Now;
                        return;

                    }

                }
            }
            public static void SendDroneTpCharge(int StationId, int DroneId)
            {
                Station tempStation = IDAL.DalObject.DalObject.ShowStation(StationId);
                Drone tempDrone = IDAL.DalObject.DalObject.ShowDrone(DroneId);
                tempDrone.status = DroneStatuses.maintenance;
                tempStation.ChargeSlots--;///עידכון עמדות טעינה
                IDAL.DataSource.drones.Add(tempDrone);
                IDAL.DataSource.drones.Remove(IDAL.DalObject.DalObject.ShowDrone(DroneId));
                IDAL.DataSource.Stations.Add(tempStation);///הוספת התחנה המעודכנת
                IDAL.DataSource.Stations.Remove(IDAL.DalObject.DalObject.ShowStation(StationId));
                DroneCharge tempDronecharge = new DroneCharge();
                tempDronecharge.Droneld = DroneId;
                tempDronecharge.StationId = StationId;
                IDAL.DataSource.DronesCharge.Add(tempDronecharge);
            }
            public static void ReleaseDroneFromChargeStation(int DroneId)
            {

                Drone tempDrone1 = IDAL.DalObject.DalObject.ShowDrone(DroneId);
                tempDrone1.status = DroneStatuses.available;
                IDAL.DataSource.drones.Add(tempDrone1);
                IDAL.DataSource.drones.Remove(IDAL.DalObject.DalObject.ShowDrone(DroneId));
                List<DroneCharge> runDronesCharge = IDAL.DataSource.DronesCharge;
                int save = 0;
                for (int i = 0; i < runDronesCharge.Count; i++)
                {
                    if (runDronesCharge[i].Droneld == DroneId)
                    {
                        save = runDronesCharge[i].StationId;
                        IDAL.DataSource.DronesCharge.Remove(runDronesCharge[i]);

                    }
                }
                Station s = IDAL.DalObject.DalObject.ShowStation(save);
                s.ChargeSlots++;
                IDAL.DataSource.Stations.Add(s);
                IDAL.DataSource.Stations.Remove(IDAL.DalObject.DalObject.ShowStation(save));
            }

            public static void PackageDalvery(int ParcelId)
            {
                Parcel p = new Parcel();
                List<Parcel> runOfParcel = IDAL.DataSource.packets;
                for (int i = 0; i < runOfParcel.Count(); i++)
                {
                    if (runOfParcel[i].Id == ParcelId)
                    {
                        p = runOfParcel[i];
                    }
                }
                List<Drone> run = IDAL.DataSource.drones;
                Drone temp = new Drone();
                for (int i = 0; i < run.Count(); i++)
                {
                    if (run[i].id == p.DroneId)
                    {
                        temp.id = run[i].id;
                        temp.MaxWeight = run[i].MaxWeight;
                        temp.Model = run[i].Model;
                        temp.status = DroneStatuses.available;
                        temp.Battery = run[i].Battery;
                        run.Remove(run[i]);
                        run.Add(temp);
                        p.Delivered = DateTime.Now;
                    }
                }
            }
            public static Station ShowStation(int id)
            {
                List<Station> run = IDAL.DataSource.Stations;
                Station temp = new Station();
                for (int i = 0; i < run.Count(); i++)
                {
                    if (run[i].Id == id)
                    {
                        temp = run[i];

                    }
                }
                return temp;
            }
            /// <summary>
            /// Print all drone's list
            /// </summary>

            public static Drone ShowDrone(int id)
            {
                List<Drone> run = IDAL.DataSource.drones;
                Drone temp = new Drone();
                for (int i = 0; i < run.Count(); i++)
                {
                    if (run[i].id == id)
                    {
                        temp = run[i];

                    }
                }
                return temp;
            }
            /// <summary>
            /// run on the packets list and print
            /// </summary>
            /// <param name="id"> Get the id of parcel </param>
            public static Parcel ShowParcel(int id)
            {
                List<Parcel> run = IDAL.DataSource.packets;
                Parcel temp = new Parcel();
                for (int i = 0; i < run.Count(); i++)
                {
                    if (run[i].Id == id)
                    {
                        temp = run[i];

                    }
                }
                return temp;

            }
            public static Customer ShowCustomer(int s)
            {
                List<Customer> run = IDAL.DataSource.customers;
                Customer temp = new Customer();
                for (int i = 0; i < run.Count; i++)
                {
                    if (run[i].Id == s)
                    {
                        temp = run[i];
                    }
                }
                return temp;
            }


            public static List<Parcel> ShowParcelId()
            {
                List<Parcel> temp = new List<Parcel>();
                List<Parcel> run = IDAL.DataSource.packets;
                for (int i = 0; i < run.Count; i++)
                {
                    if (run[i].DroneId == 0)
                    {
                        temp.Add(run[i]);
                    }

                }
                return temp;
            }

            public static List<Station> ShowStationAvailable()
            {
                List<Station> run = IDAL.DataSource.Stations;
                List<Station> temp = new List<Station>();
                for (int i = 0; i < run.Count; i++)
                {
                    if (run[i].ChargeSlots > 0)
                        temp.Add(run[i]);
                }
                return temp;
            }


            public static List<Station> ShowStationList()
            {  
                List<Station> temp = IDAL.DataSource.Stations;
                return temp;
            }
            public static List<Customer> ShowCustomerList()
            {
               
                List<Customer> temp = IDAL.DataSource.customers;
                return temp;
            }
            public static List<Drone> ShowDroneList()
            {
                
                List<Drone> temp = IDAL.DataSource.drones;
                return temp;
            }
            public static List<Parcel> ShowParcelList()
            {
                
                List<Parcel> temp = IDAL.DataSource.packets;
                return temp;
            }

        }
    }
}




