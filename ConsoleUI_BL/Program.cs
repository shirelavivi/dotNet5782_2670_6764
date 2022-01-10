//using System;
//using IBL.BO;

//namespace ConsoleUI_BL
//{
//    class Program
//    {

//        public enum Options { Addition, Update, Display, ShowLists, Exit }
//        public enum Addition { AddStation, AddDrone, AddCustomer, AddParcel, Exit }
//        public enum UpDate { UpDateDrone, UpDateStation, UpDateCustomer, SendingDroneforCharging, ReleaseDronefromCharging, ParcelToDrone, CollectionParcelByDrone, SupplyParcelByDrone, Exit }
//        public enum Show { Station, Drone, Customer, Parcel, Exit }
//        public enum ShowList { Station, Drone, Customer, Parcel, ParcelFreeDrone, StationFree, Exit }


//        static void Main(string[] args)

//        {
//            try
//            {
//                IBL.BO.BL bl = new IBL.BO.BL();
//                Customer GetingCustomer()
//                {
//                    IBL.BO.Customer customer = new IBL.BO.Customer();
//                    IBL.BO.Location location = new IBL.BO.Location();
//                    int customerId;
//                    double d;
//                    Console.WriteLine("Type a customer ID:");
//                    int.TryParse(Console.ReadLine(), out customerId);
//                    customer.Id = customerId;
//                    Console.WriteLine("Type a customer name:");
//                    customer.Name = Console.ReadLine();
//                    Console.WriteLine("Type a customer phone:");
//                    customer.Phone = Console.ReadLine();
//                    Console.WriteLine("Type a Lattitude:");
//                    double.TryParse(Console.ReadLine(), out d);
//                    location.Lattitude = d;
//                    Console.WriteLine("Type a  longitude:");
//                    double.TryParse(Console.ReadLine(), out d);
//                    location.Longitude = d;
//                    customer.location = location;
//                    return customer;
//                }//פונקציה שקולטת לקוח ומחזירה עצם של BL מטיפוס לקוח
//                Options op; 
//                int num = 1;

//                while (num != 0)
//                {
//                    try
//                    {
//                        Console.WriteLine("Please press the one of the options you want:");
//                        Console.WriteLine(" 0: Addition \n 1: Update \n 2: Display \n 3: ShowLists \n 4: Exit");
//                        op = (Options)int.Parse(Console.ReadLine());    //User input to go through the options
//                        switch (op)
//                        {
//                            case Options.Addition:
//                                {
//                                    Addition add;
//                                    Console.WriteLine("Please press the one of the options you want:");
//                                    Console.WriteLine(" 0:AddStation \n 1: AddDrone \n 2:  AddCustome \n 3: AddParcel\n 4:Exit ");
//                                    add = (Addition)int.Parse(Console.ReadLine());    //User input to go through the options
//                                    switch (add)
//                                    {
//                                        case Addition.AddStation:
//                                            {

//                                                IBL.BO.BaseStation baseStation = new IBL.BO.BaseStation();
//                                                IBL.BO.Location location = new IBL.BO.Location();
//                                                int stationId;
//                                                int available;
//                                                double temp;
//                                                Console.WriteLine(" Please type a station id:");
//                                                int.TryParse(Console.ReadLine(), out stationId);
//                                                baseStation.Idnumber = stationId;
//                                                Console.WriteLine("Please type a station name:");
//                                                baseStation.NameStation = Console.ReadLine();
//                                                Console.WriteLine("Please type a  Longitude:");
//                                                double.TryParse(Console.ReadLine(), out temp);

//                                                location.Longitude = temp;
//                                                Console.WriteLine("Please type a Lattitude:");
//                                                double.TryParse(Console.ReadLine(), out temp);
//                                                location.Lattitude = temp;
//                                                baseStation.locationOfStation = location;
//                                                Console.WriteLine("Please type a charging Station Available:");
//                                                int.TryParse(Console.ReadLine(), out available);
//                                                baseStation.ChargingAvailable = available;
//                                                baseStation.droneInCharging = null;
//                                                bl.AddBaseStation(baseStation);

