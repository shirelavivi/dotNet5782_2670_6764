using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;

namespace DalObject
{
    public interface Idal
    {
        #region customer
       
        public void AddCustomer(Customer c);
        public Customer ShowCustomer(int s);
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
        public Station ShowStation(int id);
        public List<Station> ShowStationAvailable();
        public IEnumerable<Station> ShowStationList();
        #endregion


        #region drone
        public void AddDrone(Drone c);
        public void ConnectParcelToDron(int ParcelId, int DronId);
        public void SendDroneTpCharge(int StationId, int DroneId);
        public Drone ShowDrone(int id);
        public Drone GetDrone(int id);
        public bool CheckDrone(int id);
        public IEnumerable<Drone> GetALLDrones();
        public void UpdDrone(Drone st);
        public void DelDrone(int id);
        public IEnumerable<Drone> GetDronetsByPerdicate(Predicate<Drone> predicate);
        public IEnumerable<Drone> ShowDroneList();  
        #endregion

        #region parcel
        public int runNumber();
        public void AddParcel(Parcel p); 
        public void collection(int ParcelId);
        public void ReleaseDroneFromChargeStation(int DroneId);
        public void PackageDalvery(int ParcelId); 
        public Parcel ShowParcel(int id);
        public List<Parcel> ShowParcelId();
        public IEnumerable<Parcel> ShowParcelList();
        #endregion

    }

}
