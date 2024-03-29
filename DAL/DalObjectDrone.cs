﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IDAL.DO;
namespace DalObject
{

    public partial class DalObject : Idal
    {

        public void AddDrone(Drone c)
        {

            if (CheckDrone(c.id))

                throw new IDAL.DO.DuplicateIdException(c.id, "Drone");


            IDAL.DataSource.drones.Add(c);

        }

        public bool CheckDrone(int id)
        {
            return IDAL.DataSource.drones.Any(st => st.id == id);
        }

        public void UpdDrone(Drone st)
        {
            int count = IDAL.DataSource.customers.RemoveAll(st => st.Id == st.Id);

            if (count == 0)
                throw new MissingIdException(st.id, "Drone");

            IDAL.DataSource.drones.Add(st);
        }

        public void DelDrone(int id)
        {
            int count = IDAL.DataSource.drones.RemoveAll(st => st.id == id);

            if (count == 0)
                throw new MissingIdException(id, "Drone");
        }

        public IEnumerable<Drone> GetDronetsByPerdicate(Predicate<Drone> predicate)
        {
            return from st in IDAL.DataSource.drones
                   where predicate(st)
                   select st;
        }
        public Drone GetDrone(int id)
        {
            if (!CheckDrone(id))
                throw new MissingIdException(id, "Drone");

            Drone st = IDAL.DataSource.drones.Find(st => st.id == id);
            return st;
        }
        public IEnumerable<Drone> GetALLDrones()
        {

            return from st in IDAL.DataSource.drones select st;
            //return DataSource.Customers;
        }
        public IEnumerable<Drone> GetALLDroneToList(Predicate<Drone> predicate)
        {
            return from dron in IDAL.DataSource.drones
                   where predicate(dron)
                   select dron;

        }
    }
}
