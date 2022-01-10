using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using DalApi;
using Dal;
using static Dal.DataSource;



namespace Dal
{
    sealed partial class DalObject : IDal     
    {
    
       
        public void AddStation(Station c)
        {

            if (CheckStation(c.Id))

                throw new DO.DuplicateIdException(c.Id, "Station");
            

            Dal.DataSource.Stations.Add(c);

        }

        public bool CheckStation(int id)
        {
            return Dal.DataSource.Stations.Any(st => st.Id == id);
        }


        public void UpdStation(Station st)
        {
            int count = Dal.DataSource.Stations.RemoveAll(st => st.Id == st.Id);

            if (count == 0)
                throw new MissingIdException(st.Id, "Station");

            Dal.DataSource.Stations.Add(st);
        }

        public void DelStation(int id)
        {
            int count = Dal.DataSource.Stations.RemoveAll(st => st.Id == id);

            if (count == 0)
                throw new MissingIdException(id, "Station");
        }

        public IEnumerable<Station> GetStationByPerdicate(Predicate<Station> predicate)
        {
            return from st in Dal.DataSource.Stations
                   where predicate(st)
                   select st;
        }
        public Station GetStation(int s)
        {
            if (!CheckStation(s))
                throw new MissingIdException(s, "Station");

            Station st = Dal.DataSource.Stations.Find(st => st.Id == s);
            return st;

        }
        public IEnumerable<Station> GetALLStations()
        {

            return from st in Dal.DataSource.Stations select st;
        }
    }
}

