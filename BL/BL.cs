using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL;

namespace IBL
{
    namespace BO
    {
       
       
        public partial class BL : IBL
        {
            internal static List<DroneToList> drones_bl = new List<DroneToList>();
            DalObject.DalObject dl = new DalObject.DalObject();
           
            // void AddParcel(BO.Parcel parcel)
            //{
            //    try
            //    {
                   
            //        parcels.add(parcel);
            //        IDAL.DO.Parcel parcel_do = new IDAL.DO.Parcel();
            //        parcel_do.Id = dl.runNumber();
            //        parcel_do.SenderId = parcel.Sender.id;
            //        parcel_do.TargetId = parcel.Target.id;
            //        parcel_do.Weight = (IDAL.DO.Weightcategories)parcel.Weight;
            //        parcel_do.Priority = (IDAL.DO.Priorities)parcel.Priority;
            //        parcel_do.Requested = DateTime.MinValue;
            //        parcel_do.Scheduled = DateTime.MinValue;
            //        parcel_do.PickedUp = DateTime.Now;
            //        parcel_do.Delivered = DateTime.MinValue;
            //        dl.add(parcel_do);
            //    }
            //    catch
            //    {

            //    }

            //}
            

        }
    }
}