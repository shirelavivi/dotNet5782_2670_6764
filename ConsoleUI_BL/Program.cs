using System;
using IBL.BO;
namespace ConsoleUI_BL
{
    class Program
    {
        
        public enum Options { Addition, Update, Display, ShowLists, Exit }
        public enum Addition { AddStation, AddDrone, AddCustomer, AddParcel, Exit }
        public enum UpDate { UpDateDrone, UpDateStation, UpDateCustome, SendingDroneforCharging, ReleaseDronefromCharging, ParcelToDrone, CollectionParcelByDrone, SupplyParcelByDrone }
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
                customer.
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
                                        
                                        Console.WriteLine("Type Send Customer ID:");
                                        parcel.Sender = GetingCustomer();
                                        Console.WriteLine("Type Receiving customer ID:");
                                        parcel.Target = GetingCustomer();
                                        Console.WriteLine("Choose package Weight: 0 : easy,  1 : middle,  2 : weighty:");
                                        Weightcategories parcelWeigh;
                                        Weightcategories.TryParse(Console.ReadLine(), out parcelWeigh);
                                        parcel.Weight = parcelWeigh;
                                        Console.WriteLine("Choose package Priority: 0 : Standard , 1: fast , 2:emergency:");
                                        Priorities parcelPriority;
                                        Priorities.TryParse(Console.ReadLine(), out parcelPriority);
                                        parcel.Priority = parcelPriority;
                                        dl.AddParcel(parcel); ; // Adding the new object to the list of that object

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


                        Console.WriteLine("Hello World!");
                }
            }
        }
    }
}
