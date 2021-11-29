using System;

using IDAL.DO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL;
using DalObject;



namespace ConsoleUI
{
    class Program
    {

        public enum Options { Addition, Update, Display, ShowLists, Exit }
        public enum Addition { AddStation, AddDrone, AddCustome, AddParcel, Exit }
        public enum UpDate { Assign, Collection, delivery, sending_for_loading, release_from_loading, Exit }
        public enum Show { Station, Drone, Customer, Parcel, Exit }
        public enum ShowList { Station, Drone, Customer, Parcel, Exit }

        static void Main(string[] args)
        {
            Options op; 
            Console.WriteLine("Please press the one of the options you want:");
            Console.WriteLine(" 0: Addition \n 1: Update \n 2: Display \n 3: ShowLists \n 4: Exit");
            op = (Options)int.Parse(Console.ReadLine());    //User input to go through the options
            int num = 1;
            DalObject.DalObject dl = new DalObject.DalObject();

            while (num != 0)
            {
                switch (op)
                {
                    case Options.Addition:
                        {
                            Addition add;
                            Console.WriteLine("Please press the one of the options you want:");
                            Console.WriteLine(" 0:AddStation \n 1: AddDrone \n 2:  AddCustome \n 3: AddParcel\n 4:Exit ");
                            add = (Addition)int.Parse(Console.ReadLine());    //User input to go through the options
                            switch (add)
                            {
                                case Addition.AddStation:
                                    {
                                        Station temp1 = new Station();
                                        int tempId;
                                        double tempLongitude;
                                        double tempLattitude;
                                        int tempChargeSlots;
                                        Console.WriteLine(" Please type a station id:");
                                        int.TryParse(Console.ReadLine(), out tempId);
                                        temp1.Id = tempId;
                                        Console.WriteLine("Please type a station name:");
                                        temp1.Name = Console.ReadLine();
                                        Console.WriteLine("Please type a  Longitude:");
                                        double.TryParse(Console.ReadLine(), out tempLongitude);
                                        temp1.Longitude = tempLongitude;
                                        Console.WriteLine("Please type a Lattitude:");
                                        double.TryParse(Console.ReadLine(), out tempLattitude);
                                        temp1.Lattitude = tempLattitude;
                                        Console.WriteLine("Please type a cChargeSlots :");
                                        int.TryParse(Console.ReadLine(), out tempChargeSlots);
                                        temp1.ChargeSlots = tempChargeSlots;
                                        dl.AddStation(temp1);   // Adding the new object to the list of that object
                                    }
                                    break;


                                case Addition.AddDrone:
                                    {
                                        Drone temp = new Drone();
                                        int tempId = 0;
                                        Console.WriteLine("Please type a drone id:");
                                        int.TryParse(Console.ReadLine(), out tempId);
                                        temp.id = tempId;
                                        Console.WriteLine("Please type a model drone :");
                                        //double tempBattery;
                                        temp.Model = Console.ReadLine();
                                        Console.WriteLine("Please Choose package Weight: 0 : easy,  1 : middle,  2 : weighty:");
                                        Weightcategories tempWeight;
                                        Weightcategories.TryParse(Console.ReadLine(), out tempWeight);
                                        temp.MaxWeight = tempWeight;
                                        Console.WriteLine("Please type DroneStatuses: 0: available, 1: maintenance, 2: transport");
                                        DroneStatuses tempstatus;
                                        DroneStatuses.TryParse(Console.ReadLine(), out tempstatus);
                                        //temp.status = tempstatus;
                                        //Console.WriteLine("Please type a Percent Battery :");
                                        //double.TryParse(Console.ReadLine(), out tempBattery);
                                        //temp.Battery = tempBattery;
                                       dl.AddDrone(temp);   // Adding the new object to the list of that object

                                    }
                                    break;
                                case Addition.AddCustome:
                                    {
                                        Customer temp = new Customer();
                                        int customerId;
                                        double d;
                                        Console.WriteLine("Type a customer ID:");
                                        int.TryParse(Console.ReadLine(), out customerId);
                                        temp.Id = customerId;
                                        Console.WriteLine("Type a customer name:");
                                        temp.Name = Console.ReadLine();
                                        Console.WriteLine("Type a customer phone:");
                                        temp.Phone = Console.ReadLine();
                                        Console.WriteLine("Type a Lattitude:");
                                        double.TryParse(Console.ReadLine(), out d);
                                        temp.Lattitude = d;
                                        Console.WriteLine("Type a  longitude:");
                                        double.TryParse(Console.ReadLine(), out d);
                                        temp.Longitude = d;
                                        dl.AddCustomer(temp);

                                    }
                                    break;
                                case Addition.AddParcel:
                                    {
                                        Parcel p = new Parcel();
                                        int temp;
                                        p.Id = dl.runNumber();
                                        Console.WriteLine("Type Send Customer ID:");
                                        int.TryParse(Console.ReadLine(), out temp);
                                        p.SenderId = temp;
                                        Console.WriteLine("Receiving customer ID:");
                                        int.TryParse(Console.ReadLine(), out temp);
                                        p.TargetId = temp;
                                        Console.WriteLine("Choose package Weight: 0 : easy,  1 : middle,  2 : weighty:");
                                        Weightcategories parcelWeigh;
                                        Weightcategories.TryParse(Console.ReadLine(), out parcelWeigh);
                                        p.Weight = parcelWeigh;
                                        Console.WriteLine("Choose package Priority: 0 : Standard , 1: fast , 2:emergency:");
                                        Priorities parcelPriority;
                                        Priorities.TryParse(Console.ReadLine(), out parcelPriority);
                                        p.Priority = parcelPriority;
                                        Console.WriteLine("Type operation skimmer ID:");
                                        int.TryParse(Console.ReadLine(), out temp);
                                        p.Priority = parcelPriority;
                                        p.Scheduled = DateTime.Now;
                                        dl.AddParcel(p); ; // Adding the new object to the list of that object

                                    }
                                    break;

                                case Addition.Exit:
                                    {
                                        num = 0;

                                    }
                                    break;
                                default:
                                    break;
                            }
                            break;
                        }
                       

                    case Options.Update:
                        {
                            UpDate up;
                            Console.WriteLine("please press the one of the options you want:");
                            Console.WriteLine(" 0:Assign a package to a skimmer\n 1: Package collection by skimmer\n 2: Delivery package to customer\n 3: sending_for_loading\n 4:release_from_loading\n 5:Exit ");
                            up = (UpDate)int.Parse(Console.ReadLine());
                            switch (up)
                            {
                                case UpDate.Assign:
                                    {
                                        Console.WriteLine("Type skimmer ID arrived package:");
                                        int droneID, parcelID;
                                        int.TryParse(Console.ReadLine(), out droneID);
                                        int.TryParse(Console.ReadLine(), out parcelID);
                                        dl.ConnectParcelToDron(parcelID, droneID);

                                    }
                                    break;
                                case UpDate.Collection:
                                    {
                                        Console.WriteLine("Type parcel ID:");
                                        int parcelID;
                                        int.TryParse(Console.ReadLine(), out parcelID);
                                        dl.collection(parcelID);
                                    }
                                    break;
                                case UpDate.delivery:
                                    {
                                        Console.WriteLine("Type parcel ID:");
                                        int parcelID;
                                        int.TryParse(Console.ReadLine(), out parcelID);
                                        dl.PackageDalvery(parcelID);
                                    }
                                    break;
                                case UpDate.sending_for_loading:
                                    {
                                        List<Station> s = dl.ShowStationAvailable();
                                        Console.WriteLine("Type drone ID:");
                                        int droneID, stationID;
                                        int.TryParse(Console.ReadLine(), out droneID);
                                        Console.WriteLine("Type a station ID from the list:");
                                        for (int i = 0; i < s.Count(); i++)
                                        {
                                            Console.WriteLine(s[i].Id + "\n");
                                        }
                                        int.TryParse(Console.ReadLine(), out stationID);
                                       dl.SendDroneTpCharge(stationID, droneID);
                                    }
                                    break;
                                case UpDate.release_from_loading:
                                    {

                                        Console.WriteLine("Type drone ID:");
                                        int droneID;
                                        int.TryParse(Console.ReadLine(), out droneID);
                                       dl.ReleaseDroneFromChargeStation(droneID);

                                    }
                                    break;
                                case UpDate.Exit:
                                    {
                                        num = 0;

                                    }
                                    break;

                                default:
                                    break;
                            }
                            break;

                        }


                    case Options.Display:
                        {
                            Show ShowSomthing;
                            Console.WriteLine("Please type one of the following options:");
                            Console.WriteLine("0: Station\n 1: Drone \n 2:Customer \n 3:Parcel \n4: Exit ");
                            ShowSomthing = (Show)int.Parse(Console.ReadLine());
                            switch (ShowSomthing)
                            {
                                case Show.Station:
                                    {
                                        Station stationToShow;
                                        Console.WriteLine("Type a station ID:");
                                        int stationlID;
                                        int.TryParse(Console.ReadLine(), out stationlID);
                                        stationToShow = dl.GetStation(stationlID);
                                        stationToShow.ToString();

                                    }
                                    break;
                                case Show.Drone:
                                    {
                                        Drone droneToShow;
                                        Console.WriteLine("Type Drone ID:");
                                        int dronelID;
                                        int.TryParse(Console.ReadLine(), out dronelID);
                                        droneToShow = dl.GetDrone(dronelID);
                                        droneToShow.ToString();

                                    }
                                    break;
                                case Show.Customer:
                                    {
                                        Customer customerToShow;
                                        Console.WriteLine("Type Customer ID:");
                                        int customerID;
                                        int.TryParse(Console.ReadLine(), out customerID);
                                        customerToShow = dl.GetCustomer(customerID);
                                        customerToShow.ToString();
                                    }
                                    break;
                                case Show.Parcel:
                                    {
                                        Parcel parcelToShow;
                                        Console.WriteLine("Type Parcel ID:");
                                        int parcelID;
                                        int.TryParse(Console.ReadLine(), out parcelID);
                                        parcelToShow = dl.GetParcel(parcelID);
                                        parcelToShow.ToString();
                                    }
                                    break;
                                case Show.Exit:
                                    {
                                        num = 0;

                                    }

                                    break;
                                default:
                                    break;
                            }
                            break;
                        }

                    case Options.ShowLists:
                        {
                            ShowList ShowListOf;
                            Console.WriteLine("Please type one of the following options:");
                            Console.WriteLine("0: Station\n 1: Drone \n 2:Customer \n 3:Parcel \n4: Exit ");
                            ShowListOf = (ShowList)int.Parse(Console.ReadLine());
                            switch (ShowListOf)
                            {
                                case ShowList.Station:
                                    {
                                       
                                        foreach (Station item in dl.GetALLStations())
                                        {
                                           item.ToString();
                                           Console.WriteLine("\n\n");
                                        }

                                    }
                                    break;
                                case ShowList.Drone:
                                    {
                                        foreach (Drone item in dl.GetALLDrones())
                                        {
                                            item.ToString();
                                            Console.WriteLine("\n\n");
                                        }
                                    }
                                    break;
                                case ShowList.Customer:
                                    {
                                        foreach (Customer item in dl.ShowCustomerList())
                                        {
                                            item.ToString();
                                            Console.WriteLine("\n\n");
                                        }
                                    }
                                    break;
                                case ShowList.Parcel:
                                    {
                                        foreach (Parcel item in dl.GetALLParcel())
                                        {
                                            item.ToString();
                                            Console.WriteLine("\n\n");
                                        }
                                    }
                                    break;
                                case ShowList.Exit:
                                    {
                                        num = 0;

                                    }
                                    break;

                                default:
                                    break;
                            }
                            break;
                        }

                    case Options.Exit:
                        {
                            num = 0;

                        }
                        break;
                    default:
                        break;
                }
                break;
            }

        }
    }
}


