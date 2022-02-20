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
       
        public void AddParcel(Parcel p)
        {
            p.Id = DataSource.config.runCounterPackets();
            p.Requested = DateTime.Now;
            if (CheckParcel(p.Id))

                throw new DO.DuplicateIdException(p.Id, "Parcel");


            Dal.DataSource.packets.Add(p);

        }

        public bool CheckParcel(int id)
        {
            return Dal.DataSource.packets.Any(st => st.Id == id);
        }


        public void UpdParcel(Parcel st) 
        {
            int count = Dal.DataSource.packets.RemoveAll(st => st.Id == st.Id);

            if (count == 0)
                throw new MissingIdException(st.Id, "Parcel");

            Dal.DataSource.packets.Add(st);
        }

        public void DelParcel(int id)
        {
            int count = Dal.DataSource.packets.RemoveAll(st => st.Id == id);

            if (count == 0)
                throw new MissingIdException(id, "Parcel");
        }

        public IEnumerable<Parcel> GetParcelByPerdicate(Predicate<Parcel> predicate)
        {
            return from st in Dal.DataSource.packets
                   where predicate(st)
                   select st;
        }
        public Parcel GetParcel(int s)
        {
            if (!CheckParcel(s))
                throw new MissingIdException(s, "Parcel");

            Parcel st = Dal.DataSource.packets.Find(st => st.Id == s);
            return st;

        }
        public IEnumerable<Parcel> GetALLParcel()
        {

            return Dal.DataSource.packets;
            
        }
        
    }
}

