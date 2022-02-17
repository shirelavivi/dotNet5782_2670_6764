using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace DalApi

{
    public interface IDal
    {

        #region customer

        public void AddCustomer(Customer c);
        public bool CheckCusromer(int id);
        public IEnumerable<Customer> GetALLCustomers();
        public void UpdCustomer(Customer st);
        public void DelCustomer(int id);
        public IEnumerable<Customer> GetCustomertsByPerdicate(Predicate<Customer> predicate);
        public Customer GetCustomer(int id);







       
        
        
        #endregion

        #region station
        public void AddStation(Station s);
        //public List<Station> ShowStationAvailable();
        public IEnumerable<Station> GetALLStations();
        public bool CheckStation(int id);
        public void UpdStation(Station st);
        public void DelStation(int id);
        public IEnumerable<Station> GetStationByPerdicate(Predicate<Station> predicate);
        public Station GetStation(int s);
        //public int GetChargingRate();

        #endregion

        #region drone
        public void AddDrone(Drone c);
        public bool CheckDrone(int id);
        public void UpdDrone(Drone st);
        public void DelDrone(int id);
        public IEnumerable<Drone> GetDronetsByPerdicate(Predicate<Drone> predicate);
        public Drone GetDrone(int id);
        public IEnumerable<Drone> GetALLDrones();

        #endregion

        #region parcel
        public void AddParcel(Parcel p);
        public bool CheckParcel(int id);
        public void UpdParcel(Parcel st);
        public void DelParcel(int id);
        public IEnumerable<Parcel> GetParcelByPerdicate(Predicate<Parcel> predicate);

        public Parcel GetParcel(int s);
        public IEnumerable<Parcel> GetALLParcel();



        //public void collection(int ParcelId);
        //public void ReleaseDroneFromChargeStation(int DroneId);
        //public void PackageDalvery(int ParcelId);







        #endregion

        #region droneCharge
        public void AddDroneCharging(DroneCharge c);
       

        public bool CheckDroneCharging(int id);
        


        public void UpdDroneCharging(DroneCharge st);
       

        public void DelDroneCharge(int droneId);
     
        public IEnumerable<DroneCharge> ByPerdicate(Predicate<DroneCharge> predicate);


        public DroneCharge GetDroneCharge(int stationId);

        public IEnumerable<DroneCharge> GetALLDroneCharge();
        public int GetrunNumberPackage();

        #endregion

        public void AddUser(User c);
        public bool CheckUser(int passWord);
        public User GetUser(int s);
        public IEnumerable<User> GetALLUsers();
       
        




        public void SendDroneTpCharge(int StationId, int DroneId);
        public void ConnectParcelToDron(int ParcelId, int DronId);
        public void ReleaseDroneFromChargeStation(int DroneId);
        public void collection(int ParcelId);
        public void PackageDalvery(int ParcelId);
        public List<Station> ShowStationAvailable();
        public int GetChargingRate();
        public double[] batteryArr();
    }
}



