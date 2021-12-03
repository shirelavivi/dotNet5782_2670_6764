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
            public void ConnectParcelToDrone(int droneid)//שייוך חבילה לרחפן
            {
                double minFar = 10000000;//המרחק המינימלי של החבילה מהרחפן
                int keepParclId = 0;
                try
                {
                    if (GetDroneToList(droneid).DroneStatuses != (DroneStatuses)0)//אם הסטטוס שונה מפנוי יש חריגה
                        throw new UnsuitableDroneMode(GetDroneToList(droneid).DroneStatuses, "Drone");

                    else
                    {
                        foreach (IDAL.DO.Parcel item in dl.GetALLParcel())
                        {
                            if (item.Priority == IDAL.DO.Priorities.emergency)//אם החבילה היא חירום
                                if (IfDronCanTakeParcel(item, droneid))//אם הרחפן יכול לשאת את המשקל
                                {
                                    double distance = DistanceTo(dl.GetCustomer(item.SenderId).Lattitude, dl.GetCustomer(item.SenderId).Longitude, GetDroneToList(droneid).ThisLocation.Lattitude, GetDroneToList(droneid).ThisLocation.Longitude);
                                    if (distance < minFar)
                                    {
                                        minFar = distance;
                                        keepParclId = item.Id;
                                    }
                                }
                        }
                        Location senderId = new Location();
                        Location targetId = new Location();
                        Location statin = new Location();
                        senderId.Lattitude = dl.GetCustomer(dl.GetParcel(keepParclId).SenderId).Lattitude;// המיקום של החבילה שאנחנו רוצים לשייך לה רחפן
                        senderId.Longitude = dl.GetCustomer(dl.GetParcel(keepParclId).SenderId).Longitude;
                        if (IfDronCanGoTo(senderId, GetDroneToList(droneid).ThisLocation, droneid))//אם יש מספיק סוללה כדי להגיע לקחת את החבילה מהשולח
                        {
                            targetId.Lattitude = dl.GetCustomer(dl.GetParcel(keepParclId).TargetId).Lattitude;// המיקום של הלקוח שאמור לקבל את החבילה
                            targetId.Longitude = dl.GetCustomer(dl.GetParcel(keepParclId).TargetId).Longitude;
                            {
                                if (IfDronCanGoTo(targetId, senderId, GetDroneToList(droneid).Weightcategories, droneid))//אם הרחפן לרחפן יש מספיק בטריה בין בשולח למקבל
                                {
                                    statin.Lattitude = dl.GetStation(MinFarToStation(targetId).idnumber).Lattitude;// המיקום של התחנה טעינה הקרובה 
                                    statin.Longitude = dl.GetStation(MinFarToStation(targetId).idnumber).Longitude;
                                    if (IfDronCanGoTo(statin, targetId, droneid))
                                        dl.ConnectParcelToDron(keepParclId, droneid);
                                }

                            }
                        }
                    }
                    if (minFar == 0 || keepParclId == 0)
                        throw new Exception();// צריך לבדוק איזה חריגה מתאימה אם לא נמצא חבילה בשביל משלוח


                }
                catch (Exception ex)//לבדוק מה לתפוס כאן
                {
                    throw ex; 
                }
            }
            
                public Parcel GetParcel(int id)
                {
                    try
                    {
                        var parcel = dl.GetParcel(id);
                        Parcel myParcel = new Parcel
                        {
                            Id = parcel.Id,
                            Sender = new CustomerAtParcels
                            {
                                Id = parcel.SenderId,
                                Name = dl.GetCustomer(parcel.SenderId).Name
                            },
                            Target = new CustomerAtParcels
                            {
                                Id = parcel.TargetId,
                                Name = dl.GetCustomer(parcel.TargetId).Name
                            },
                            Weight = (Weightcategories)parcel.Weight,
                            Priority = (Priorities)parcel.Priority,
                            Requested = parcel.Requested,
                            droneAtParcel = (parcel.DroneId == 0 ? null : new DroneInParcel
                            {
                                IdNumber = parcel.DroneId
                            }),
                            Scheduled = parcel.Scheduled,
                            PickedUp = parcel.PickedUp,
                            Delivered = parcel.Delivered
                        };

                        if (parcel.Id != 0)
                        {
                            var drone = dronesBl.Find(d => d.Idnumber == parcel.DroneId);
                            if (drone == default(DroneToList))
                                throw new ArgumentException("The drone the package is assign to doesn't exist");
                            myParcel.droneAtParcel.ButerryStatus = drone.ButerryStatus;
                            myParcel.droneAtParcel.ThisLocation = drone.ThisLocation;
                        }

                        return myParcel;
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
                //public bool IfItPossible(BO.DroneToList drone,BO.Parcel parcel)
                //{

            

            public bool IfDronCanGoTo(Location a, Location b, Weightcategories weightcategories, int dronid)//פונקצית עזר שבודקת אם לרחפן יש מספיק סוללה בין ללכת בין הנקודה A ל B
            {
                double x = DistanceTo(a.Lattitude, a.Longitude, b.Lattitude, a.Longitude);
                if (BatteryConsumption(x, weightcategories) <= GetDroneToList(dronid).ButerryStatus)
                    return true;
                return false;
            }
            public bool IfDronCanGoTo(Location a, Location b, int dronid)// כשהרחפן ריק פונקצית עזר שבודקת אם לרחפן יש מספיק סוללה בין ללכת בין הנקודה A ל B
            {
                double x = DistanceTo(a.Lattitude, a.Longitude, b.Lattitude, a.Longitude);
                if (BatteryConsumption(x) <= GetDroneToList(dronid).ButerryStatus)
                    return true;
                return false;
            }
            public bool IfDronCanTakeParcel(IDAL.DO.Parcel parcel, int droneid)//פונקצית עזר שבודקת אם הרחפן יכול לשאת את החבילה
            {
                if ((int)parcel.Weight <= (int)(GetDroneToList(droneid).Weightcategories))
                    return true;
                return false;
            }
            public IEnumerable<BO.ParcelToList> GetALLParcelToList()
            {
                ParcelToList parcel = new ParcelToList();
                foreach (IDAL.DO.Parcel item in dl.GetALLParcel())//מיוי הנתונים ב BL מתוך DAL
                {
                    parcel.Id = item.Id;
                    parcel.Priority = (Priorities)(item.Priority);
                    parcel.SenderName = dl.GetCustomer(item.SenderId).Name;
                    parcel.TargetName = dl.GetCustomer(item.TargetId).Name;
                    parcel.Weight = (Weightcategories)(item.Weight);
                    parcelBl.Add(parcel);

                }
                return parcelBl;
            }
            public IEnumerable<ParcelToList> GetALLParcelsNotConnectToDrone()//רשימת חבילות שעדיין לא שוייכו לרחפן
            {
                List<ParcelToList> st = new List<ParcelToList>();
                foreach (ParcelToList item in GetALLParcelToList())
                {
                    if (dl.GetParcel(item.Id).DroneId == default(int))
                    {
                        st.Add(item);
                    }
                }
                //if (st == null)
                //throw לראות איזה חריגה לעשות כאן
                return st;
            }
    }   }
}        

    

