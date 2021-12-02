//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//using IDAL.DO;
//namespace DalObject
//{
//    public partial class DalObject : Idal
//    {

//        public void AddDroneCharging(Station c)
//        {

//            if (CheckStation(c.Id))

//                throw new IDAL.DO.DuplicateIdException(c.Id, "Station");


//            IDAL.DataSource.Stations.Add(c);

//        }

//        public bool CheckDroneCharging(int id)
//        {
//            return IDAL.DataSource.Stations.Any(st => st.Id == id);
//        }


//        public void UpdSDroneCharging(DroneCharge st)
//        {
//            int count = IDAL.DataSource.Stations.RemoveAll(st => st.Id == st.Id);

//            if (count == 0)
//                throw new MissingIdException(st.Id, "Station");

//            IDAL.DataSource.Stations.Add(st);
//        }

//        public void DelStation(int id)
//        {
//            int count = IDAL.DataSource.Stations.RemoveAll(st => st.Id == id);

//            if (count == 0)
//                throw new MissingIdException(id, "DroneCharge");
//        }

//        public IEnumerable<DroneCharge> GetStationByPerdicate(Predicate<DroneCharge> predicate)
//        {
//            return from st in IDAL.DataSource.DroneCharge
//                   where predicate(st)
//                   select st;
//        }
//        public Station GetDroneCharge(int s)
//        {
//            if (!CheckStation(s))
//                throw new MissingIdException(s, "DroneCharge");

//            Station st = IDAL.DataSource.Stations.Find(st => st.Id == s);
//            return st;

//        }
//        public IEnumerable<Station> GetALLStations()
//        {

//            return from st in IDAL.DataSource.Stations select st;
//        }
//    }
//}

