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
                try
                {
                    IDAL.DO.Drone drone = dl.GetDrone(id);
                    drone.Model = model;
                    dl.DelDrone(id);
                    dl.AddDrone(drone);
                    DroneToList droneToList = drones_bl.Find(d => d.Idnumber == id);//צריך גם לעדכן כאן?
                    drones_bl.Remove(droneToList);
                    droneToList.Model = model;
                    drones_bl.Add(droneToList);
                }
                catch (IDAL.DO.MissingIdException ex)
                {
                    throw new MissingIdException(ex.ID, ex.EntityName);
                }
            }
            public void AddDrone(BO.Drone drone, int stationId)
            {
                if (drone.IdDrone < 0)
                    throw new ArgumentOutOfRangeException("drone.Id", "The drone number must be greater or equal to 0");
                if (stationId < 0)
                    throw new ArgumentOutOfRangeException("stationId", "The station number must be greater or equal to 0");
                try
                {
                   
                    IDAL.DO.Drone droneDo = new IDAL.DO.Drone();
                    droneDo.Id = drone.IdDrone;
                    droneDo.Model = drone.Model;
                    droneDo.MaxWeight = (IDAL.DO.Weightcategories)drone.Weightcategories;
                    dl.AddDrone(droneDo);

                    //BL:
                    Random rand = new Random();
                    DroneToList droneToListBL = new DroneToList();
                    droneToListBL.Idnumber= drone.IdDrone;
                    droneToListBL.Model = drone.Model;
                    droneToListBL.Weightcategories= (Weightcategories)drone.Weightcategories;
                    droneToListBL.ButerryStatus = rand.Next(20, 41);
                    droneToListBL.DroneStatuses = DroneStatuses.maintenance;
                    droneToListBL.PackageNumberTransferred = 0;
                    IDAL.DO.Station st=dl.GetStation(stationId);
                    droneToListBL.ThisLocation = new Location() { Lattitude = st.Lattitude, Longitude = st.Longitude };
                    drones_bl.Add(droneToListBL);
                }
                catch (IDAL.DO.DuplicateIdException ex)
                {
                    throw new DuplicateIdException(ex.ID, ex.EntityName);
                }


            }
            public void ReleaseDroneFromChargeStation(int droneId, int timeInCharging)
            {
                if (droneId < 0)
                    throw new ArgumentOutOfRangeException("id", "The drone number must be greater or equal to 0");
                if (timeInCharging < 0)
                    throw new ArgumentOutOfRangeException("chargingTime", "The charging time must be greater or equal to 0");

                DroneToList droneToList = drones_bl.Find(d => d.Idnumber == droneId);
                if (droneToList == default(DroneToList))
                    throw new ArgumentException("Drone with the given ID number doesn't exist");

                if (droneToList.DroneStatuses != DroneStatuses.maintenance)
                    throw new InvalidOperationException("The drone is not charging");

                try
                {
                    dl.ReleaseDroneFromChargeStation(droneId);
                    drones_bl.Remove(droneToList);
                    droneToList.ButerryStatus +=(timeInCharging * ChargingRate);//קצב טעינה
                    droneToList.DroneStatuses = DroneStatuses.available;
                    drones_bl.Add(droneToList);
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            public void PickUpPackage(int id)
            {
                if (id < 0)
                    throw new ArgumentOutOfRangeException("id", "The drone number must be greater or equal to 0");

                DroneToList drone = drones_bl.Find(d => d.Idnumber == id);
                if (drone == default(DroneToList))
                    throw new ArgumentException("Drone with the given ID number doesn't exist");

                if (drone.DroneStatuses!= DroneStatuses.)
                    throw new InvalidOperationException("The drone is not assigned to any package");

                var parcel = dl.GetParcel(drone.PackageNumberTransferred);
                if (parcel.Scheduled == default(DateTime) || parcel.PickedUp != default(DateTime))//ביקשתי את החריגה הזאת
                    throw new InvalidOperationException("The package is not ready to pick up");

                try
                {
                    drones_bl.Remove(drone);
                    var sender = dl.GetCustomer(parcel.SenderId);
                    double distance = dl.DistanceTo(drone.ThisLocation.Lattitude, sender.Lattitude,
                        drone.ThisLocation.Longitude, sender.Longitude);//לקרוא לפונ חישוב מרחק
                    drone.ThisLocation = new Location//עדכון מיקום למיקום שולח
                    {
                        Lattitude = sender.Lattitude,
                        Longitude = sender.Longitude
                    };
                    drone.ButerryStatus -= BatteryConsumption(distance,drone.Weightcategories);
                    dl.collection(parcel.Id);
                    drones_bl.Add(drone);

                }
                catch (Exception e)
                {
                    throw e;
                }
            }

            public Drone GetDrone(int droneId)
            {
                try
                {
                    DroneToList droneToList = BL.drones_bl.Find(i => i.Idnumber == droneId);
                    if (droneToList.Idnumber == 0)
                        throw new NotImplementedException();
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
                        drone.PackageInTransfer = ;


                    return drone;
                }
                catch (Exception e)
                {
                    throw e;
                }

            }
            

        }
    }
    
}

