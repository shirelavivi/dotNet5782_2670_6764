﻿using System;
using IBL.BO;
namespace ConsoleUI_BL
{
    class Program
    {
        
        public enum Options { Addition, Update, Display, ShowLists, Exit }
        public enum Addition { AddStation, AddDrone, AddCustomer, AddParcel, Exit }
        public enum UpDate { UpDateDrone, UpDateStation, UpDateCustome, SendingDroneforCharging, ReleaseDronefromCharging, ParcelToDrone, CollectionParcelByDrone, SupplyParcelByDrone, Exit }
        public enum Show { Station, Drone, Customer, Parcel, Exit }
        public enum ShowList { Station, Drone, Customer, Parcel, ParcelFreeDrone, StationFree, Exit }


        static void Main(string[] args)
        {
            Customer GetingCustomer()
            {
                IBL.BO.Customer customer = new IBL.BO.Customer();
                IBL.BO.Location location = new IBL.BO.Location();
                int customerId;
                double d;
                Console.WriteLine("Type a customer ID:");
                int.TryParse(Console.ReadLine(), out customerId);
                customer.Id = customerId;
                Console.WriteLine("Type a customer name:");
                customer.Name = Console.ReadLine();
                Console.WriteLine("Type a customer phone:");
                customer.Phone = Console.ReadLine();
                Console.WriteLine("Type a Lattitude:");
                double.TryParse(Console.ReadLine(), out d);
                location.Lattitude = d;
                Console.WriteLine("Type a  longitude:");
                double.TryParse(Console.ReadLine(), out d);
                location.Longitude = d;
                customer.location = location;
                return customer;
            }//פונקציה שקולטת לקוח ומחזירה עצם של BL מטיפוס לקוח
            Options op;
            Console.WriteLine("Please press the one of the options you want:");
            Console.WriteLine(" 0: Addition \n 1: Update \n 2: Display \n 3: ShowLists \n 4: Exit");
            op = (Options)int.Parse(Console.ReadLine());    //User input to go through the options
            IBL.BO.BL bl = new IBL.BO.BL();
            int num = 1;
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
                                
                                case Addition.AddCustomer:
                                    {
                                      bl.AddCustomer(GetingCustomer());

                                    }
                                    break;
                                case Addition.AddParcel:
                                    {
                                        IBL.BO.Parcel parcel = new IBL.BO.Parcel();
                                        int customerId;
                                        Console.WriteLine("Type Send Customer ID:");
                                        int.TryParse(Console.ReadLine(), out customerId);
                                        parcel.Sender.Id = customerId;
                                        Console.WriteLine("Type a Send customer name:");
                                        parcel.Sender.Name = Console.ReadLine();
                                        Console.WriteLine("Type Receiving Customer ID:");
                                        int.TryParse(Console.ReadLine(), out customerId);
                                        parcel.Target.Id = customerId;
                                        Console.WriteLine("Type a Receiving customer name:");
                                        parcel.Target.Name = Console.ReadLine();
                                        Console.WriteLine("Choose package Weight: 0 : easy,  1 : middle,  2 : weighty:");
                                        Weightcategories parcelWeigh;
                                        Weightcategories.TryParse(Console.ReadLine(), out parcelWeigh);
                                        parcel.Weight = parcelWeigh;
                                        Console.WriteLine("Choose package Priority: 0 : Standard , 1: fast , 2:emergency:");
                                        Priorities parcelPriority;
                                        Priorities.TryParse(Console.ReadLine(), out parcelPriority);
                                        parcel.Priority = parcelPriority;
                                        bl.AddrParcel(parcel); ; // Adding the new object to the list of that object

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
                            Console.WriteLine("0: UpDateDrone \n 1:UpDateStation\n 2: UpDateCustome\n 3: SendingDroneforCharging\n 4: ReleaseDronefromCharging\n 5: ParcelToDrone\n 6:CollectionParcelByDrone\n 7:SupplyParcelByDrone \n 8:Exsit");
                            up = (UpDate)int.Parse(Console.ReadLine());
                            switch (up)
                            {
                                case UpDate.UpDateDrone:
                                    {
                                        Console.WriteLine("Type drone ID :");
                                        Console.WriteLine("Type drone model :");
                                        int droneID;
                                        string model;
                                        int.TryParse(Console.ReadLine(), out droneID);
                                        model= Console.ReadLine();
                                        //........

                                    }
                                    break;

                                case UpDate.UpDateStation:
                                    {
                                      
                                        Console.WriteLine("Type ststion ID:");
                                        int stationId, numberChargSlout;
                                        string nameStation;
                                        int.TryParse(Console.ReadLine(), out stationId);
                                        Console.WriteLine("Type a station name :");
                                        nameStation = Console.ReadLine();
                                        Console.WriteLine("Type a number of charg slouts:");
                                        int.TryParse(Console.ReadLine(), out numberChargSlout);
                                        bl.UpdStation(stationId, nameStation, numberChargSlout);

                                    }
                                    break;
                                case UpDate.SendingDroneforCharging:
                                    {
                                        int droneId;
                                        int.TryParse(Console.ReadLine(), out droneId);
                                        bl.SendingDroneforCharging(droneId);
                                    }
                                    break;
                                case UpDate.ParcelToDrone:
                                    {
                                        int droneId;
                                        int.TryParse(Console.ReadLine(), out droneId);
                                        bl.ConnectParcelToDrone(droneId);
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
    }
}