//                                                break;
//                                            }
//                                        case Addition.AddDrone:
//                                            {

//                                                IBL.BO.Drone drone = new IBL.BO.Drone();
//                                                IBL.BO.Location location = new IBL.BO.Location();
//                                                int droneId;
//                                                int maxweight;
//                                                int available;

//                                                Console.WriteLine(" Please type a drone id:");
//                                                int.TryParse(Console.ReadLine(), out droneId);
//                                                drone.IdDrone = droneId;
//                                                Console.WriteLine("Please type drone's model:");
//                                                drone.Model = Console.ReadLine();
                                               
//                                                Console.WriteLine("Please type a MaxWeight: 0 : easy,  1 : middle,  2 : weighty:");
//                                                int.TryParse(Console.ReadLine(), out maxweight);
//                                                drone.Weightcategories = (Weightcategories)maxweight;
//                                                Console.WriteLine("Please type a charging Station Available from the next list:");
//                                                foreach (IBL.BO.BaseStationToList item in bl.GetALLStationWithFreeStation())
//                                                {
//                                                    Console.WriteLine(item.idnumber);
//                                                }
//                                                int.TryParse(Console.ReadLine(), out available);
//                                                Random random = new Random();
//                                                drone.ButerryStatus = (random.NextDouble() * (20.0)) + 20.0;
//                                                drone.DroneStatuses = DroneStatuses.maintenance;
//                                                bl.AddDrone(drone, available);


//                                            }
//                                            break;

//                                        case Addition.AddCustomer:
//                                            {
//                                                bl.AddCustomer(GetingCustomer());

//                                            }
//                                            break;

//                                        case Addition.AddParcel:
//                                            {
//                                                IBL.BO.Parcel parcel = new IBL.BO.Parcel();
//                                                int customerId;
//                                                Console.WriteLine("Type Send Customer ID from the next list:");
//                                                foreach (CustomerToList item in bl.GetALLCostumerToList())
//                                                {
//                                                    Console.WriteLine(item.Id);
//                                                }
//                                                int.TryParse(Console.ReadLine(), out customerId);
//                                                parcel.Sender = new CustomerAtParcels();
//                                                parcel.Sender.Id = customerId;
                                               
//                                                parcel.Sender.Name = bl.GetCustomer(customerId).Name;
//                                                Console.WriteLine("Type receiveing Customer ID from the next list:");
//                                                foreach (CustomerToList item in bl.GetALLCostumerToList())
//                                                {
//                                                    Console.WriteLine(item.Id);
//                                                }
//                                                int.TryParse(Console.ReadLine(), out customerId);
//                                                parcel.Target = new CustomerAtParcels();
//                                                parcel.Target.Id = customerId;
//                                                parcel.Target.Name = bl.GetCustomer(customerId).Name;
//                                                Console.WriteLine("Choose package Weight: 0 : easy,  1 : middle,  2 : weighty:");
//                                                Weightcategories parcelWeigh;
//                                                Weightcategories.TryParse(Console.ReadLine(), out parcelWeigh);
//                                                parcel.Weight = parcelWeigh;
//                                                Console.WriteLine("Choose package Priority: 0 : Standard , 1: fast , 2:emergency:");
//                                                Priorities parcelPriority;
//                                                Priorities.TryParse(Console.ReadLine(), out parcelPriority);
//                                                parcel.Priority = parcelPriority;
//                                                bl.AddrParcel(parcel); ; // Adding the new object to the list of that object

//                                            }
//                                            break;

//                                        case Addition.Exit:
//                                            {
//                                                num = 0;

//                                            }
//                                            break;
//                                        default:
//                                            break;
//                                    }

//                                    break;
//                                }

