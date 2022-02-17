
//using System;
//using System.Collections.Generic;
//using BO;
//using System.Threading;
//using System.Linq;


//namespace BL
//{
//    class Simulator
//    {
//        private const int DELAY = 500; // milliseconds
//        private const double SPEED = 40; // km per hour
//        private Drone drone;
//        private Location location;
//        private double timeDrive;

//        public Simulator(BlApi.IBL bl, int droneId, Action updateView, Func<bool> stopSimulator)
//        {
//            while (!stopSimulator())
//            {
//                lock (bl)
//                {
//                    drone = bl.getDrone(droneId);
//                }

//                switch (drone.DroneStatuses)
//                {
//                    case DroneStatuses.available:
//                    {
//                            try
//                            {
//                                lock (bl)
//                                {
//                                    bl.ConnectParcelToDrone(drone.IdDrone);
//                                }
//                            }
//                            catch (DO.DuplicateIdException ex)
//                            {
//                                throw new BO.DuplicateIdException(ex.ID, ex.EntityName);
//                            }
//                            catch (DO.MissingIdException ex)
//                            {
//                                throw new BO.MissingIdException(ex.ID, ex.EntityName);
//                            }

//                        //    catch (NoParcelsToDroneException)
//                        //    {
//                        //        IEnumerable<ParcelToList> parcels;
//                        //        lock (bl)
//                        //        {
//                        //            parcels = bl.GetParcelsNoDrones();
//                        //        }

//                        //        if (parcels.Any()) // if there is no parcels in requested.
//                        //        {
//                        //            throw new NoParcelsToDroneException("No more parcels that can be assigned to the drone, please click the button: 'Regular'");
//                        //        }

//                        //        else // if there is no battery to drone to take parcels.
//                        //        {
//                        //            location = drone.ThisLocation; // the place before the charge
//                        //            lock (bl)
//                        //            {
//                        //                bl.SendingDroneforCharging(drone.IdDrone);
//                        //                timeDrive = bl.distanceTo(location, drone.ThisLocation) / SPEED;
//                        //            }

//                        //            Thread.Sleep(Convert.ToInt32(timeDrive) * 1000); // the place after the charge
//                        //        }
//                        //    }
//                        //    catch (StatusDroneException) { }

//                          break;
//                        }

//                    case DroneStatuses.maintenance:
//                        {
//                            lock (bl)
//                            {
//                                try
//                                {
//                                    bl.ReleaseDroneFromChargeStation(drone.IdDrone,); // release, to check how much battery there is to drone
//                                    drone = bl.GetDrone(droneId);
//                                    if (drone.ButerryStatus != ButerryStatus.Full) // if there is no to drone full battery.
//                                        bl.SendingDroneforCharging(drone.IdDrone);
//                                }
//                                //catch (StatusDroneException) { }
//                                catch (DO.DuplicateIdException ex)
//                                {
//                                    throw new BO.DuplicateIdException(ex.ID, ex.EntityName);
//                                }
//                                catch (DO.MissingIdException ex)
//                                {
//                                    throw new BO.MissingIdException(ex.ID, ex.EntityName);
//                                }
//                            }
//                            break;
//                        }

//                    case DroneStatuses.transport:
//                        {
//                            if (drone.PackageInTransfer.OnTheWay == false) // if the parcel just connect, don't collect
//                            {
//                                lock (bl)
//                                {
//                                    bl.ConnectParcelToDrone(drone.IdDrone);
//                                    timeDrive = drone.PackageInTransfer.DistanceOfTransfer / SPEED;
//                                }

//                                Thread.Sleep(Convert.ToInt32(timeDrive) * 1000);
//                            }
//                            else // if the parcel collect
//                            {
//                                lock (bl)
//                                {
//                                    bl.SupplyParcelByDrone(drone.IdDrone);
//                                    timeDrive = drone.PackageInTransfer.DistanceOfTransfer / SPEED;
//                                }

//                                Thread.Sleep(Convert.ToInt32(timeDrive) * 1000);
//                            }
//                            break;
//                        }
//                }

//                Thread.Sleep(DELAY);
//                updateView();
//            }
//        }
//    }
//}

