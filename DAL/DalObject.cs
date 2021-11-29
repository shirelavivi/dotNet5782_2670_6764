using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IDAL.DO;
namespace DalObject
{

    public partial class DalObject : Idal
    {

        IDAL.DataSource.config ds = new IDAL.DataSource.config();
        public DalObject()
        {
            ds.Initialize();
        }
        public int runNumber()
        {

            return ds.runCounterPackets();
        }
        /// <summary>
        /// Add station to stations list
        /// </summary>
        /// <param name="s"></param>
        public void AddStation(Station s)
        {
            IDAL.DataSource.Stations.Add(s);
        }
        
        public void AddParcel(Parcel p)
        {
            IDAL.DataSource.packets.Add(p);

        }
        public void ConnectParcelToDron(int ParcelId, int DronId)// (מעודכן(קישור חבילה לרחפן
        {
            //    List<Drone> runOfDrone = IDAL.DataSource.drones;
            //    List<Parcel> runOfParcel = IDAL.DataSource.packets;
            Parcel p = new Parcel();
            Drone d = new Drone();
            int j, i = IDAL.DataSource.packets.Count() + 1;
            try
            {
                for (i = 0; i < IDAL.DataSource.packets.Count(); i++)
                {
                    if (IDAL.DataSource.packets[i].Id == ParcelId)
                    {
                        p = IDAL.DataSource.packets[i];
                        break;
                    }
                }
            }
            catch (MissingIdException ec)
            {
                Console.WriteLine("My Exc: ");
                Console.WriteLine(ec);

            }
            try
            {
                for (j = 0; j < IDAL.DataSource.drones.Count(); j++)
                {
                    if (IDAL.DataSource.drones[j].id == DronId)
                    {
                        d = IDAL.DataSource.drones[j];
                        break;
                    }
                }
            }
            catch (MissingIdException ec)
            {
                Console.WriteLine("My Exc: ");
                Console.WriteLine(ec);

            }

            if (d.MaxWeight == p.Weight)//צריך לבדוק לא רק אם הרחפן יכול לשאת משקל זהה ....
            {
                p.DroneId = d.id;
                p.Scheduled = DateTime.Now;
                IDAL.DataSource.packets[i] = p;

            }

        }
        /// <summary>
        /// If the package ID number matches the drone's ID number then it will collect the package,
        /// The drone's status changes for transpot and we will delete the old drone from the list
        /// </summary>
        /// <param name="p"></param>
        public void collection(int ParcelId)//(מעודכן)איסוף חבעלה על ידי רחפן

        {
            Parcel p = new Parcel();
            int i;
            for (i = 0; i < IDAL.DataSource.packets.Count(); i++)
            {
                if (IDAL.DataSource.packets[i].Id == ParcelId)
                {
                    p = IDAL.DataSource.packets[i];
                    break;
                }
            }
            List<Drone> run = IDAL.DataSource.drones;

            for (int j = 0; j < run.Count(); j++)
            {
                if (run[j].id == p.DroneId)
                {

                    Drone temp = new Drone();
                    //temp.status = DroneStatuses.transport;
                    run[j] = temp;
                    Parcel temp2 = IDAL.DataSource.packets[i];
                    temp2.PickedUp = DateTime.Now;
                    IDAL.DataSource.packets[i] = temp2;
                    return;

                }

            }
        }
        public void SendDroneTpCharge(int StationId, int DroneId)///)מעודכן)שליחת רחפן לטעינה
        {
            DalObject dl = new DalObject();
            Station tempStation = dl.ShowStation(StationId);
            Drone tempDrone = dl.ShowDrone(DroneId);
            //tempDrone.status = DroneStatuses.maintenance;
            tempStation.ChargeSlots--;///עידכון עמדות טעינה
            IDAL.DataSource.drones.Add(tempDrone);
            IDAL.DataSource.drones.Remove(dl.ShowDrone(DroneId));
            IDAL.DataSource.Stations.Add(tempStation);///הוספת התחנה המעודכנת
            IDAL.DataSource.Stations.Remove(dl.ShowStation(StationId));
            DroneCharge tempDronecharge = new DroneCharge();
            tempDronecharge.Droneld = DroneId;
            tempDronecharge.StationId = StationId;
            IDAL.DataSource.DronesCharge.Add(tempDronecharge);
        }
        public void ReleaseDroneFromChargeStation(int DroneId)//(מעודכן) ניתוק רחפן מעמדת טעינה
        {
            DalObject dl = new DalObject();
            Drone tempDrone1 = dl.ShowDrone(DroneId);
            //tempDrone1.status = DroneStatuses.available;
            IDAL.DataSource.drones.Add(tempDrone1);
            IDAL.DataSource.drones.Remove(dl.ShowDrone(DroneId));
            //List<DroneCharge> runDronesCharge = IDAL.DataSource.DronesCharge;
            int save = 0;
            for (int i = 0; i < IDAL.DataSource.DronesCharge.Count; i++)
            {
                if (IDAL.DataSource.DronesCharge[i].Droneld == DroneId)
                {
                    save = IDAL.DataSource.DronesCharge[i].StationId;
                    IDAL.DataSource.DronesCharge.Remove(IDAL.DataSource.DronesCharge[i]);

                }
            }
            Station s = dl.ShowStation(save);
            s.ChargeSlots++;
            IDAL.DataSource.Stations.Add(s);
            IDAL.DataSource.Stations.Remove(dl.ShowStation(save));
        }

