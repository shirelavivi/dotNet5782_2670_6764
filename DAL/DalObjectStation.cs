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
        public Station GetStation(int id)
        {
            if (!CheckStation(id))
                throw new MissingIdException(id, "Station");

            Station st = IDAL.DataSource.Stations.Find(st => st.Id == id);
            return st;
        }
        public void AdditionStation(Station c)
        {

            if (CheckStation(c.Id))

                throw new IDAL.DO.DuplicateIdException(c.Id, "Station");


            IDAL.DataSource.Stations.Add(c);

        }

        public bool CheckStation(int id)
        {
            return IDAL.DataSource.Stations.Any(st => st.Id == id);
        }

        public IEnumerable<Station> GetALLStations()
        {
            return from st in IDAL.DataSource.Stations select st;
            //return DataSource.Customers;
        }



        public void UpdStation(Station st)
        {
            int count = IDAL.DataSource.Stations.RemoveAll(st => st.Id == st.Id);

            if (count == 0)
                throw new MissingIdException(st.Id, "Station");

            IDAL.DataSource.Stations.Add(st);
        }

        public void DelStation(int id)
        {
            int count = IDAL.DataSource.Stations.RemoveAll(st => st.Id == id);

            if (count == 0)
                throw new MissingIdException(id, "Station");
        }

        public IEnumerable<Station> GetStationByPerdicate(Predicate<Station> predicate)
        {
            return from st in IDAL.DataSource.Stations
                   where predicate(st)
                   select st;
        }
        public Station ShowStations(int s)
        {
            List<Station> run = IDAL.DataSource.Stations;
            Station temp = new Station();
            for (int i = 0; i < run.Count; i++)
            {
                if (run[i].Id == s)
                {
                    temp = run[i];
                }
            }
            return temp;

        }
    //    public IEnumerable<Station> ShowStationList()//כפילות בפונציה יש אותה פעמיים בשמות שונים 
    //    {

    //        List<Station> temp = new List<Station>();
    //        foreach (Station item in IDAL.DataSource.Stations)
    //        {
    //            temp.Add(item);
    //        }

    //        return temp;
    //    }
    }
}

