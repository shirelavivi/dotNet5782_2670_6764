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
        public BO.Customer GetCustomer(int id);
      
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
        public BO.Parcel GetParcel(int id);
        public IEnumerable<ParcelToList> GetALLParce(Predicate<ParcelToList> predicate = null);
        public void RemoveParcel(int id);

        #endregion


        #region BaseStation
        public BaseStation GetBaseStation(int id);
        public void AddBaseStation(BO.BaseStation station);
        public void UpdStation(int numStation, string nameStation = "", int countChargingSlots = 0);
        public DO.Station MinFarToStation(BO.Location location);
        public IEnumerable<BO.BaseStationToList> GetALLbaseStationToList();
        public IEnumerable<BO.BaseStationToList> GetALLStationWithFreeStation();
        public IEnumerable<BaseStationToList> GetAllStation(Predicate<BaseStationToList> predicate = null);

        #endregion

        #region Drone
        public void UpdateDrone(int id, string model);
        //public void AddDrone(BO.Drone drone);
        public void ReleaseDroneFromChargeStation(int droneId, int timeInCharging);
        public void PickUpPackage(int id);
        public void SendingDroneforCharging(int droneId);
        public IEnumerable<BO.DroneToList> GetALLDroneToList();
        public void AddDrone(BO.Drone drone, int stationId);     
        public DroneToList GetDroneToList(int dronId);
        public BO.Drone GetDrone(int droneId);
        public IEnumerable<DroneToList> GetALLDroneToList(Predicate<DroneToList> predicate = null);
        // public Drone GetDrone(int droneId);
        #endregion
        public void AddUser(BO.User user);
        public BO.User GetUser(int pass);      
        public IEnumerable<BO.User> GetALLusers();
        public IEnumerable<BO.User> GetAllUsersByPredicat(Predicate<BO.User> predicate = null);

        public double DistanceTo(double lat1, double lon1, double lat2, double lon2, char unit = 'K');


        void SimulatorMod(int droneId, Action updateView, Func<bool> stopSimulator); //for the simulator mod

    }
}