        public void PackageDalvery(int ParcelId)//)מעודכן) איסוף חבילה עך ידי רחפן
        {
            int i, j;
            Parcel p = new Parcel();
            List<Parcel> runOfParcel = IDAL.DataSource.packets;
            for (i = 0; i < runOfParcel.Count(); i++)
            {
                if (runOfParcel[i].Id == ParcelId)
                {
                    p = runOfParcel[i];
                    break;
                }
            }
            List<Drone> run = IDAL.DataSource.drones;

            for (j = 0; j < run.Count(); j++)
            {
                if (run[j].id == p.DroneId)
                {
                    Drone temp = run[j];
                    //temp.status = DroneStatuses.available;
                    run[j] = temp;
                    Parcel temp2 = runOfParcel[i];
                    temp2.Delivered = DateTime.Now;
                    runOfParcel[i] = temp2;

                }
            }
        }
        public Station ShowStation(int id)
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

        
        /// <summary>
        /// run on the packets list and print
        /// </summary>
        /// <param name="id"> Get the id of parcel </param>
        public Parcel ShowParcel(int id)
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
       


        public List<Parcel> ShowParcelId()
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

        public List<Station> ShowStationAvailable()
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


        public IEnumerable<Station> ShowStationList()
        {
            List<Station> temp = new List<Station>();
            foreach (Station item in IDAL.DataSource.Stations)
            {
                temp.Add(item);
            }

            return temp;
        }
        
     
        public IEnumerable<Parcel> ShowParcelList()
        {

            List<Parcel> temp = new List<Parcel>();
            foreach (Parcel item in IDAL.DataSource.packets)
            {
                temp.Add(item);
            }

            return temp;
        }

        //public Station IsFoundStation(int id)
        //{
        //    foreach (Station item in IDAL.DataSource.Stations)
        //    {
        //        if (item.Id == id)
        //            return item;

        //    }
        //    throw new IDAL.DO.MissingIdException(id, "Station");

        //}
        //public Drone IsFoundDrone(int id)
        //{
        //    foreach (Drone item in IDAL.DataSource.drones)
        //    {
        //        if (item.id == id)
        //            return item;

        //    }

        //    throw new IDAL.DO.MissingIdException(id, "Drone");

        //}
        //public Parcel IsFoundParcel(int id)
        //{
        //    foreach (Parcel item in IDAL.DataSource.packets)
        //    {
        //        if (item.Id == id)
        //            return item;

        //    }
        //    throw new IDAL.DO.MissingIdException(id, "Parcel");

        //}

    }
    
    
}





