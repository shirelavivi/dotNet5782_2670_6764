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
        public double[] batteryArr()
        {
           return ds.returnArrBattery();
        }
        public int runNumber()
        {

            return ds.runCounterPackets();
        }
        /// <summary>
        /// Add station to stations list
        /// </summary>
        /// <param name="s"></param>
       
        public void ConnectParcelToDron(int ParcelId, int DronId)// (מעודכן(קישור חבילה לרחפן
        {
            //    List<Drone> runOfDrone = IDAL.DataSource.drones;
            //    List<Parcel> runOfParcel = IDAL.DataSource.packets;
            Parcel p = new Parcel();
            Drone d = new Drone();
            int  i = IDAL.DataSource.packets.Count() + 1;
            p = GetParcel(ParcelId);
            d =GetDrone(DronId);     
            if (d.MaxWeight == p.Weight)//צריך לבדוק לא רק אם הרחפן יכול לשאת משקל זהה ....
            {
                p.DroneId = d.Id;
                p.Scheduled = DateTime.Now;
                IDAL.DataSource.packets[IDAL.DataSource.packets.FindIndex(ParcelId)] = p;//לבדוק איך עושים את הפיינד אינדקס

            }
        }
        /// <summary>
        /// If the package ID number matches the drone's ID number then it will collect the package,
        /// The drone's status changes for transpot and we will delete the old drone from the list
        /// </summary>
        /// <param name="p"></param>
        public void collection(int ParcelId)//(מעודכן)איסוף חבילה על ידי רחפן

        {
            Parcel p = new Parcel();
            int i;
            p = GetParcel(ParcelId);
            List<Drone> run = IDAL.DataSource.drones;

            for (int j = 0; j < run.Count(); j++)
            {
                if (run[j].Id == p.DroneId)
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
            Station tempStation = dl.GetStation(StationId);
            Drone tempDrone = dl.GetDrone(DroneId);
            //tempDrone.status = DroneStatuses.maintenance;
            tempStation.ChargeSlots--;///עידכון עמדות טעינה
            IDAL.DataSource.drones.Add(tempDrone);
            IDAL.DataSource.drones.Remove(dl.GetDrone(DroneId));
            IDAL.DataSource.Stations.Add(tempStation);///הוספת התחנה המעודכנת
            IDAL.DataSource.Stations.Remove(dl.GetStation(StationId));
            DroneCharge tempDronecharge = new DroneCharge();
            tempDronecharge.Droneld = DroneId;
            tempDronecharge.StationId = StationId;
            IDAL.DataSource.DronesCharge.Add(tempDronecharge);
        }
        public void ReleaseDroneFromChargeStation(int DroneId)//(מעודכן) ניתוק רחפן מעמדת טעינה
        {
            DalObject dl = new DalObject();
            Drone tempDrone1 = dl.GetDrone(DroneId);
            //tempDrone1.status = DroneStatuses.available;
            IDAL.DataSource.drones.Add(tempDrone1);
            IDAL.DataSource.drones.Remove(dl.GetDrone(DroneId));
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
            Station s = dl.GetStation(save);
            s.ChargeSlots++;
            IDAL.DataSource.Stations.Add(s);
            IDAL.DataSource.Stations.Remove(dl.GetStation(save));
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
                if (run[j].Id == p.DroneId)
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

        public int GetChargingRate()
        {
            return IDAL.DataSource.config.ChargingRate;
        }



    }
    
    
}





