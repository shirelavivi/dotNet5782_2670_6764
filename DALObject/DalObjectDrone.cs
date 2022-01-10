using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dal.DataSource;
using DO;
using System.Collections.Generic;
using DalApi;

namespace Dal
{
    sealed partial class DalObject : IDal

    {
        public void AddDrone(Drone c)
        {

            if (CheckDrone(c.id))

                throw new DO.DuplicateIdException(c.id, "Drone");


            Dal.DataSource.drones.Add(c);

        }

        public bool CheckDrone(int id)
        {
            return Dal.DataSource.drones.Any(st => st.id == id);
        }

        public void UpdDrone(Drone st)
        {
            int count = Dal.DataSource.customers.RemoveAll(st => st.Id == st.Id);

            if (count == 0)
                throw new MissingIdException(st.id, "Drone");

            Dal.DataSource.drones.Add(st);
        }

        public void DelDrone(int id)
        {
            int count = Dal.DataSource.drones.RemoveAll(st => st.id == id);

            if (count == 0)
                throw new MissingIdException(id, "Drone");
        }

        public IEnumerable<Drone> GetDronetsByPerdicate(Predicate<Drone> predicate)
        {
            return from st in Dal.DataSource.drones
                   where predicate(st)
                   select st;
        }
        public Drone GetDrone(int id)
        {
            if (!CheckDrone(id))
                throw new MissingIdException(id, "Drone");

            Drone st = Dal.DataSource.drones.Find(st => st.id == id);
            return st;
        }
        public IEnumerable<Drone> GetALLDrones()
        {

            return from st in Dal.DataSource.drones select st;
           
        }
       
    }
}
