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
          public void AddrParcel(BO.Parcel parcel)
            {

                try
                {
                    IDAL.DO.Parcel parcel_do = new IDAL.DO.Parcel();
                    parcel_do.Id = dl.runNumber();
                    parcel_do.SenderId = parcel.Sender.Id;
                    parcel_do.TargetId = parcel.Target.Id;
                    parcel_do.Weight = (IDAL.DO.Weightcategories)parcel.Weight;
                    parcel_do.Priority = (IDAL.DO.Priorities)parcel.Priority;
                    parcel_do.Requested = DateTime.MinValue;
                    parcel_do.Scheduled = DateTime.MinValue;
                    parcel_do.PickedUp = DateTime.Now;
                    parcel_do.Delivered = DateTime.MinValue;
                    dl.AddParcel(parcel_do);
                }
                catch (IDAL.DO.DuplicateIdException ex)
                {
                    throw new DuplicateIdException(ex.ID, ex.EntityName);
                }
            }
            public void ConnectParcelToDrone(int droneid)
            {
                if (GetDroneToList(droneid).DroneStatuses != (DroneStatuses)0)//אם הסטטוס שונה מפנוי יש חריגה
                    throw new UnsuitableDroneMode(GetDroneToList(droneid).DroneStatuses, "Drone");

            }
            //public bool IfItPossible(BO.DroneToList drone,BO.Parcel parcel)
            //{

            //}
            public IEnumerable<BO.ParcelToList> GetALLParcelToList()
            {
                ParcelToList parcel = new ParcelToList();
                foreach (IDAL.DO.Parcel item in dl.GetALLParcel())//מיוי הנתונים ב BL מתוך DAL
                {
                    parcel.Id = item.Id;
                    parcel.Priority = (Priorities)(item.Priority);
                    parcel.SenderName = dl.GetCustomer(item.SenderId).Name;
                    parcel.TargetName= dl.GetCustomer(item.TargetId).Name;
                    parcel.Weight = (Weightcategories)(item.Weight);
                    parcelBl.Add(parcel);

                }
                return parcelBl;
            }

        }

    }
}
