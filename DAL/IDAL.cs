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
        public List< Station> ShowStationAvailable();
        public IEnumerable<Station> ShowStationList();
        public void AdditionStation( Station st);
        public Station GetStation(int id);
        public bool CheckStation(int id);
        public void UpdStation(Station st);
        public void DelStation(int id);
         public IEnumerable<Station> GetStationByPerdicate(Predicate<Station> predicate);
        public Station ShowStations(int s);
         //public IEnumerable<Station> ShowStationList();
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
         public Parcel GetParcel(int id);
         public void AdditionParcel(Parcel c);
         public bool CheckParcel(int id);
        public IEnumerable<Parcel> GetALLParcel();
        public void UpdParcel(Parcel st);
         public void DelParcel(int id);
         public IEnumerable<Parcel> GetParcelByPerdicate(Predicate<Parcel> predicate);
         public Parcel ShowParcels(int s);
        //public IEnumerable<Parcel> ShowParcelList();
        #endregion

    }

}
