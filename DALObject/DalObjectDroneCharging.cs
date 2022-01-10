using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dal.DataSource;
using DO;
using DalApi;


namespace Dal
{
    sealed partial class DalObject : IDal
    {

        public void AddDroneCharging(DroneCharge c)
        {

            if (CheckDroneCharging(c.StationId))

                throw new DO.DuplicateIdException(c.StationId, "DroneCharge");


            Dal.DataSource.DronesCharge.Add(c);

        }

        public bool CheckDroneCharging(int id)
        {
            return Dal.DataSource.DronesCharge.Any(st => st.StationId == id);
        }


        public void UpdDroneCharging(DroneCharge st)
        {
            int count = Dal.DataSource.DronesCharge.RemoveAll(st => st.Droneld == st.Droneld);

            if (count == 0)
                throw new MissingIdException(st.Droneld, "DroneCharge");

            Dal.DataSource.DronesCharge.Add(st);
        }

        public void DelDroneCharge(int droneId)
        {
            int count = Dal.DataSource.DronesCharge.RemoveAll(st => st.Droneld == droneId);

            if (count == 0)
                throw new MissingIdException(droneId, "DroneCharge");
        }

        public IEnumerable<DroneCharge> 
            ByPerdicate(Predicate<DroneCharge> predicate)
        {
            return from st in Dal.DataSource.DronesCharge
                   where predicate(st)
                   select st;
        }
        public DroneCharge GetDroneCharge(int stationId)
        {
            if (!CheckDroneCharging(stationId))
                throw new MissingIdException(stationId, "DroneCharge");

          DroneCharge st = Dal.DataSource.DronesCharge.Find(st => st.StationId  == stationId);
            return st;

        }
        public IEnumerable<DroneCharge> GetALLDroneCharge()
        {

            return from st in Dal.DataSource.DronesCharge select st;
        }
    }
}

