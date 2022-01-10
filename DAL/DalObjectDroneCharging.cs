using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IDAL.DO;
namespace DalObject
{
    public partial class DalObject : Idal
    {

        public void AddDroneCharging(DroneCharge c)
        {

            if (CheckDroneCharging(c.StationId))

                throw new IDAL.DO.DuplicateIdException(c.StationId, "DroneCharge");


            IDAL.DataSource.DronesCharge.Add(c);

        }

        public bool CheckDroneCharging(int id)
        {
            return IDAL.DataSource.DronesCharge.Any(st => st.StationId == id);
        }


        public void UpdDroneCharging(DroneCharge st)
        {
            int count = IDAL.DataSource.DronesCharge.RemoveAll(st => st.Droneld == st.Droneld);

            if (count == 0)
                throw new MissingIdException(st.Droneld, "DroneCharge");

            IDAL.DataSource.DronesCharge.Add(st);
        }

        public void DelDroneCharge(int droneId)
        {
            int count = IDAL.DataSource.DronesCharge.RemoveAll(st => st.Droneld == droneId);

            if (count == 0)
                throw new MissingIdException(droneId, "DroneCharge");
        }

        public IEnumerable<DroneCharge>
            ByPerdicate(Predicate<DroneCharge> predicate)
        {
            return from st in IDAL.DataSource.DronesCharge
                   where predicate(st)
                   select st;
        }
        public DroneCharge GetDroneCharge(int stationId)
        {
            if (!CheckDroneCharging(stationId))
                throw new MissingIdException(stationId, "DroneCharge");

            DroneCharge st = IDAL.DataSource.DronesCharge.Find(st => st.StationId == stationId);
            return st;

        }
        public IEnumerable<DroneCharge> GetALLDroneCharge()
        {

            return from st in IDAL.DataSource.DronesCharge select st;
        }
    }
}
