using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;

namespace IDAL
{
    public interface DalObject
    {

        public int runNumber();
        public void add(Station s);
        public void add(Drone d);
        public void add(Customer c);
        public void add(Parcel p);
        public void ConnectParcelToDron(int ParcelId, int DronId);
        public void collection(int ParcelId);
        public void SendDroneTpCharge(int StationId, int DroneId);
        public void ReleaseDroneFromChargeStation(int DroneId);
        public void PackageDalvery(int ParcelId);
        public Station ShowStation(int id);
        public Drone ShowDrone(int id);
        public Parcel ShowParcel(int id);
        public Customer ShowCustomer(int s);
        public List<Parcel> ShowParcelId();
        public List<Station> ShowStationAvailable();
        public IEnumerable<Station> ShowStationList();
        public IEnumerable<Customer> ShowCustomerList();
        public IEnumerable<Drone> ShowDroneList();
        public IEnumerable<Parcel> ShowParcelList();
    }

}
