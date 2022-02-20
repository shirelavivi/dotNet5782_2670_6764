
using System;
using System.Collections.Generic;
using BO;
using System.Threading;
using System.Linq;
using BlApi;



namespace BL
{
    class Simulator 
    {
        private const int DELAY = 500; // milliseconds
        private const double SPEED = 40; // km per hour
        private Drone drone;
        private Location location;
        private double timeDrive;
        public Simulator(BlApi.IBL bl, int droneId, Action updateView, Func<bool> stopSimulator)
        {
            while (!stopSimulator())
            {
                lock (bl)
                {
                    drone = bl.GetDrone(droneId);
                }

                switch (drone.DroneStatuses)
                {
                    case DroneStatuses.available:
                        {
                            try
                            {
                                lock (bl)
                                {
                                    bl.ConnectParcelToDrone(drone.IdDrone);
                                }
                            }
                            catch (NoParcelsToDroneException)//אם לא מצאתה חבילה שאפשר לשייך לרחפן 
                            {
                               

                                IEnumerable <ParcelToList> parcels;
                                lock (bl)
                                {
                                    parcels = bl.GetALLParcelsNotConnectToDrone();
                                }

                                if (parcels.Any()) // if there is no parcels in requested.
                                {
                                    throw new BO.NoParcelsToDroneException("No more parcels that can be assigned to the drone, please click the button: 'Regular'");
                                }

                                else // if there is no battery to drone to take parcels.
                                {
                                    location = drone.ThisLocation; // the place before the charge
                                    lock (bl)
                                    {
                                        bl.SendingDroneforCharging(drone.IdDrone);
                                        timeDrive = bl.DistanceTo(location.Lattitude, location.Lattitude, drone.ThisLocation.Lattitude, drone.ThisLocation.Longitude) / SPEED;
                                    }

                                    Thread.Sleep(Convert.ToInt32(timeDrive) * 1000); // the place after the charge
                                }
                            }
                          
                            break;
                        }

                    case DroneStatuses.maintenance:
                        {
                            lock (bl)
                            {
                                try
                                {
                                    bl.ReleaseDroneFromChargeStation(drone.IdDrone,60); // release, to check how much battery there is to drone
                                    drone = bl.GetDrone(droneId);
                                    if (drone.ButerryStatus != 100) // if there is no to drone full battery.
                                        bl.SendingDroneforCharging(drone.IdDrone);
                                }
                                catch (BO.UnsuitableDroneMode) { }
                                
                            }
                            break;
                        }

                    case DroneStatuses.transport:
                        {
                            if (drone.PackageInTransfer[0].PackageMode == false) // if the parcel just connect, don't collect
                            {
                                lock (bl)
                                {
                                    bl.ConnectParcelToDrone(drone.IdDrone);
                                    timeDrive = drone.PackageInTransfer[0].TransportDistance / SPEED;
                                }

                                Thread.Sleep(Convert.ToInt32(timeDrive) * 1000);
                            }
                            else // if the parcel collect
                            {
                                lock (bl)
                                {
                                    bl.SupplyParcelByDrone(drone.IdDrone);
                                    timeDrive = drone.PackageInTransfer[0].TransportDistance / SPEED;
                                }

                                Thread.Sleep(Convert.ToInt32(timeDrive) * 1000);
                            }
                            break;
                        }
                }

                Thread.Sleep(DELAY);
                updateView();
            }
        }
    }
}

