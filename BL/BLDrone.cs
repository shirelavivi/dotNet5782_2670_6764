using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public partial class BL : IBL
        {

            public void UpdateDrone(int id, string model)
            {
                if (id < 0)
                    throw new ArgumentOutOfRangeException("id", "The drone number must be greater or equal to 0");
                try
                {
                    IDAL.DO.Drone drone = dl.GetDrone(id);
                    drone.Model = model;
                    dl.DelDrone(id);
                    dl.AddDrone(drone);
                    DroneToList droneToList = dronesBl.Find(d => d.Idnumber == id);
                    dronesBl.Remove(droneToList);
                    droneToList.Model = model;
                    dronesBl.Add(droneToList);
                }
                catch (IDAL.DO.MissingIdException ex)
                {
                    throw new MissingIdException(ex.ID, ex.EntityName);
                }
            }

            public void SendingDroneforCharging(int droneId)// שליחת רחפן לטעינה
            {

                double kilometer, battery = 0;
                int stationId = 0;
                IDAL.DO.Station minstation;
                try
                {
                    if (GetDroneToList(droneId).DroneStatuses != DroneStatuses.available)//אם הסטטוס שונה מפנוי יש חריגה
                        throw new UnsuitableDroneMode(GetDroneToList(droneId).DroneStatuses, "Drone");
                    else
                    {

                        minstation = MinFarToStation(GetDroneToList(droneId).ThisLocation);
                        //חישוב מרחק בין התחנה לרחפן
                        kilometer = DistanceTo(minstation.Lattitude, minstation.Longitude, GetDroneToList(droneId).ThisLocation.Lattitude, GetDroneToList(droneId).ThisLocation.Longitude);
                        battery = BatteryConsumption(kilometer, GetDroneToList(droneId).Weightcategories);//שמירת כמות הבטריה שמתבזבזת
                        if (battery < GetDroneToList(droneId).ButerryStatus)//צריך לבדוק אם הסוללה הנדרשת מספיקה לסוללה שיש לי ברחפן
                            dl.SendDroneTpCharge(stationId, droneId);
                        DroneToList drone = GetDroneToList(droneId);//מכאן נשנה את המצב של הרחפן והבטריה ברשימה של ה bl
                        drone.ButerryStatus -= battery;
                        drone.DroneStatuses = DroneStatuses.maintenance;
                        Location l = new Location();
                        l.Lattitude = dl.GetStation(stationId).Lattitude;
                        l.Longitude = dl.GetStation(stationId).Longitude;
                        drone.ThisLocation = l;
                        dronesBl.Remove(GetDroneToList(droneId));
                        dronesBl.Add(drone);

                    }
                }
                catch (IDAL.DO.MissingIdException ex)//  חריגה לא נכונה !!!!!!!!! לעשות חדשה
                {
                    throw new MissingIdException(ex.ID, ex.EntityName);
                }

            }


            public IEnumerable<BO.DroneToList> GetALLDroneToList()//הצגת רשימת רחפנים
            {

                //foreach (IDAL.DO.Drone item in dl.GetALLDrones())//מיוי הנתונים ב BL מתוך DAL
                //{
                //    Drone drone = GetDrone(item.id);
                //    DroneToList drone1 = new DroneToList();
                //    drone1.Idnumber = drone.IdDrone;
                //    drone1.Model = drone.Model;
                //    drone1.ThisLocation =new Location();
                //    drone1.ThisLocation.Lattitude = drone.ThisLocation.Lattitude;
                //    drone1.ThisLocation.Longitude = drone.ThisLocation.Longitude;
                //    drone1.Weightcategories = drone.Weightcategories;
                //    drone1.PackageNumberTransferred = drone.PackageInTransfer[drone.PackageInTransfer.Count()].IdPacket;    
                //    dronesBl.Add(drone1);
                //}
                return dronesBl;
            }
            public void AddDrone(BO.Drone drone, int stationId)// הוספת רחפן
            {
                if (drone.IdDrone < 0)
                    throw new ArgumentOutOfRangeException("drone.Id", "The drone number must be greater or equal to 0");
                if (stationId < 0)
                    throw new ArgumentOutOfRangeException("stationId", "The station number must be greater or equal to 0");
                try
                {

                    IDAL.DO.Drone droneDo = new IDAL.DO.Drone();
                    droneDo.id = drone.IdDrone;
                    droneDo.Model = drone.Model;
                    droneDo.MaxWeight = (IDAL.DO.Weightcategories)drone.Weightcategories;
                    dl.AddDrone(droneDo);

                    //BL:
                    Random rand = new Random();
                    DroneToList droneToListBL = new DroneToList();
                    droneToListBL.Idnumber = drone.IdDrone;
                    droneToListBL.Model = drone.Model;
                    droneToListBL.Weightcategories = (Weightcategories)drone.Weightcategories;
                    droneToListBL.ButerryStatus = rand.Next(20, 41);
                    droneToListBL.DroneStatuses = DroneStatuses.maintenance;
                    droneToListBL.PackageNumberTransferred = 0;
                    IDAL.DO.Station st = dl.GetStation(stationId);
                    droneToListBL.ThisLocation = new Location() { Lattitude = st.Lattitude, Longitude = st.Longitude };
                    dronesBl.Add(droneToListBL);
                }
                catch (IDAL.DO.MissingIdException ex)
                {
                    throw new MissingIdException(ex.ID, ex.EntityName);
                }
                catch (IDAL.DO.DuplicateIdException ex)
                {
                    throw new DuplicateIdException(ex.ID, ex.EntityName);
                }


            }
            public void ReleaseDroneFromChargeStation(int droneId, int timeInCharging)//שחרור רחפן מטעינה
            {
                if (droneId < 0)
                    throw new ArgumentOutOfRangeException("id", "The drone number must be greater or equal to 0");
                if (timeInCharging < 0)
                    throw new ArgumentOutOfRangeException("chargingTime", "The charging time must be greater or equal to 0");

                DroneToList droneToList = dronesBl.Find(d => d.Idnumber == droneId);
                if (droneToList == default(DroneToList))
                    throw new ArgumentException("Drone with the given ID number doesn't exist");

                if (droneToList.DroneStatuses != DroneStatuses.maintenance)
                    throw new InvalidOperationException("The drone is not charging");

                try
                {
                    dl.ReleaseDroneFromChargeStation(droneId);
                    dronesBl.Remove(droneToList);
                    droneToList.ButerryStatus += (timeInCharging * ChargingRate);//קצב טעינה
                    droneToList.DroneStatuses = DroneStatuses.available;
                    dronesBl.Add(droneToList);
                }
                catch (IDAL.DO.DuplicateIdException ex)
                {
                    throw new DuplicateIdException(ex.ID, ex.EntityName);
                }
            }
            public void PickUpPackage(int id)//איסוף חבילה על ידי רחפן
            {
                if (id < 0)
                    throw new ArgumentOutOfRangeException("id", "The drone number must be greater or equal to 0");

                DroneToList drone = dronesBl.Find(d => d.Idnumber == id);
                if (drone == default(DroneToList))
                    throw new ArgumentException("Drone with the given ID number doesn't exist");

                if (drone.DroneStatuses != DroneStatuses.transport)
                    throw new InvalidOperationException("The drone is not assigned to any package");

                var parcel = dl.GetParcel(drone.PackageNumberTransferred);
                if (parcel.Scheduled == default(DateTime) || parcel.PickedUp != default(DateTime))//ביקשתי את החריגה הזאת
                    throw new InvalidOperationException("The package is not ready to pick up");

                try
                {

                    var sender = dl.GetCustomer(parcel.SenderId);
                    double distance = DistanceTo(drone.ThisLocation.Lattitude, sender.Lattitude,
                        drone.ThisLocation.Longitude, sender.Longitude);//לקרוא לפונ חישוב מרחק
                    drone.ThisLocation = new Location//עדכון מיקום למיקום שולח
                    {
                        Lattitude = sender.Lattitude,
                        Longitude = sender.Longitude
                    };
                    drone.ButerryStatus -= BatteryConsumption(distance, drone.Weightcategories);
                    dl.collection(parcel.Id);

                }
                catch (IDAL.DO.DuplicateIdException ex)
                {
                    throw new DuplicateIdException(ex.ID, ex.EntityName);
                }
            }

            public Drone GetDrone(int droneId)
            {
                try
                {
                    ParcelInTransfer parcel = new ParcelInTransfer();
                    DroneToList droneToList = dronesBl.Find(i => i.Idnumber == droneId);
                    if (droneToList.Idnumber == 0)
                        throw new ErorrValueExceptin(droneId, "ERROR VALUE");
                    Drone drone = new()
                    {
                        IdDrone = droneToList.Idnumber,
                        ButerryStatus = droneToList.ButerryStatus,
                        DroneStatuses = droneToList.DroneStatuses,
                        ThisLocation = droneToList.ThisLocation,
                        Weightcategories = droneToList.Weightcategories,
                        Model = droneToList.Model
                    };

                    if (droneToList.DroneStatuses == DroneStatuses.transport)
                        drone.PackageInTransfer.Add(getPackageInDelivery(droneToList.PackageNumberTransferred));
                    return drone;

                }
                catch (IDAL.DO.MissingIdException ex)
                {
                    throw new MissingIdException(ex.ID, ex.EntityName);
                }


            }
            private ParcelInTransfer getPackageInDelivery(int packageId)
            {
                var pac = dl.GetParcel(packageId);

                ParcelInTransfer pacInDalivery = new ParcelInTransfer
                {
                    IdPacket = pac.DroneId,
                    PackageMode = pac.PickedUp != default(DateTime),
                    Weightcategories = (Weightcategories)pac.Weight,
                    Priorities = (Priorities)pac.Priority
                };

                var sender = dl.GetCustomer(pac.SenderId);
                var target = dl.GetCustomer(pac.TargetId);
                pacInDalivery.TransportDistance = DistanceTo(sender.Lattitude, target.Lattitude, sender.Longitude, target.Longitude);

                pacInDalivery.CustomerInPackageGeting = new CustomerAtParcels
                {
                    Id = target.Id,
                    Name = target.Name
                };

                pacInDalivery.CustomerInPackageSender = new CustomerAtParcels
                {
                    Id = sender.Id,
                    Name = sender.Name
                };

                pacInDalivery.DeliveryLocation = new Location
                {
                    Lattitude = sender.Lattitude,
                    Longitude = sender.Longitude
                };

                pacInDalivery.CollectionLocation = new Location
                {
                    Lattitude = target.Lattitude,
                    Longitude = target.Longitude
                };
                return pacInDalivery;
            }
            public IEnumerable<DroneToList> GetALLDroneToList(Predicate<DroneToList> predicate=null)
            {
                return dronesBl.FindAll(predicate); 
                //return from dron in dronesBl
                //       where predicate(dron)
                //       select dron;

            }
        }
    }

}












