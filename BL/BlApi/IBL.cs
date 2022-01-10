using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using BL;
using DO;
using DalApi;

namespace BlApi
{
   public interface IBL
    {
        #region Customer
        public void AddCustomer(BO.Customer customer);
        public void UpdCustomer(int idCustomer, string nameCustomer = "", string newNumPhone = "");
        public IEnumerable<BO.CustomerToList> GetALLCostumerToList();
        #endregion

        #region parcel
        public void AddrParcel(BO.Parcel parcel);
        public void ConnectParcelToDrone(int droneid);
        public bool IfDronCanGoTo(Location a, Location b,BO.Weightcategories weightcategories, int dronid);
        public bool IfDronCanGoTo(Location a, Location b, int dronid);
        public bool IfDronCanTakeParcel(DO.Parcel parcel, int droneid);
        public IEnumerable<BO.ParcelToList> GetALLParcelToList();
        public IEnumerable<ParcelToList> GetALLParcelsNotConnectToDrone();
        public void SupplyParcelByDrone(int droneID);

        #endregion


        #region BaseStation

        public void AddBaseStation(BO.BaseStation station);
        public void UpdStation(int numStation, string nameStation = "", int countChargingSlots = 0);
        public DO.Station MinFarToStation(BO.Location location);
        public IEnumerable<BO.BaseStationToList> GetALLbaseStationToList();
        public IEnumerable<BO.BaseStationToList> GetALLStationWithFreeStation();

        #endregion

        #region Drone
        public void UpdateDrone(int id, string model);
        //public void AddDrone(BO.Drone drone);// למה אין את הפונקציה הזאת ????????????????????????????????????????????????????????????????????
        public void ReleaseDroneFromChargeStation(int droneId, int timeInCharging);
        public void PickUpPackage(int id);
        public void SendingDroneforCharging(int droneId);
        public IEnumerable<BO.DroneToList> GetALLDroneToList();
        public void AddDrone(BO.Drone drone, int stationId);     
        public DroneToList GetDroneToList(int dronId);
        public IEnumerable<DroneToList> GetALLDroneToList(Predicate<DroneToList> predicate = null);
        // public Drone GetDrone(int droneId);

        #endregion

    }
}
