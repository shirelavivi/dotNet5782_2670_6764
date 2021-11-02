﻿using System;
using IDAL.DO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DalObject;


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
            IDAL.DalObject.DalObject Obg=new DalObject();
            Options op;
            Console.WriteLine("please press the one of the options you want:");
            Console.WriteLine(" 0: Addition \n 1: Update \n 2: Display \n 3: ShowLists \n 4: Exit");
            op = (Options)int.Parse(Console.ReadLine());    //User input to go through the options
            int num = 1;

            while (num != 0)
            {
                switch (op)
                {
                    case Options.Addition:
                        {
                            Addition add;
                            Console.WriteLine("please press the one of the options you want:");
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
                                        IDAL.DalObject.DalObject.add(temp1);   // Adding the new object to the list of that object
                                    }
                                    break;


                                case Addition.AddDrone:
                                    {
                                        Drone temp = new Drone();
                                        int tempId=0;
                                        Console.WriteLine(" Please type a drone id:");
                                        int.TryParse(Console.ReadLine(), out tempId);
                                        temp.id = tempId;
                                        Console.WriteLine("Please type a model drone :");
                                        double tempBattery;
                                        temp.Model = Console.ReadLine();
                                        Console.WriteLine("Please Choose package Weight: 0 : easy,  1 : middle,  2 : weighty:");
                                        Weightcategories tempWeight;
                                        Weightcategories.TryParse(Console.ReadLine(), out tempWeight);//convert??
                                        temp.MaxWeight = tempWeight;
                                        Console.WriteLine("Please type DroneStatuses: 0:available\n 1: maintenance\n2: transport");
                                        DroneStatuses tempstatus;
                                        DroneStatuses.TryParse(Console.ReadLine(), out tempstatus);
                                        temp.status = tempstatus;
                                        Console.WriteLine("Please type a Percent Battery :");
                                        double.TryParse(Console.ReadLine(), out tempBattery);
                                        temp.Battery = tempBattery;
                                        IDAL.DalObject.DalObject.add(temp);   // Adding the new object to the list of that object

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
                                        IDAL.DalObject.DalObject.add(temp);

                                    }
                                    break;
                                case Addition.AddParcel:
                                    {
                                        Parcel p = new Parcel();
                                        int temp;
                                        p.Id= IDAL.DalObject.DalObject.runNumber();
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
                                        IDAL.DalObject.DalObject.add(p); ; // Adding the new object to the list of that object

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
                                        IDAL.DalObject.DalObject.ConnectParcelToDron(parcelID, droneID);

                                    }
                                    break;
                                case UpDate.Collection:
                                    {
                                        Console.WriteLine("Type parcel ID:");
                                        int parcelID;
                                        int.TryParse(Console.ReadLine(), out parcelID);
                                        IDAL.DalObject.DalObject.collection(parcelID);
                                    }
                                    break;
                                case UpDate.delivery:
                                    {
                                        Console.WriteLine("Type parcel ID:");
                                        int parcelID;
                                        int.TryParse(Console.ReadLine(), out parcelID);
                                        IDAL.DalObject.DalObject.PackageDalvery(parcelID);
                                    }
                                    break;
                                case UpDate.sending_for_loading:
                                    {
                                        List<Station> s = IDAL.DalObject.DalObject.ShowStationAvailable();
                                        Console.WriteLine("Type drone ID:");
                                        int droneID, stationID;
                                        int.TryParse(Console.ReadLine(), out droneID);
                                        Console.WriteLine("Type a station ID from the list:");
                                        for (int i = 0; i < s.Count(); i++)
                                        {
                                            Console.WriteLine(s[i].Id +"\n");
                                        }
                                        int.TryParse(Console.ReadLine(), out stationID);
                                        IDAL.DalObject.DalObject.SendDroneTpCharge(stationID,droneID);
                                    }
                                    break;
                                case UpDate.release_from_loading:
                                    {
                                        
                                        Console.WriteLine("Type drone ID:");
                                        int droneID;
                                        int.TryParse(Console.ReadLine(), out droneID);
                                        IDAL.DalObject.DalObject.ReleaseDroneFromChargeStation(droneID);

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
                                        stationToShow = IDAL.DalObject.DalObject.ShowStation(stationlID);
                                        stationToShow.Tostring();

                                    }
                                    break;
                                case Show.Drone:
                                    {
                                        Drone droneToShow;
                                        Console.WriteLine("Type skimmer ID:");
                                        int dronelID;
                                        int.TryParse(Console.ReadLine(), out dronelID);
                                        droneToShow = IDAL.DalObject.DalObject.ShowDrone(dronelID);
                                        droneToShow.Tostring();

                                    }
                                    break;
                                case Show.Customer:
                                    {
                                        Customer customerToShow;
                                        Console.WriteLine("Type Customer ID:");
                                        int customerID;
                                        int.TryParse(Console.ReadLine(), out customerID);
                                        customerToShow = IDAL.DalObject.DalObject.ShowCustomer(customerID);
                                        customerToShow.Tostring();
                                    }
                                    break;
                                case Show.Parcel:
                                    {
                                        Parcel parcelToShow;
                                        Console.WriteLine("Type Pack ID:");
                                        int parcelID;
                                        int.TryParse(Console.ReadLine(), out parcelID);
                                        parcelToShow = IDAL.DalObject.DalObject.ShowParcel(parcelID);
                                        parcelToShow.Tostring();
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
                                        List<Station> allStation = IDAL.DalObject.DalObject.ShowStationList();
                                        for (int i = 0; i < allStation.Count(); i++)
                                        {
                                            allStation[i].Tostring();
                                            Console.WriteLine("\n\n");
                                        }

                                    }
                                    break;
                                case ShowList.Drone:
                                    {
                                        List<Drone> allDrones = IDAL.DalObject.DalObject.ShowDroneList();
                                        for (int i = 0; i < allDrones.Count(); i++)
                                        {
                                            allDrones[i].Tostring();
                                            Console.WriteLine("\n\n");
                                        }
                                    }
                                    break;
                                case ShowList.Customer:
                                    {
                                        List<Customer> allCustomer = IDAL.DalObject.DalObject.ShowCustomerList();
                                        for (int i = 0; i < allCustomer.Count(); i++)
                                        {
                                            allCustomer[i].Tostring();
                                            Console.WriteLine("\n\n");
                                        }
                                    }
                                    break;
                                case ShowList.Parcel:
                                    {
                                        List<Parcel> allParcel = IDAL.DalObject.DalObject.ShowParcelList();
                                        for (int i = 0; i < allParcel.Count(); i++)
                                        {
                                            allParcel[i].Tostring();
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
























//using System;
//using IDAL.DO;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using IDAL.DalObject;


//namespace ConsoleUI
//{
//    class Program
//    {
//        public enum Options { Addition, Update, Display, ShowLists, Exit }
//<<<<<<< HEAD
//        public enum Addition { AddStation, AddDrone, AddCustome, AddParcel, Exit }
//        public enum UpDate { Assign, Collection, delivery, sending_for_loading, release_from_loading, Exit }
//        public enum Show { Station, Drone, Customer, Parcel, Exit }
//        public enum ShowList { Station, Drone, Customer, Parcel, Exit }

//=======
//        public enum Addition { AddStation, AddDrone, AddCustomer, AddParcel }
//        public enum Update { connectParcelToDrone, PickedUpByDrone, DeliveredToCustomer,}
//>>>>>>> e4cf01371ea474ccd0d879a94287be7a9bba2167
//        static void Main(string[] args)
//        {
//            Options op;
//            Console.WriteLine("please press the one of the options you want:");
//            Console.WriteLine(" 1: Addition \n 2: Update \n 3: Display \n 4: ShowLists \n 5: Exit");
//            op = (Options)int.Parse(Console.ReadLine());    //User input to go through the options
//            int num = 1;

//            while (num != 0)
//            {
//                switch (op)
//                {
//                    case Options.Addition:
//                        {
//                            Addition add;
//                            Console.WriteLine("please press the one of the options you want:");
//                            Console.WriteLine(" 1:AddStation \n 2: AddDrone \n 3:  AddCustome \n 4: AddParcel ");
//                            add = (Addition)int.Parse(Console.ReadLine());    //User input to go through the options
//                            switch (add)
//                            {
//                                case Addition.AddStation:
//                                    {
//                                        Station temp1 = new Station();
//                                        int tempId;
//                                        double tempLongitude;
//                                        double tempLattitude;
//                                        int tempChargeSlots;
//                                        int.TryParse(Console.ReadLine(), out tempId);
//                                        temp1.Name = Console.ReadLine();
//                                        double.TryParse(Console.ReadLine(), out tempLongitude);
//                                        temp1.Longitude = tempLongitude;
//                                        double.TryParse(Console.ReadLine(), out tempLattitude);
//                                        temp1.Lattitude = tempLattitude;
//                                        int.TryParse(Console.ReadLine(), out tempChargeSlots);
//                                        temp1.ChargeSlots = tempChargeSlots;
//                                        IDAL.DalObject.DalObject.add(temp1);   // Adding the new object to the list of that object


//                                    }
//                                    break;
//                                case Addition.AddDrone:
//                                    {
//                                        Drone temp = new Drone();
//                                        int tempId;
//                                        int.TryParse(Console.ReadLine(), out tempId);
//                                        double tempBattery;
//                                        temp.Model = Console.ReadLine();
//                                        Weightcategories tempWeight;
//                                        Weightcategories.TryParse(Console.ReadLine(), out tempWeight);//convert??
//                                        temp.MaxWeight = tempWeight;
//                                        DroneStatuses tempstatus;
//                                        DroneStatuses.TryParse(Console.ReadLine(), out tempstatus);
//                                        temp.status = tempstatus;
//                                        double.TryParse(Console.ReadLine(), out tempBattery);
//                                        IDAL.DalObject.DalObject.add(temp);   // Adding the new object to the list of that object

//                                    }
//                                    break;
//                                case Addition.AddCustomer:
//                                    {
//                                        Customer temp = new Customer();
//                                        int customerId;
//                                        double d;
//                                        Console.WriteLine("Type a customer ID:");
//                                        int.TryParse(Console.ReadLine(), out customerId);
//                                        temp.Id = customerId;
//                                        Console.WriteLine("Type a customer name:");
//                                        temp.Name = Console.ReadLine();
//                                        Console.WriteLine("Type a customer phone:");
//                                        temp.Phone = Console.ReadLine();
//                                        Console.WriteLine("Type a Lattitude:");
//                                        double.TryParse(Console.ReadLine(), out d);
//                                        temp.Lattitude = d;
//                                        Console.WriteLine("Type a  longitude:");
//                                        double.TryParse(Console.ReadLine(), out d);
//                                        temp.Longitude = d;
//                                        IDAL.DalObject.DalObject.add(temp);

//                                    }
//                                    break;
//                                case Addition.AddParcel:
//                                    {
//                                        Parcel p = new Parcel();
//                                        int temp;
//                                        Console.WriteLine("Type Send Customer ID:");
//                                        int.TryParse(Console.ReadLine(), out temp);
//                                        p.SenderId = temp;
//                                        Console.WriteLine("Receiving customer ID:");
//                                        int.TryParse(Console.ReadLine(), out temp);
//                                        p.TargetId = temp;
//                                        Console.WriteLine("Choose package Weight: 0 : easy,  1 : middle,  2 : weighty:");
//                                        Weightcategories parcelWeigh;
//                                        Weightcategories.TryParse(Console.ReadLine(), out parcelWeigh);
//                                        p.Weight = parcelWeigh;
//                                        Console.WriteLine("Choose package Priority: 0 : Standard , 1: fast , 2:emergency:");
//                                        Priorities parcelPriority;
//                                        Priorities.TryParse(Console.ReadLine(), out parcelPriority);
//                                        p.Priority = parcelPriority;
//                                        Console.WriteLine("Type operation skimmer ID:");
//                                        int.TryParse(Console.ReadLine(), out temp);
//                                        p.Priority = parcelPriority;
//                                        p.Scheduled = DateTime.Now;
//                                        IDAL.DalObject.DalObject.add(p); ; // Adding the new object to the list of that object

//                                    }
//                                    break;
//                                case Addition.Exit:

//                                default:
//                                    break;
//                            }
//                        }

//                        break;
//                    case Options.Update:
//                        {
//                            UpDate up;
//                            Console.WriteLine("please press the one of the options you want:");
//                            Console.WriteLine(" 1:Assign a package to a skimmer\n 2: Package collection by skimmer\n 3: Delivery package to customer\n 4: sending_for_loading\n 5:release_from_loading\n 6:Exit ");
//                            up = (UpDate)int.Parse(Console.ReadLine());
//                            switch (up)
//                            {
//                                case UpDate.Assign:
//                                    {
//                                        Console.WriteLine("הקלד מזהה רחפן ולאחר מכן מזהה חבילה:");
//                                        int droneID, parcelID;
//                                        int.TryParse(Console.ReadLine(), out droneID);
//                                        int.TryParse(Console.ReadLine(), out parcelID);
//                                        IDAL.DalObject.DalObject.ConnectParcelToDron(parcelID, droneID);

//                                    }
//                                    break;
//                                case UpDate.Collection:
//                                    {
//                                        Console.WriteLine("הקלד מזהה חבילה:");
//                                        int parcelID;
//                                        int.TryParse(Console.ReadLine(), out parcelID);
//                                        IDAL.DalObject.DalObject.collection(parcelID);
//                                    }
//                                    break;
//                                case UpDate.delivery:
//                                    {
//                                        Console.WriteLine("הקלד מזהה חבילה:");
//                                        int parcelID;
//                                        int.TryParse(Console.ReadLine(), out parcelID);
//                                        IDAL.DalObject.DalObject.PackageDalvery(parcelID);
//                                    }
//                                    break;
//                                case UpDate.sending_for_loading:
//                                    {

//                                    }
//                                    break;
//                                case UpDate.release_from_loading:
//                                    break;
//                                case UpDate.Exit:
//                                    break;
//                                default:
//                                    break;
//                            }

//<<<<<<< HEAD
//=======
//                        break;
//                    case Options.Update:
//                        {
//>>>>>>> e4cf01371ea474ccd0d879a94287be7a9bba2167

//                        }

//                        break;
//                    case Options.Display:
//                        {
//                            Show ShowSomthing;
//                            Console.WriteLine("הקלד בבקשה את אחת מהאופציות הבאות:");
//                            Console.WriteLine("1: Station\n 2: Drone \n 3:Customer \n 4:Parcel \n5: Exit ");
//                            ShowSomthing = (Show)int.Parse(Console.ReadLine());
//                            switch (ShowSomthing)
//                            {
//                                case Show.Station:
//                                    {
//                                        Station stationToShow;
//                                        Console.WriteLine("הקלד מזהה תחנה:");
//                                        int stationlID;
//                                        int.TryParse(Console.ReadLine(), out stationlID);
//                                        stationToShow = IDAL.DalObject.DalObject.ShowStation(stationlID);
//                                        stationToShow.Tostring();

//                                    }
//                                    break;
//                                case Show.Drone:
//                                    {
//                                        Drone droneToShow;
//                                        Console.WriteLine("הקלד מזהה רחפן:");
//                                        int dronelID;
//                                        int.TryParse(Console.ReadLine(), out dronelID);
//                                        droneToShow = IDAL.DalObject.DalObject.ShowDrone(dronelID);
//                                        droneToShow.Tostring();

//                                    }
//                                    break;
//                                case Show.Customer:
//                                    {
//                                        Customer customerToShow;
//                                        Console.WriteLine("הקלד מזהה לקוח:");
//                                        int customerID;
//                                        int.TryParse(Console.ReadLine(), out customerID);
//                                        customerToShow = IDAL.DalObject.DalObject.ShowCustomer(customerID);
//                                        customerToShow.Tostring();
//                                    }
//                                    break;
//                                case Show.Parcel:
//                                    {
//                                        Parcel parcelToShow;
//                                        Console.WriteLine("הקלד מזהה חבילה:");
//                                        int parcelID;
//                                        int.TryParse(Console.ReadLine(), out parcelID);
//                                        parcelToShow = IDAL.DalObject.DalObject.ShowParcel(parcelID);
//                                        parcelToShow.Tostring();
//                                    }
//                                    break;
//                                case Show.Exit:
//                                    break;
//                                default:
//                                    break;
//                            }
//                        }
//                        break;
//                    case Options.ShowLists:
//                        {
//                            ShowList ShowListOf;
//                            Console.WriteLine("הקלד בבקשה את אחת מהאופציות הבאות:");
//                            Console.WriteLine("1: Station\n 2: Drone \n 3:Customer \n 4:Parcel \n5: Exit ");
//                            ShowListOf = (ShowList)int.Parse(Console.ReadLine());
//                            switch (ShowListOf)
//                            {
//                                case ShowList.Station:
//                                    {
//                                        List<Station> allStation = IDAL.DalObject.DalObject.ShowStationList();
//                                        for (int i = 0; i < allStation.Count(); i++)
//                                        {
//                                            allStation[i].Tostring();
//                                            Console.WriteLine("\n\n");
//                                        }

//                                    }
//                                    break;
//                                case ShowList.Drone:
//                                    {
//                                        List<Drone> allDrones = IDAL.DalObject.DalObject.ShowDroneList();
//                                        for (int i = 0; i < allDrones.Count(); i++)
//                                        {
//                                            allDrones[i].Tostring();
//                                            Console.WriteLine("\n\n");
//                                        }
//                                    }
//                                    break;
//                                case ShowList.Customer:
//                                    {
//                                        List<Customer> allCustomer = IDAL.DalObject.DalObject.ShowCustomerList();
//                                        for (int i = 0; i < allCustomer.Count(); i++)
//                                        {
//                                            allCustomer[i].Tostring();
//                                            Console.WriteLine("\n\n");
//                                        }
//                                    }
//                                    break;
//                                case ShowList.Parcel:
//                                    {
//                                        List<Parcel> allParcel = IDAL.DalObject.DalObject.ShowParcelList();
//                                        for (int i = 0; i < allParcel.Count(); i++)
//                                        {
//                                            allParcel[i].Tostring();
//                                            Console.WriteLine("\n\n");
//                                        }
//                                    }
//                                    break;
//                                case ShowList.Exit:
//                                    break;
//                                default:
//                                    break;
//                            }
//                        }

//                        break;
//                    case Options.Exit:
//                        num = 0;
//                        break;
//                    default:
//                        break;
//                }
//            }

//        }
//    }
//}











/*using System;
using IDAL.DO;
using DalObject;


namespace ConsoleUI
{
    class Program
    {
        ///Enum for for User Option
        enum Menu { Exit, Add, Update, DisplayItem, DisplayList, Distance };
        enum UpdateOptions { Exit, Assignment, PickedUp, Delivered, Charging, FinishCharging };
        enum ObjectMenu { Exit, Client, Drone, Station, Package };
        enum ObjectList { Exit, ClientList, DroneList, StationList, PackageList, PackageWithoutDrone, StationWithCharging };
        enum DistanceOptions { Exit, Client, Station };

        /// <summary>
        /// Main function to run the program, the program get user input and display the relevant application from user choice, User can: Add An object, Update different type of information, Display specific object and Display every element from different list.
        /// </summary>
        public static void Display()
        {
            Menu choice;
            ObjectMenu objectMenu;
            UpdateOptions updateOptions;
            ObjectList objectList;
            DistanceOptions distanceOptions;
            int num = 1;

            while (num != 0)
            {
                Console.WriteLine("Choose an Option:");
                Console.WriteLine(" 1: Add \n 2: Update \n 3: Display specific Item \n 4: Display Item List \n 5: Distance \n 0: Exit");
                choice = (Menu)int.Parse(Console.ReadLine());    //User input to go through the menu

                switch (choice)
                {
                    case Menu.Add:  //Adding a new Object to the list of different object
                        {
                            Console.WriteLine("Choose an Adding Option: \n 1 : Client \n 2 : Drone \n 3 : Station: \n 4 : Package \n ");
                            objectMenu = (ObjectMenu)int.Parse(Console.ReadLine());

                            switch (objectMenu)
                            {
                                case ObjectMenu.Client:

                                    Console.WriteLine("Enter Client Data: ID, Name, Phone, Latitude, Longitude  \n");  // Getting Client data from user
                                    int clientId;
                                    int.TryParse(Console.ReadLine(), out clientId);
                                    string clientName = Console.ReadLine();
                                    string clientPhone = Console.ReadLine();
                                    double clientLatitude, clientLongitude;
                                    double.TryParse(Console.ReadLine(), out clientLatitude);
                                    double.TryParse(Console.ReadLine(), out clientLongitude);

                                    Client client = new Client();   //creating new object then assigning user input to that object

                                    client.ID = clientId;
                                    client.Name = clientName;
                                    client.Phone = clientPhone;
                                    client.Latitude = clientLatitude;
                                    client.Longitude = clientLongitude;

                                    DalObject.DalObject.AddClient(client);  // Adding the new object to the list of that object
                                    break;

                                case ObjectMenu.Drone:

                                    Console.WriteLine("Enter Drone Data: ID, Model, Weight, Status, Battery \n");  // Getting Drone data from user
                                    int droneId;
                                    int.TryParse(Console.ReadLine(), out droneId);
                                    string droneModel = Console.ReadLine();
                                    Console.WriteLine("Choose Drone Weight: 0 : Light, 1 : Medium, 2 : Heavy :\n");  //getting different type of weight from user
                                    string chosen = (Console.ReadLine());  //used to get the num from user and chose with it different enum option
                                    WeightCategories droneWeight = (WeightCategories)Convert.ToInt32(chosen);
                                    Console.WriteLine("Choose Drone Status: 0 : Available, 1 : Maintenance, 2 : Shipping :\n"); // For different type of status from user
                                    chosen = (Console.ReadLine());
                                    DroneStatus droneStatus = (DroneStatus)Convert.ToInt32(chosen);
                                    double droneBattery;
                                    double.TryParse(Console.ReadLine(), out droneBattery);

                                    Drone drone = new Drone();      //creating new object then assigning user input to that object

                                    drone.ID = droneId;
                                    drone.Model = droneModel;
                                    drone.MaxWeight = droneWeight;
                                    drone.Status = droneStatus;
                                    drone.Battery = droneBattery;

                                    DalObject.DalObject.AddDrone(drone);   // Adding the new object to the list of that object
                                    break;

                                case ObjectMenu.Station:

                                    Console.WriteLine("Enter Station Data: ID, Name, Num of ChargingSlot, Longitude, Latitude\n");   // Getting Station data from user
                                    int stationId;
                                    int.TryParse(Console.ReadLine(), out stationId);
                                    string stationName = Console.ReadLine();
                                    int stationChargeSlot = int.Parse(Console.ReadLine());
                                    double stationLatitude, stationLongitude;
                                    double.TryParse(Console.ReadLine(), out stationLatitude);
                                    double.TryParse(Console.ReadLine(), out stationLongitude);

                                    Station station = new Station();  //creating new object then assigning user input to that object

                                    station.ID = stationId;
                                    station.Name = stationName;
                                    station.ChargeSlots = stationChargeSlot;
                                    station.Longitude = stationLongitude;
                                    station.Latitude = stationLatitude;

                                    DalObject.DalObject.AddStation(station);        // Adding the new object to the list of that object
                                    break;

                                case ObjectMenu.Package:
                                    Console.WriteLine("Enter All Package Data: SenderId, TargetId, DroneId, MaxWeight, Priority");  // Getting Package data from user
                                    int packageSenderId, packageTargetId, packageDroneId;
                                    int.TryParse(Console.ReadLine(), out packageSenderId);
                                    int.TryParse(Console.ReadLine(), out packageTargetId);
                                    int.TryParse(Console.ReadLine(), out packageDroneId);

                                    Console.WriteLine("Choose package Weight: 0 : Light, 1 : Medium, 2 : Heavy :");
                                    chosen = (Console.ReadLine());
                                    WeightCategories packageWeight = (WeightCategories)Convert.ToInt32(chosen);
                                    Console.WriteLine("Choose package Priority: 0 :  Standard, 1 : Fast, 2 : Urgent :");
                                    chosen = (Console.ReadLine());
                                    Priorities packagePriority = (Priorities)Convert.ToInt32(chosen);

                                    Package package = new Package();   //creating new object then assigning user input to that object

                                    package.ID = 0;
                                    package.SenderId = packageSenderId;
                                    package.TargetId = packageTargetId;
                                    package.DroneId = packageDroneId;
                                    package.Weight = packageWeight;
                                    package.Priority = packagePriority;
                                    package.Created = DateTime.Now;

                                    Console.WriteLine($"Your package ID number is {DalObject.DalObject.AddPackage(package)}\n"); // Adding the new object to the list of that object
                                    break;

                                default:
                                    break;
                            }
                            break;
                        }

                    case Menu.Update:   //Update item
                        {
                            Console.WriteLine("Choose an Option:");
                            Console.WriteLine(" 1: Assigning a package to a drone \n 2: Pick Up Package by Drone \n 3: Delivery of a package to the client: \n 4: Charging drone \n 5: Finish charging drone \n 0: Exit");  ///User Choose Different type of Update
                            updateOptions = (UpdateOptions)int.Parse(Console.ReadLine());

                            switch (updateOptions)
                            {
                                case UpdateOptions.Exit:
                                    break;
                                case UpdateOptions.Assignment:  //Assign Package to a drone using Drone and package ID.
                                    int droneId, packageId;
                                    Console.WriteLine("What is the drone's ID?");
                                    int.TryParse(Console.ReadLine(), out droneId);
                                    Console.WriteLine("What is the package's ID?");
                                    int.TryParse(Console.ReadLine(), out packageId);
                                    DalObject.DalObject.packageToDrone(DalObject.DalObject.PackageById(packageId), DalObject.DalObject.DroneById(droneId));  //Getting ID input then sending the ID inputed to Dalobject method packagebyId and droneById that return the items who match the Id's then put both items in packageToDrone method
                                    break;

                                case UpdateOptions.PickedUp:    //Getting a drone to pick up a package 
                                    Console.WriteLine("What is the package's ID?");
                                    int.TryParse(Console.ReadLine(), out packageId);
                                    DalObject.DalObject.PickedUpByDrone(DalObject.DalObject.PackageById(packageId));  // Same as previous
                                    break;

                                case UpdateOptions.Delivered:   //Deliver a Package to a client
                                    Console.WriteLine("What is the package's ID?");
                                    int.TryParse(Console.ReadLine(), out packageId);
                                    DalObject.DalObject.DeliveredToClient(DalObject.DalObject.PackageById(packageId));
                                    break;

                                case UpdateOptions.Charging:    //sending a drone to a station to get it charged
                                    Console.WriteLine("What is the drone's ID?");
                                    int.TryParse(Console.ReadLine(), out droneId);
                                    Console.WriteLine("At which station do you want to recharge the drone?\n");
                                    foreach (var station in (DalObject.DalObject.StationWithCharging()))  // Display the stations list who have places to charge
                                    {
                                        Console.WriteLine(station);
                                    }
                                    Console.WriteLine("What is the station ID ?\n");
                                    int stationId;
                                    int.TryParse(Console.ReadLine(), out stationId);
                                    DalObject.DalObject.DroneCharge(DalObject.DalObject.DroneById(droneId), stationId);
                                    break;

                                case UpdateOptions.FinishCharging:  //Getting a drone back from charging
                                    Console.WriteLine("What is the drone's ID?");
                                    int.TryParse(Console.ReadLine(), out droneId);
                                    DalObject.DalObject.FinishCharging(DalObject.DalObject.DroneChargeByIdDrone(droneId));
                                    break;

                                default:
                                    break;
                            }
                            break;
                        }
                    case Menu.DisplayItem:   // Output Specific item Data
                        {
                            Console.WriteLine("Choose which Item to display: \n 1: Client \n 2: Drone \n 3: Station \n 4: Package \n 0: Exit ");
                            objectMenu = (ObjectMenu)int.Parse(Console.ReadLine());
                            switch (objectMenu)
                            {
                                case ObjectMenu.Exit:
                                    break;

                                case ObjectMenu.Client:
                                    Console.WriteLine("What is the client's ID?");
                                    int clientId;
                                    int.TryParse(Console.ReadLine(), out clientId);
                                    Console.WriteLine(DalObject.DalObject.ClientById(clientId));  //output the tostring func of client object that match the id user inputed
                                    break;

                                case ObjectMenu.Drone:
                                    Console.WriteLine("What is the drone's ID?");
                                    int droneId;
                                    int.TryParse(Console.ReadLine(), out droneId);
                                    Console.WriteLine(DalObject.DalObject.DroneById(droneId));   //same for rest
                                    break;

                                case ObjectMenu.Station:
                                    Console.WriteLine("What is the station's ID?");
                                    int stationId;
                                    int.TryParse(Console.ReadLine(), out stationId);
                                    Console.WriteLine(DalObject.DalObject.StationById(stationId));
                                    break;

                                case ObjectMenu.Package:
                                    Console.WriteLine("What is the package's ID?");
                                    int packageId;
                                    int.TryParse(Console.ReadLine(), out packageId);
                                    Console.WriteLine(DalObject.DalObject.PackageById(packageId));
                                    break;

                                default:
                                    break;
                            }
                            break;
                        }
                    case Menu.DisplayList:   // Output all list of different object
                        {
                            Console.WriteLine("Choose Which Item list to display: \n 1: Clients list \n 2: Drones list\n 3: Stations list \n 4: Packages list\n 5: List of packages that do not belong to the drone \n 6: List of stations with available charging slots \n 0: Exit ");
                            objectList = (ObjectList)int.Parse(Console.ReadLine());
                            switch (objectList)
                            {
                                case ObjectList.Exit:
                                    break;

                                case ObjectList.ClientList:
                                    foreach (var client in DalObject.DalObject.ClientsList())  // Display every element in Client list, same for all
                                    {
                                        Console.WriteLine(client);
                                    }
                                    break;

                                case ObjectList.DroneList:
                                    foreach (var drone in DalObject.DalObject.DroneList())
                                    {
                                        Console.WriteLine(drone);
                                    }
                                    break;

                                case ObjectList.StationList:
                                    foreach (var station in DalObject.DalObject.StationsList())
                                    {
                                        Console.WriteLine(station);
                                    }
                                    break;

                                case ObjectList.PackageList:
                                    foreach (var package in DalObject.DalObject.PackageList())
                                    {
                                        Console.WriteLine(package);
                                    }
                                    break;

                                case ObjectList.PackageWithoutDrone:
                                    foreach (var package in DalObject.DalObject.PackageWithoutDrone())
                                    {
                                        Console.WriteLine(package);
                                    }
                                    break;

                                case ObjectList.StationWithCharging:
                                    foreach (var station in DalObject.DalObject.StationWithCharging())
                                    {
                                        Console.WriteLine(station);
                                    }
                                    break;

                                default:
                                    break;
                            }
                            break;
                        }
                    case Menu.Distance:
                        {
                            double latitude, longitude;
                            int ID;

                            Console.WriteLine("What is the Latitude?");
                            double.TryParse(Console.ReadLine(), out latitude);
                            Console.WriteLine("What is the Longitude?");
                            double.TryParse(Console.ReadLine(), out longitude);

                            Console.WriteLine("To where do you want to check distance ? client - 1 Station - 2");
                            distanceOptions = (DistanceOptions)int.Parse(Console.ReadLine());
                            switch (distanceOptions)
                            {
                                case DistanceOptions.Exit:
                                    break;

                                case DistanceOptions.Client:
                                    Console.WriteLine("What is the client ID ?");
                                    int.TryParse(Console.ReadLine(), out ID);
                                    Client client = DalObject.DalObject.ClientById(ID);
                                    Console.WriteLine($"The distance is: {Math.Round(DAL.Coordinates.distance(latitude, longitude, client.Latitude, client.Longitude), 3)}");
                                    break;

                                case DistanceOptions.Station:
                                    Console.WriteLine("What is the station ID ?");
                                    int.TryParse(Console.ReadLine(), out ID);
                                    Station station = DalObject.DalObject.StationById(ID);
                                    Console.WriteLine($"The distance is: {Math.Round(DAL.Coordinates.distance(latitude, longitude, station.Latitude, station.Longitude), 3)}");
                                    break;

                                default:
                                    break;
                            }

                            break;
                        }

                    case Menu.Exit:
                        num = 0;
                        break;

                    default:
                        break;

                }
            }
        }

        static void Main(string[] args)
        {
            new DalObject.DalObject();
            Display();
        }

    }
}*/
