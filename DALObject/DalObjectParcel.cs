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
       
        public void AddParcel(Parcel p)
        {

            if (CheckParcel(p.Id))

                throw new DO.DuplicateIdException(p.Id, "Parcel");


            DAL.DataSource.packets.Add(p);

        }

        public bool CheckParcel(int id)
        {
            return DAL.DataSource.packets.Any(st => st.Id == id);
        }



        public void UpdParcel(Parcel st)
        {
            int count = DAL.DataSource.packets.RemoveAll(st => st.Id == st.Id);

            if (count == 0)
                throw new MissingIdException(st.Id, "Parcel");

            DAL.DataSource.packets.Add(st);
        }

        public void DelParcel(int id)
        {
            int count = DAL.DataSource.packets.RemoveAll(st => st.Id == id);

            if (count == 0)
                throw new MissingIdException(id, "Parcel");
        }

        public IEnumerable<Parcel> GetParcelByPerdicate(Predicate<Parcel> predicate)
        {
            return from st in DAL.DataSource.packets
                   where predicate(st)
                   select st;
        }
        public Parcel GetParcel(int s)
        {
            if (!CheckParcel(s))
                throw new MissingIdException(s, "Parcel");

            Parcel st = DAL.DataSource.packets.Find(st => st.Id == s);
            return st;

        }
        public IEnumerable<Parcel> GetALLParcel()
        {

            return DAL.DataSource.packets;
            
        }
    }
}