//                            case Options.Update:
//                                {
//                                    UpDate up;
//                                    Console.WriteLine("please press the one of the options you want:");
//                                    Console.WriteLine("0: UpDateDrone \n 1:UpDateStation\n 2: UpDateCustome\n 3: SendingDroneforCharging\n  4: ReleaseDronefromCharging\n 5: ParcelToDrone\n 6:CollectionParcelByDrone\n 7:SupplyParcelByDrone \n 8:Exsit");
//                                    up = (UpDate)int.Parse(Console.ReadLine());
//                                    switch (up)
//                                    {
//                                        case UpDate.UpDateDrone:
//                                            {

//                                                int droneID;
//                                                string model;
//                                                Console.WriteLine("Type drone ID from the next list :");
//                                                foreach (DroneToList item in bl.dronesBl)
//                                                {
//                                                    Console.WriteLine(item.Idnumber);
//                                                }
//                                                int.TryParse(Console.ReadLine(), out droneID);
//                                                Console.WriteLine("Type drone model :");
//                                                model = Console.ReadLine();
//                                                bl.UpdateDrone(droneID, model);
//                                                Console.WriteLine("the Drone :" + droneID+" " +model+" "+ "is update");

//                                            }
//                                            break;

//                                        case UpDate.UpDateStation:
//                                            {

//                                                Console.WriteLine("Type ststion ID from the next list:");
//                                                foreach (BaseStationToList item in bl.GetALLbaseStationToList())
//                                                {
//                                                    Console.WriteLine(item.idnumber);
//                                                }
//                                                int stationId, numberChargSlout;
//                                                string nameStation;
//                                                int.TryParse(Console.ReadLine(), out stationId);
//                                                Console.WriteLine("Type a station name :");
//                                                nameStation = Console.ReadLine();
//                                                Console.WriteLine("Type a number of charg slouts:");
//                                                int.TryParse(Console.ReadLine(), out numberChargSlout);
//                                                bl.UpdStation(stationId, nameStation, numberChargSlout);
//                                                Console.WriteLine("the Station :" + nameStation + "update");
//                                            }
//                                            break;
//                                        case UpDate.UpDateCustomer:
//                                            {
//                                                int customerID;
//                                                string nameCustomer, phoneCustomer;
//                                                Console.WriteLine("Type Customer ID from the next list:");
//                                                foreach (CustomerToList item in bl.GetALLCostumerToList())
//                                                {
//                                                    Console.WriteLine(item.Id);
//                                                }
//                                                int.TryParse(Console.ReadLine(), out customerID);
//                                                Console.WriteLine("Type Customer Name :");
//                                                nameCustomer = Console.ReadLine();
//                                                phoneCustomer = Console.ReadLine();
//                                                bl.UpdCustomer(customerID, phoneCustomer, nameCustomer);
//                                                Console.WriteLine("the Customer:" + nameCustomer+"update");
                                                
//                                            }
//                                            break;
//                                        case UpDate.SendingDroneforCharging:
//                                            {
//                                                int droneId;
//                                                Console.WriteLine("Type drone ID from the next list :");
//                                                foreach (DroneToList item in bl.dronesBl)
//                                                {
//                                                    Console.WriteLine(item.Idnumber);
//                                                }
//                                                int.TryParse(Console.ReadLine(), out droneId);
//                                                bl.SendingDroneforCharging(droneId);
//                                                Console.WriteLine("the drone"+ droneId+ "sendig for Charging.");
//                                            }
//                                            break;
//                                        case UpDate.ReleaseDronefromCharging:// שחרור רחפן מטעינה
//                                            {
//                                                int droneID, timeInCharging;
//                                                Console.WriteLine("Type drone ID:");
//                                                foreach (DroneToList item in bl.dronesBl)
//                                                {
//                                                    if (item.DroneStatuses==DroneStatuses.maintenance)
//                                                           Console.WriteLine(item.Idnumber);
//                                                }
//                                                int.TryParse(Console.ReadLine(), out droneID);
//                                                Console.WriteLine("Type Time In Charging:");
//                                                int.TryParse(Console.ReadLine(), out timeInCharging);
//                                                bl.ReleaseDroneFromChargeStation(droneID, timeInCharging);
//                                                Console.WriteLine("The drone released from charging");

