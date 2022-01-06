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
       
        public void AddStation(Station c)
        {

            if (CheckStation(c.Id))

                throw new DO.DuplicateIdException(c.Id, "Station");


            DAL.DataSource.Stations.Add(c);

        }

        public bool CheckStation(int id)
        {
            return DAL.DataSource.Stations.Any(st => st.Id == id);
        }


        public void UpdStation(Station st)
        {
            int count = DAL.DataSource.Stations.RemoveAll(st => st.Id == st.Id);

            if (count == 0)
                throw new MissingIdException(st.Id, "Station");

            DAL.DataSource.Stations.Add(st);
        }

        public void DelStation(int id)
        {
            int count = DAL.DataSource.Stations.RemoveAll(st => st.Id == id);

            if (count == 0)
                throw new MissingIdException(id, "Station");
        }

        public IEnumerable<Station> GetStationByPerdicate(Predicate<Station> predicate)
        {
            return from st in DAL.DataSource.Stations
                   where predicate(st)
                   select st;
        }
        public Station GetStation(int s)
        {
            if (!CheckStation(s))
                throw new MissingIdException(s, "Station");

            Station st = DAL.DataSource.Stations.Find(st => st.Id == s);
            return st;

        }
        public IEnumerable<Station> GetALLStations()
        {

            return from st in DAL.DataSource.Stations select st;
        }
    }
}

