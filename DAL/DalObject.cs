﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DO;


namespace DalObject
{

    public partial class DalObject : Idal
    {
        static readonly Idal instance = new DalObject();
        public static Idal Instance { get => instance; }
        DAL.DataSource ds;
        DAL.DataSource.config ds1;
        //DalObject dl;/*= new DalObject();*/
        public static DalObject()
        {
            //ds = new DAL.DataSource();
            //ds1 = new DAL.DataSource.config();
            DataSource.Initialize();
        }
        public double[] batteryArr()
        {
           return ds1.returnArrBattery();
        }
        public int runNumber()
        {

            return ds1.runCounterPackets();
        }
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
                int i = DAL.DataSource.packets.Count() + 1;
                p = GetParcel(ParcelId);
                d = GetDrone(DronId);
                p.DroneId = d.id;
                p.Scheduled = DateTime.Now;
                DAL.DataSource.packets[DAL.DataSource.packets.FindIndex(x => x.Id == p.Id)] = p;//לבדוק איך עושים את הפיינד אינדקס
            }
            catch(MissingIdException)
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
                Station tempStation = GetStation(StationId);
                Drone tempDrone = GetDrone(DroneId);   
                tempStation.ChargeSlots--;///עידכון עמדות טעינה
                DAL.DataSource.drones.Add(tempDrone);
                DAL.DataSource.drones.Remove(GetDrone(DroneId));
                DAL.DataSource.Stations.Add(tempStation);///הוספת התחנה המעודכנת
                DAL.DataSource.Stations.Remove(GetStation(StationId));
                DroneCharge tempDronecharge = new DroneCharge();
                tempDronecharge.Droneld = DroneId;
                tempDronecharge.StationId = StationId;
                DAL.DataSource.DronesCharge.Add(tempDronecharge);
            }
            catch (DAL.DO.MissingIdException ex)
            {
                throw new MissingIdException(ex.ID, ex.EntityName);
            }
        }
        public void ReleaseDroneFromChargeStation(int DroneId)//(שחרור רחפן לטעינה
        {
            try
            {

                //DalObject dl = new DalObject();
                Drone tempDrone1 = GetDrone(DroneId);
                //tempDrone1.status = DroneStatuses.available;
                //IDAL.DataSource.drones.Add(tempDrone1);
                //IDAL.DataSource.drones.Remove(GetDrone(DroneId));
                //List<DroneCharge> runDronesCharge = IDAL.DataSource.DronesCharge;
                int save = 0;
                for (int i = 0; i < DAL.DataSource.DronesCharge.Count; i++)
                {
                    if (DAL.DataSource.DronesCharge[i].Droneld == DroneId)
                    {
                        save = DAL.DataSource.DronesCharge[i].StationId;
                        DAL.DataSource.DronesCharge.Remove(DAL.DataSource.DronesCharge[i]);

                    }
                }
                Station s = GetStation(save);
                s.ChargeSlots++;
                DAL.DataSource.Stations.Add(s);
                DAL.DataSource.Stations.Remove(GetStation(save));
            }
            catch (DAL.DO.MissingIdException ex)
            {
                throw new MissingIdException(ex.ID, ex.EntityName);
            }
        }

        public void collection(int ParcelId)//(מעודכן)איסוף חבילה על ידי רחפן

        {
            Parcel p = new Parcel();
            p = GetParcel(ParcelId);
            DAL.DataSource.packets.Remove(p);
            p.PickedUp = DateTime.Now;
            DAL.DataSource.packets.Add(p);
        }
        public void PackageDalvery(int ParcelId)//אספקת חבילה על ידי רחפן
        {
           
            Parcel p = new Parcel();
            p = GetParcel(ParcelId);
            DAL.DataSource.packets.Remove(p);
            p.Delivered = DateTime.Now;
            DAL.DataSource.packets.Add(p);
            
        }
       

        public List<Station> ShowStationAvailable()
        {
            List<Station> run = DAL.DataSource.Stations;
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
            return DAL.DataSource.config.ChargingRate;
        }

        private class Idal
        {
        }
    }
    
    
}





