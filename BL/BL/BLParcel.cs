using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using BlApi;
using DalApi;
using DO;

namespace BL
{
    sealed partial class BL:IBL
    {
        public void AddrParcel(BO.Parcel parcel)
        {

            try
            {
                DO.Parcel parcel_do = new DO.Parcel();
                parcel_do.SenderId = parcel.Sender.Id;
                parcel_do.TargetId = parcel.Target.Id;
                parcel_do.Weight = (DO.Weightcategories)parcel.Weight;
                parcel_do.Priority = (DO.Priorities)parcel.Priority;
                parcel_do.Requested = DateTime.Now;
                parcel_do.Scheduled = null;
                parcel_do.PickedUp = null;
                parcel_do.Delivered = null;
                dal.AddParcel(parcel_do);
            }
            catch (DO.DuplicateIdException ex)
            {
                throw new BO.DuplicateIdException(ex.ID, ex.EntityName);
            }
        }
        public void RemoveParcel(int id)
        {
            try 
            { 
                dal.DelParcel(id);  
            }
            catch (DO.MissingIdException ex)
            {
                throw new BO.MissingIdException(ex.ID, ex.EntityName);
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
                        foreach (DO.Parcel item in dal.GetALLParcel())
                        {
                            if (item.Scheduled == null)
                            {
                                if (item.Priority == (DO.Priorities)i)//אם החבילה היא חירום
                                {
                                    if (IfDronCanTakeParcel(item, droneid))//אם הרחפן יכול לשאת את המשקל
                                    {
                                        Location senderId = new Location();// האם הוא יכולה לעשות את כל הדרך עם הבטריה שיש לה
                                        Location targetId = new Location();
                                        Location statin = new Location();
                                        double battary = GetDroneToList(droneid).ButerryStatus;// הבטריה שיש לרחפן
                                        senderId.Lattitude = dal.GetCustomer(dal.GetParcel(item.Id).SenderId).Lattitude;// המיקום של החבילה שאנחנו רוצים לשייך לה רחפן
                                        senderId.Longitude = dal.GetCustomer(dal.GetParcel(item.Id).SenderId).Longitude;
                                        battary -= BatteryConsumption(DistanceTo(senderId.Lattitude, senderId.Longitude, GetDroneToList(droneid).ThisLocation.Lattitude, GetDroneToList(droneid).ThisLocation.Longitude));
                                        if (battary > 0)//אם יש מספיק סוללה כדי להגיע לקחת את החבילה מהשולח
                                        {
                                            targetId.Lattitude = dal.GetCustomer(dal.GetParcel(item.Id).TargetId).Lattitude;// המיקום של הלקוח שאמור לקבל את החבילה
                                            targetId.Longitude = dal.GetCustomer(dal.GetParcel(item.Id).TargetId).Longitude;
                                            battary -= BatteryConsumption(DistanceTo(targetId.Lattitude, targetId.Longitude, senderId.Lattitude, senderId.Longitude), GetDroneToList(droneid).Weightcategories);
                                            if (battary > 0)//אם הרחפן לרחפן יש מספיק בטריה בין בשולח למקבל
                                            {
                                                statin.Lattitude = dal.GetStation(MinFarToStation(targetId).Id).Lattitude;// המיקום של התחנה טעינה הקרובה 
                                                statin.Longitude = dal.GetStation(MinFarToStation(targetId).Id).Longitude;
                                                battary -= BatteryConsumption(DistanceTo(targetId.Lattitude, targetId.Longitude, statin.Lattitude, statin.Longitude));
                                                if (battary >= 0)//האם אתה יכול להגיע לעמדת טעינה הקרובה 
                                                {
                                                    double distance = DistanceTo(dal.GetCustomer(item.SenderId).Lattitude, dal.GetCustomer(item.SenderId).Longitude, GetDroneToList(droneid).ThisLocation.Lattitude, GetDroneToList(droneid).ThisLocation.Longitude);
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
                        dal.ConnectParcelToDron(keepParclId, droneid);// רק לאחר שנמצאה החבילה המתאימה נבצעה עליה את הפעולה של שכבת ה dal
                        DroneToList d = dronesBl.Find(x => x.Idnumber == droneid);
                        d.PackageNumberTransferred = keepParclId;
                        d.DroneStatuses = DroneStatuses.transport;
                        dronesBl.Remove(GetDroneToList(droneid));
                        dronesBl.Add(d);
                    }
                    else
                    {
                        throw new BO.NoParcelsToDroneException("No parcel to this drone");
                    }
                }

            }
            catch (DO.DuplicateIdException ex)
            {
                throw new BO.DuplicateIdException(ex.ID, ex.EntityName);
            }
            catch (DO.MissingIdException ex)
            {
                throw new BO.MissingIdException(ex.ID, ex.EntityName);
            }
        }

        public BO.Parcel GetParcel(int id)
        {
            try
            {
                var parcel = dal.GetParcel(id);
                BO.Parcel myParcel = new BO.Parcel
                {
                    Id = parcel.Id,
                    Sender = new CustomerAtParcels
                    {
                        Id = parcel.SenderId,
                        Name = dal.GetCustomer(parcel.SenderId).Name
                    },
                    Target = new CustomerAtParcels
                    {
                        Id = parcel.TargetId,
                        Name = dal.GetCustomer(parcel.TargetId).Name
                    },

                    Weight = (BO.Weightcategories)parcel.Weight,
                    Priority = (BO.Priorities)parcel.Priority,
                    Requested = parcel.Requested,
                    droneAtParcel = (parcel.DroneId == 0 ? null : new DroneInParcel
                    {
                        
                        IdNumber = parcel.DroneId
                    }),
                    Scheduled = parcel.Scheduled,
                    PickedUp = parcel.PickedUp,
                    Delivered = parcel.Delivered
                };
                if (parcel.DroneId == 0)
                {
                    myParcel.droneAtParcel = null;
                    
                }
                else
                {
                    if (parcel.Id != 0)
                    {
                        var drone = dronesBl.Find(d => d.Idnumber == parcel.DroneId);
                        if (drone == null)
                            throw new BO.MissingIdException(drone.Idnumber, "drone");
                        if (drone != null)
                        {
                            myParcel.droneAtParcel.ButerryStatus = drone.ButerryStatus;
                            myParcel.droneAtParcel.ThisLocation = drone.ThisLocation;
                        }
                    }
                }
               
                return myParcel;
            }
            catch (BO.MissingIdException ex)
            {
                throw new BO.MissingIdException(ex.ID, ex.EntityName);
            }
        }



        public bool IfDronCanGoTo(Location a, Location b, BO.Weightcategories weightcategories, int dronid)//פונקצית עזר שבודקת אם לרחפן יש מספיק סוללה בין ללכת בין הנקודה A ל B
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
        public bool IfDronCanTakeParcel(DO.Parcel parcel, int droneid)//פונקצית עזר שבודקת אם הרחפן יכול לשאת את החבילה
        {
            if ((int)parcel.Weight <= (int)(GetDroneToList(droneid).Weightcategories))
                return true;
            return false;
        }
        public IEnumerable<BO.ParcelToList> GetALLParcelToList()
        {

            List<ParcelToList> parcelToLists = new List<ParcelToList>();
            foreach (DO.Parcel item in dal.GetALLParcel())//מיוי הנתונים ב BL מתוך DAL
            {
                ParcelToList parcel = new ParcelToList();
                parcel.Id = item.Id;
                parcel.Priority = (BO.Priorities)(item.Priority);
                parcel.SenderName = dal.GetCustomer(item.SenderId).Name;
                parcel.TargetName = dal.GetCustomer(item.TargetId).Name;
                parcel.Weight = (BO.Weightcategories)(item.Weight);
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
                    if (dal.GetParcel(item.Id).DroneId == default(int))
                    {
                        st.Add(item);
                    }
                }
                return st;
            }
            catch (DO.MissingIdException ex)
            {
                throw new BO.MissingIdException(ex.ID, ex.EntityName);
            }

        }
        public void SupplyParcelByDrone(int droneID)// אספקת חבילה על ידי רחפן
        {
            try
            {
                DroneToList drone = dronesBl.FirstOrDefault(x => x.Idnumber == droneID);
                DO.Parcel parcel = dal.GetALLParcel().ToList().Find(x => x.DroneId == droneID);
                //if (parcel.Delivered != null || parcel.PickedUp != null)
                //    throw new BO.ErorrValueExceptin(droneID, "Parcel Exeption", "Parcel can't be delivered. Time Problem");
                DO.Customer customer = dal.GetALLCustomers().ToList().Find(x => x.Id == parcel.TargetId);
                double distance = DistanceTo(drone.ThisLocation.Lattitude, drone.ThisLocation.Longitude, customer.Lattitude, customer.Longitude);
                double buttery = BatteryConsumption(distance, (BO.Weightcategories)parcel.Weight);
                if (buttery > drone.ButerryStatus)
                    throw new ErorrValueExceptin(droneID, "Parcel Exeption", "Not enough battery left");
                drone.ThisLocation.Lattitude = customer.Lattitude;
                drone.ThisLocation.Longitude = customer.Longitude;
                drone.DroneStatuses = DroneStatuses.available;
                drone.ButerryStatus -= buttery;
                dal.PackageDalvery(parcel.Id);
                drone.PackageNumberTransferred = 0;
                dronesBl.Remove(GetDroneToList(droneID));
                dronesBl.Add(drone);
            }
            catch (DO.MissingIdException ex)
            {
                throw new BO.MissingIdException(ex.ID, ex.EntityName);
            }
            catch (DO.DuplicateIdException ex)
            {
                throw new BO.DuplicateIdException(ex.ID, ex.EntityName);
            }
        }
        public IEnumerable<ParcelToList> GetALLParce(Predicate<ParcelToList> predicate = null)
        {
            return GetALLParcelToList().ToList().FindAll(predicate);
        }
       
    }
}

