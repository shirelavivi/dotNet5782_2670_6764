using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DAL.DataSource;
using DO;

namespace DalObject
{
    sealed partial class DalObject : Idal
    {

        public void AddDroneCharging(DroneCharge c)
        {

            if (CheckDroneCharging(c.StationId))

                throw new DO.DuplicateIdException(c.StationId, "DroneCharge");


            DAL.DataSource.DronesCharge.Add(c);

        }

        public bool CheckDroneCharging(int id)
        {
            return DAL.DataSource.DronesCharge.Any(st => st.StationId == id);
        }


        public void UpdDroneCharging(DroneCharge st)
        {
            int count = DAL.DataSource.DronesCharge.RemoveAll(st => st.Droneld == st.Droneld);

            if (count == 0)
                throw new MissingIdException(st.Droneld, "DroneCharge");

            DAL.DataSource.DronesCharge.Add(st);
        }

        public void DelDroneCharge(int droneId)
        {
            int count = DAL.DataSource.DronesCharge.RemoveAll(st => st.Droneld == droneId);

            if (count == 0)
                throw new MissingIdException(droneId, "DroneCharge");
        }

        public IEnumerable<DroneCharge> 
            ByPerdicate(Predicate<DroneCharge> predicate)
        {
            return from st in DAL.DataSource.DronesCharge
                   where predicate(st)
                   select st;
        }
        public DroneCharge GetDroneCharge(int stationId)
        {
            if (!CheckDroneCharging(stationId))
                throw new MissingIdException(stationId, "DroneCharge");

          DroneCharge st = DAL.DataSource.DronesCharge.Find(st => st.StationId  == stationId);
            return st;

        }
        public IEnumerable<DroneCharge> GetALLDroneCharge()
        {

            return from st in DAL.DataSource.DronesCharge select st;
        }
    }
}

