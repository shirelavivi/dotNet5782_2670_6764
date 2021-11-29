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
        public Parcel GetParcel(int id)
        {
            if (!CheckParcel(id))
                throw new MissingIdException(id, "Parcel");

            Parcel st = IDAL.DataSource.packets.Find(st => st.Id == id);
            return st;
        }
        public void AdditionParcel(Parcel c)
        {

            if (CheckParcel(c.Id))

                throw new IDAL.DO.DuplicateIdException(c.Id, "Parcel");


            IDAL.DataSource.packets.Add(c);

        }

        public bool CheckParcel(int id)
        {
            return IDAL.DataSource.packets.Any(st => st.Id == id);
        }

        public IEnumerable<Parcel> GetALLParcel()
        {
            return from st in IDAL.DataSource.packets select st;
            //return DataSource.Customers;
        }



        public void UpdParcel(Parcel st)
        {
            int count = IDAL.DataSource.packets.RemoveAll(st => st.Id == st.Id);

            if (count == 0)
                throw new MissingIdException(st.Id, "Parcel");

            IDAL.DataSource.packets.Add(st);
        }

        public void DelParcel(int id)
        {
            int count = IDAL.DataSource.packets.RemoveAll(st => st.Id == id);

            if (count == 0)
                throw new MissingIdException(id, "Parcel");
        }

        public IEnumerable<Parcel> GetParcelByPerdicate(Predicate<Parcel> predicate)
        {
            return from st in IDAL.DataSource.packets
                   where predicate(st)
                   select st;
        }
        public Parcel ShowParcels(int s)
        {
            List<Parcel> run = IDAL.DataSource.packets;
            Parcel temp = new Parcel();
            for (int i = 0; i < run.Count; i++)
            {
                if (run[i].Id == s)
                {
                    temp = run[i];
                }
            }
            return temp;

        }
        //public IEnumerable<Parcel> ShowParcelList()//כפילות בפונציה יש אותה פעמיים בשמות שונים 
        //{

        //    List<Parcel> temp = new List<Parcel>();
        //    foreach (Parcel item in IDAL.DataSource.packets)
        //    {
        //        temp.Add(item);
        //    }

        //    return temp;
        //}
    }
}