//                                            }
//                                            break;
//                                        case UpDate.ParcelToDrone://שייוך חבילה לרחפן
//                                            {
//                                                int droneId;
//                                                Console.WriteLine("Type drone ID from the next list :");
//                                                foreach (DroneToList item in bl.dronesBl)
//                                                {
//                                                    Console.WriteLine(item.Idnumber);
//                                                }
//                                                int.TryParse(Console.ReadLine(), out droneId);
//                                                bl.ConnectParcelToDrone(droneId);
//                                                Console.WriteLine("The drone connected to the package");
//                                            }
//                                            break;
//                                        case UpDate.CollectionParcelByDrone:
//                                            {
//                                                int droneID;
//                                                Console.WriteLine("Type drone ID from the next list :");
//                                                foreach (DroneToList item in bl.dronesBl)
//                                                {
//                                                    Console.WriteLine(item.Idnumber);
//                                                }
//                                                int.TryParse(Console.ReadLine(), out droneID);
//                                                bl.PickUpPackage(droneID);
//                                                Console.WriteLine("The drone collected the package");

//                                            }
//                                            break;
//                                        case UpDate.SupplyParcelByDrone:
//                                            {
//                                                int droneID;
//                                                Console.WriteLine("Type drone ID from the next list :");
//                                                foreach (DroneToList item in bl.GetALLDroneToList(x=>x.DroneStatuses==IBL.BO.DroneStatuses.transport))
//                                                {
//                                                    Console.WriteLine(item.Idnumber);
//                                                }
//                                                int.TryParse(Console.ReadLine(), out droneID);
//                                                bl.SupplyParcelByDrone(droneID);
//                                                Console.WriteLine("The drone comming to destination!");
//                                                break;
//                                            }

//                                        case UpDate.Exit:
//                                            {
//                                                num = 0;



//                                                break;
//                                            }
//                                        default:
//                                            break;

//                                    }
//                                }
//                                break;
//                            case Options.Display:
//                                {
//                                    Show ShowSomthing;
//                                    Console.WriteLine("Please type one of the following options:");
//                                    Console.WriteLine("0: Station\n 1: Drone \n 2:Customer \n 3:Parcel \n4: Exit ");
//                                    ShowSomthing = (Show)int.Parse(Console.ReadLine());
//                                    switch (ShowSomthing)
//                                    {
//                                        case Show.Station:
//                                            {
//                                                BaseStation stationToShow;
//                                                Console.WriteLine("Type a station ID  to show:");
//                                                foreach (BaseStationToList item in bl.GetALLbaseStationToList())
//                                                {
//                                                    Console.WriteLine(item.idnumber);
//                                                }
//                                                int stationlID;
//                                                int.TryParse(Console.ReadLine(), out stationlID);
                                                
//                                                stationToShow = bl.GetBaseStation(stationlID);
//                                                if (stationToShow.droneInCharging == null)
//                                                    Console.WriteLine("no exsist drone in charging ");
                                                
//                                                    foreach (DroneInCharging item in stationToShow.droneInCharging)
//                                                    {
//                                                        Console.WriteLine(item + "  ");
//                                                    }

//                                                    Console.WriteLine(stationToShow);
                                                

//                                            }
//                                            break;
//                                        case Show.Drone:
//                                            {
//                                                Drone droneToShow;
//                                                Console.WriteLine("Type Drone ID  to show:");
//                                                foreach (DroneToList item in bl.dronesBl)
//                                                {
//                                                    Console.WriteLine(item.Idnumber);
//                                                }
//                                                int dronelID;
//                                                int.TryParse(Console.ReadLine(), out dronelID);
//                                                droneToShow = bl.GetDrone(dronelID);
//                                                Console.WriteLine(droneToShow);

