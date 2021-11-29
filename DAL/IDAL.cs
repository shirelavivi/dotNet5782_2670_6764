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
        #endregion

        #region station
        public void AddStation(Station s);
        public Station ShowStation(int id);
        public List<Station> ShowStationAvailable();
        public IEnumerable<Station> ShowStationList();
        //public Station IsFoundStation(int id);
        #endregion


        #region drone
        public void AddDrone(Drone d);
        public void ConnectParcelToDron(int ParcelId, int DronId);
        public void SendDroneTpCharge(int StationId, int DroneId);
        public Drone ShowDrone(int id);
        //public Drone IsFoundDrone(int id);
        #endregion

        #region parcel
        public int runNumber();
        public void AddParcel(Parcel p);
       
       
        public void collection(int ParcelId);
        public void ReleaseDroneFromChargeStation(int DroneId);
        public void PackageDalvery(int ParcelId); 
        public Parcel ShowParcel(int id);
        public List<Parcel> ShowParcelId();
        public IEnumerable<Drone> ShowDroneList();
        public IEnumerable<Parcel> ShowParcelList();
        //public Parcel IsFoundParcel(int id);
        #endregion

    }

}
