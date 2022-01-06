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
        public IEnumerable<Customer> ShowCustomerList();
        public Customer GetCustomer(int id);
        public bool CheckCusromer(int id);
        public IEnumerable<Customer> GetALLCustomers();
        public void UpdCustomer(Customer st);
        public void DelCustomer(int id);
        public IEnumerable<Customer> GetCustomertsByPerdicate(Predicate<Customer> predicate);
        #endregion

        #region station
        public void AddStation(Station s);
        public List<Station> ShowStationAvailable();
        public IEnumerable<Station> GetALLStations();
        public bool CheckStation(int id);
        public void UpdStation(Station st);
        public void DelStation(int id);
        public IEnumerable<Station> GetStationByPerdicate(Predicate<Station> predicate);
        public Station GetStation(int s);
        public int GetChargingRate();

        #endregion

        #region drone
        public void AddDrone(Drone c);
        public void ConnectParcelToDron(int ParcelId, int DronId);
        public void SendDroneTpCharge(int StationId, int DroneId);
        public Drone GetDrone(int id);
        public bool CheckDrone(int id);
        public IEnumerable<Drone> GetALLDrones();
        public void UpdDrone(Drone st);
        public void DelDrone(int id);
        public IEnumerable<Drone> GetDronetsByPerdicate(Predicate<Drone> predicate);
        public double[] batteryArr();
        public IEnumerable<DroneCharge> GetALLDroneCharge();

        #endregion

        #region parcel
        public int runNumber();
        public void AddParcel(Parcel p);
        public void collection(int ParcelId);
        public void ReleaseDroneFromChargeStation(int DroneId);
        public void PackageDalvery(int ParcelId);

        public bool CheckParcel(int id);
        public IEnumerable<Parcel> GetALLParcel();
        public void UpdParcel(Parcel st);
        public void DelParcel(int id);
        public IEnumerable<Parcel> GetParcelByPerdicate(Predicate<Parcel> predicate);
        public Parcel GetParcel(int s);

        #endregion

    }
}



