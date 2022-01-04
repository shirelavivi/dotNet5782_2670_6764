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
                    parcel_do.Requested = DateTime.Now;
                    parcel_do.Scheduled = null;
                    parcel_do.PickedUp = null;
                    parcel_do.Delivered = null;
                    dl.AddParcel(parcel_do);
                }
                catch (IDAL.DO.DuplicateIdException ex)
                {
                    throw new DuplicateIdException(ex.ID, ex.EntityName);
                }
            }
            public void ConnectParcelToDrone(int droneid)//שייוך חבילה לרחפן
            {
               int i = 0;
                double minFar = 10000000;//המרחק המינימלי של החבילה מהרחפן
                int keepParclId = 0;
                try
                {
                    if (GetDroneToList(droneid).DroneStatuses != (DroneStatuses)0)//אם הסטטוס שונה מפנוי יש חריגה
                        throw new UnsuitableDroneMode(GetDroneToList(droneid).DroneStatuses, "Drone");

                    else
                    {
                        while (i != 3)
                        {
                            foreach (IDAL.DO.Parcel item in dl.GetALLParcel())
                            {
                                if (item.Scheduled == null)
                                {
                                    if (item.Priority == (IDAL.DO.Priorities)i)//אם החבילה היא חירום
                                    {
                                        if (IfDronCanTakeParcel(item, droneid))//אם הרחפן יכול לשאת את המשקל
                                        {
                                            Location senderId = new Location();// האם הוא יכולה לעשות את כל הדרך עם הבטריה שיש לה
                                            Location targetId = new Location();
                                            Location statin = new Location();
                                            double battary = GetDroneToList(droneid).ButerryStatus;// הבטריה שיש לרחפן
                                            senderId.Lattitude = dl.GetCustomer(dl.GetParcel(item.Id).SenderId).Lattitude;// המיקום של החבילה שאנחנו רוצים לשייך לה רחפן
                                            senderId.Longitude = dl.GetCustomer(dl.GetParcel(item.Id).SenderId).Longitude;
                                            battary -= BatteryConsumption(DistanceTo(senderId.Lattitude, senderId.Longitude, GetDroneToList(droneid).ThisLocation.Lattitude, GetDroneToList(droneid).ThisLocation.Longitude));
                                            if (battary > 0)//אם יש מספיק סוללה כדי להגיע לקחת את החבילה מהשולח
                                            {
                                                targetId.Lattitude = dl.GetCustomer(dl.GetParcel(item.Id).TargetId).Lattitude;// המיקום של הלקוח שאמור לקבל את החבילה
                                                targetId.Longitude = dl.GetCustomer(dl.GetParcel(item.Id).TargetId).Longitude;
                                                battary -= BatteryConsumption(DistanceTo(targetId.Lattitude, targetId.Longitude, senderId.Lattitude, senderId.Longitude), GetDroneToList(droneid).Weightcategories);
                                                if (battary > 0)//אם הרחפן לרחפן יש מספיק בטריה בין בשולח למקבל
                                                {
                                                    statin.Lattitude = dl.GetStation(MinFarToStation(targetId).Id).Lattitude;// המיקום של התחנה טעינה הקרובה 
                                                    statin.Longitude = dl.GetStation(MinFarToStation(targetId).Id).Longitude;
                                                    battary -= BatteryConsumption(DistanceTo(targetId.Lattitude, targetId.Longitude, statin.Lattitude, statin.Longitude));
                                                    if (battary >= 0)//האם אתה יכול להגיע לעמדת טעינה הקרובה 
                                                    {
                                                        double distance = DistanceTo(dl.GetCustomer(item.SenderId).Lattitude, dl.GetCustomer(item.SenderId).Longitude, GetDroneToList(droneid).ThisLocation.Lattitude, GetDroneToList(droneid).ThisLocation.Longitude);
                                                        if (distance < minFar)
                                                        {
                                                            minFar = distance;
                                                            keepParclId = item.Id;

                                                        }
                                                    }

                                                }


                                            }

                                        }
                                    }

                                }
                            }
                            i++;
                        }
                        if (keepParclId != 0)
                        {
                            dl.ConnectParcelToDron(keepParclId, droneid);// רק לאחר שנמצאה החבילה המתאימה נבצעה עליה את הפעולה של שכבת ה dal
                            DroneToList d = dronesBl.Find(x => x.Idnumber == droneid);
                            d.PackageNumberTransferred = keepParclId;
                            d.DroneStatuses = DroneStatuses.transport;
                            dronesBl.Remove(GetDroneToList(droneid));
                            dronesBl.Add(d);
                        }
                    }

                }
                catch (IDAL.DO.DuplicateIdException ex)
                {
                    throw new DuplicateIdException(ex.ID, ex.EntityName);
                }
                catch (IDAL.DO.MissingIdException ex)
                {
                    throw new MissingIdException(ex.ID, ex.EntityName);
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
                        if (drone == null)
                            throw new MissingIdException(drone.Idnumber, "drone");
                        myParcel.droneAtParcel.ButerryStatus = drone.ButerryStatus;
                        myParcel.droneAtParcel.ThisLocation = drone.ThisLocation;
                    }

                    return myParcel;
                }
                catch (IDAL.DO.MissingIdException ex)
                {
                    throw new MissingIdException(ex.ID, ex.EntityName);
                }
            }



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

                List<ParcelToList> parcelToLists = new List<ParcelToList>();
                foreach (IDAL.DO.Parcel item in dl.GetALLParcel())//מיוי הנתונים ב BL מתוך DAL
                {
                    ParcelToList parcel = new ParcelToList();
                    parcel.Id = item.Id;
                    parcel.Priority = (Priorities)(item.Priority);
                    parcel.SenderName = dl.GetCustomer(item.SenderId).Name;
                    parcel.TargetName = dl.GetCustomer(item.TargetId).Name;
                    parcel.Weight = (Weightcategories)(item.Weight);
                    parcelToLists.Add(parcel);

                }
                return parcelToLists;
            }
            public IEnumerable<ParcelToList> GetALLParcelsNotConnectToDrone()//רשימת חבילות שעדיין לא שוייכו לרחפן
            {
                try
                {
                    List<ParcelToList> st = new List<ParcelToList>();
                    foreach (ParcelToList item in GetALLParcelToList())
                    {
                        if (dl.GetParcel(item.Id).DroneId == default(int))
                        {
                            st.Add(item);
                        }
                    }
                    return st;
                }
                catch (IDAL.DO.MissingIdException ex)
                {
                    throw new MissingIdException(ex.ID, ex.EntityName);
                }

            }
           public void  SupplyParcelByDrone(int droneID)// אספקת חבילה על ידי רחפן
            {

              
            }
            public IEnumerable<ParcelToList> GetALLParce(Predicate<ParcelToList> predicate = null)
            {
                return  GetALLParcelToList().ToList().FindAll(predicate);
            }
        }
    }
}        

    

