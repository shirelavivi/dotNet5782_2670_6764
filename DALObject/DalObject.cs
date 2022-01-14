using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using Dal;
using static Dal.DataSource;
using static Dal.DalObject;
using DO;


namespace Dal
{
    sealed partial class DalObject : IDal
    {
        static readonly IDal instance = new Dal.DalObject();
        public static IDal Instance { get => instance; }
        //Dal.DataSource ds;
        //Dal.DataSource.config ds1;
        //DalObject dl;/*= new DalObject();*/
        //static string droneChargingPath = @"droneChargingPath.xml";
        ///*למחוק אחכ זמני*/
        //static string customerPath = @"customerPathXml.xml";
        //static string stationPath = @"stationPathXml.xml";
        //static string dronePath = @"dronePathXml.xml";
        //static string parcelPath = @"parcelPathXml.xml";
        public DalObject()
        {
            DataSource.Initialize();
            /**/
            //XMLTools.SaveListToXMLSerializer(customers, customerPath);
            //XMLTools.SaveListToXMLSerializer(drones, dronePath);
            //XMLTools.SaveListToXMLSerializer(packets, parcelPath);
            ////XMLTools.SaveListToXMLElement(DronesCharge, droneChargingPath);
            //XMLTools.SaveListToXMLSerializer(Stations, stationPath);
        }
        public double[] batteryArr()
        {
            return DataSource.config.returnArrBattery();
        }
        //public int runNumber()
        //{

        //    return ds1.runCounterPackets();
        //}



        /// <summary>
        /// Add station to stations list
        /// </summary>
        /// <param name="s"></param>

        public void ConnectParcelToDron(int ParcelId, int DronId)// (מעודכן(קישור חבילה לרחפן
        {
            try
            {
                Parcel p = new Parcel();
                Drone d = new Drone();
                int i = Dal.DataSource.packets.Count() + 1;
                p = instance.GetParcel(ParcelId);
                d = instance.GetDrone(DronId);
                p.DroneId = d.id;
                p.Scheduled = DateTime.Now;
                Dal.DataSource.packets[Dal.DataSource.packets.FindIndex(x => x.Id == p.Id)] = p;//לבדוק איך עושים את הפיינד אינדקס
            }
            catch (MissingIdException)
            {
                throw new MissingIdException(ParcelId, "parcel");
            }

        }
        /// <summary>
        /// If the package ID number matches the drone's ID number then it will collect the package,
        /// The drone's status changes for transpot and we will delete the old drone from the list
        /// </summary>
        /// <param name="p"></param>

        public void SendDroneTpCharge(int StationId, int DroneId)///)מעודכן)שליחת  רחפן לטעינה
        {
            try
            {
                Station tempStation = instance.GetStation(StationId);
                Drone tempDrone = instance. GetDrone(DroneId);
                tempStation.ChargeSlots--;///עידכון עמדות טעינה
                Dal.DataSource.drones.Add(tempDrone);
                Dal.DataSource.drones.Remove(instance.GetDrone(DroneId));
                Dal.DataSource.Stations.Add(tempStation);///הוספת התחנה המעודכנת
                Dal.DataSource.Stations.Remove(instance.GetStation(StationId));
                DroneCharge tempDronecharge = new DroneCharge();
                tempDronecharge.Droneld = DroneId;
                tempDronecharge.StationId = StationId;
                Dal.DataSource.DronesCharge.Add(tempDronecharge);
            }
            catch (DO.MissingIdException ex)
            {
                throw new MissingIdException(ex.ID, ex.EntityName);
            }
        }
        public void ReleaseDroneFromChargeStation(int DroneId)//(שחרור רחפן לטעינה
        {
            try
            {

                //DalObject dl = new DalObject();
                Drone tempDrone1 = instance.GetDrone(DroneId);
                //tempDrone1.status = DroneStatuses.available;
                //IDAL.DataSource.drones.Add(tempDrone1);
                //IDAL.DataSource.drones.Remove(GetDrone(DroneId));
                //List<DroneCharge> runDronesCharge = IDAL.DataSource.DronesCharge;
                int save = 0;
                for (int i = 0; i < Dal.DataSource.DronesCharge.Count; i++)
                {
                    if (Dal.DataSource.DronesCharge[i].Droneld == DroneId)
                    {
                        save = Dal.DataSource.DronesCharge[i].StationId;
                        Dal.DataSource.DronesCharge.Remove(Dal.DataSource.DronesCharge[i]);

                    }
                }
                Station s = instance.GetStation(save);
                s.ChargeSlots++;
                Dal.DataSource.Stations.Add(s);
                Dal.DataSource.Stations.Remove(instance.GetStation(save));
            }
            catch (DO.MissingIdException ex)
            {
                throw new MissingIdException(ex.ID, ex.EntityName);
            }
        }

        public void collection(int ParcelId)//(מעודכן)איסוף חבילה על ידי רחפן

        {
            Parcel p = new Parcel();
            p = instance.GetParcel(ParcelId);
            Dal.DataSource.packets.Remove(p);
            p.PickedUp = DateTime.Now;
            Dal.DataSource.packets.Add(p);
        }
        public void PackageDalvery(int ParcelId)//אספקת חבילה על ידי רחפן
        {

            Parcel p = new Parcel();
            p = instance.GetParcel(ParcelId);
            Dal.DataSource.packets.Remove(p);
            p.Delivered = DateTime.Now;
            Dal.DataSource.packets.Add(p);

        }


        public List<Station> ShowStationAvailable()
        {
            List<Station> run = Dal.DataSource.Stations;
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
            return Dal.DataSource.config.ChargingRate;
        }
        public int GetrunNumberPackage()
        {
            return Dal.DataSource.config.CounterPackets;
        }

        //private class Idal
        //{
        //}
    }


}