//                                            }
//                                            break;
//                                        case Show.Customer:
//                                            {
//                                                Customer customerToShow;
//                                                Console.WriteLine("Type Customer ID to show:");
//                                                foreach (CustomerToList item in bl.GetALLCostumerToList())
//                                                {
//                                                    Console.WriteLine(item.Id);
//                                                }
//                                                int customerID;
//                                                int.TryParse(Console.ReadLine(), out customerID);
//                                                customerToShow = bl.GetCustomer(customerID);
//                                                Console.WriteLine(customerToShow); 
//                                            }
//                                            break;
//                                        case Show.Parcel:
//                                            {
//                                                Parcel parcelToShow;
//                                                Console.WriteLine("Type Parcel ID  to show:");
//                                                foreach (ParcelToList item in bl.GetALLParcelToList())
//                                                {
//                                                    Console.WriteLine(item.Id);
//                                                }
//                                                int parcelID;
//                                                int.TryParse(Console.ReadLine(), out parcelID);
//                                                parcelToShow = bl.GetParcel(parcelID);
//                                                Console.WriteLine(parcelToShow); 
//                                            }
//                                            break;
//                                        case Show.Exit:
//                                            {
//                                                num = 0;

//                                            }

//                                            break;
//                                        default:
//                                            break;
//                                    }
//                                    break;
//                                }

//                            case Options.ShowLists:
//                                {
//                                    ShowList ShowListOf;
//                                    Console.WriteLine("Please type one of the following options:");
//                                    Console.WriteLine("0: Station\n 1: Drone \n 2:Customer \n 3:Parcel \n4: Exit ");
//                                    ShowListOf = (ShowList)int.Parse(Console.ReadLine());
//                                    switch (ShowListOf)
//                                    {
//                                        case ShowList.Station:
//                                            {
//                                                foreach (var item in bl.GetALLbaseStationToList())
//                                                {
//                                                    Console.WriteLine(item);
//                                                }
//                                                Console.WriteLine("\n");

//                                            }
//                                            break;
//                                        case ShowList.Drone:
//                                            {
//                                                foreach (var item in bl.GetALLDroneToList())
//                                                {
//                                                    Console.WriteLine(item);
//                                                }
//                                                Console.WriteLine("\n");

//                                            }
//                                            break;
//                                        case ShowList.Customer:
//                                            {
//                                                foreach (var item in bl.GetALLCostumerToList())
//                                                {
//                                                    Console.WriteLine(item);
//                                                }

//                                                Console.WriteLine("\n");

//                                            }
//                                            break;
//                                        case ShowList.Parcel:
//                                            {
//                                                foreach (var item in bl.GetALLParcelToList())
//                                                {
//                                                    Console.WriteLine(item);
//                                                }


//                                                Console.WriteLine("\n");

//                                            }
//                                            break;
//                                        case ShowList.Exit:
//                                            {
//                                                num = 0;

//                                            }
//                                            break;

//                                        default:
//                                            break;
//                                    }
//                                    break;
//                                }
//                            case Options.Exit:
//                                {
//                                    num = 0;
//                                }
//                                break;

//                        }
//                    }
//                    catch (MissingIdException ex)
//                    {
//                        Console.WriteLine(ex.Message);
//                    }
//                    catch (DuplicateIdException ex)
//                    {
//                        Console.WriteLine(ex.Message);
//                    }
//                    catch (ErorrValueExceptin ex)
//                    {
//                        Console.WriteLine(ex.Message);
//                    }
//                    catch (UnsuitableDroneMode ex)
//                    {
//                        Console.WriteLine(ex.Message);
//                    }
//                    catch (NotEnoughBattery ex)
//                    {
//                        Console.WriteLine(ex.Message);
//                    }
                   
                    

//                }

//            }
//            catch (MissingIdException ex)
//            {
//                Console.WriteLine(ex.Message);
//            }
//            catch (DuplicateIdException ex)
//            {
//                Console.WriteLine(ex.Message);
//            }
           

//        }
//    }
//}