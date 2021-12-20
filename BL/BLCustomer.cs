

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL;

namespace IBL
{
    namespace BO
    {
        public partial class BL : IBL
        {
            public void AddCustomer(BO.Customer customer)//הוספת לקוח לשכבת הנתונים
            {
                try
                {

                    IDAL.DO.Customer customer_do = new IDAL.DO.Customer();
                    customer_do.Id = customer.Id;
                    customer_do.Name = customer.Name;
                    customer_do.Phone = customer.Phone;
                    customer_do.Lattitude = customer.location.Lattitude;
                    customer_do.Longitude = customer.location.Longitude;
                    dl.AddCustomer(customer_do);
                }
                catch (IDAL.DO.DuplicateIdException ex)
                {
                    throw new DuplicateIdException(ex.ID, ex.EntityName);
                }
            }
            public void UpdCustomer(int idCustomer, string nameCustomer = "", string newNumPhone = "")//עידכון לקוח
            {
                try
                {
                    IDAL.DO.Customer customer = dl.GetCustomer(idCustomer);
                    customer.Id = idCustomer;

                    if (nameCustomer != "")
                    {
                        customer.Name = nameCustomer;
                    }

                    if (newNumPhone != "")
                    {
                        customer.Phone = newNumPhone;
                    }

                    dl.DelCustomer(idCustomer);
                    dl.AddCustomer(customer);
                }
                catch (IDAL.DO.MissingIdException ex)
                {
                    throw new MissingIdException(ex.ID, ex.EntityName);
                }
            }
            public Customer GetCustomer(int id)
            {
                try
                {
                    var customer = dl.GetCustomer(id);
                    Customer myCustomer = new Customer
                    {
                        Id = id,
                        Name = customer.Name,
                        Phone = customer.Phone,
                        location = new Location
                        {
                            Lattitude = customer.Lattitude,
                            Longitude = customer.Longitude
                        },
                        parcelfromCustomer = (from p in dl.GetALLParcel()
                                                where p.SenderId == id
                                                select getPackageInCustomer(p, p.TargetId)).ToList(),
                        parcelsToCustomer = (from p in dl.GetALLParcel()
                                              where p.TargetId == id
                                              select getPackageInCustomer(p, p.SenderId)).ToList()
                    };
                    return myCustomer;
                }
                catch (IDAL.DO.MissingIdException ex)
                {
                    throw new MissingIdException(ex.ID, ex.EntityName);
                }
            }

            private ParcelAtCustomer getPackageInCustomer(IDAL.DO.Parcel p, int customerId)//פונקצית עזר למילוי חבילה בלקוח
            {
                try
                {
                    return new ParcelAtCustomer
                    {
                        Idnumber = p.Id,
                        Weightcategories = (Weightcategories)p.Weight,
                        Priorities = (Priorities)p.Priority,
                        PacketStatuses = (p.Scheduled == default(DateTime) ? PacketStatuses.Created :
                                                            p.PickedUp == default(DateTime) ? PacketStatuses.associated:
                                                            p.Delivered == default(DateTime) ? PacketStatuses.collected :
                                                            PacketStatuses.provided),
                        CustomerInPackage = new CustomerAtParcels
                        {
                            Id = customerId,
                            Name = dl.GetCustomer(customerId).Name
                        }
                    };
                }
                catch (IDAL.DO.MissingIdException ex)
                {
                    throw new MissingIdException(ex.ID, ex.EntityName);
                }
            }
            public IEnumerable<BO.CustomerToList> GetALLCostumerToList()//הצגת רשימת הלקוחות
            {
                List<CustomerToList> customerBl = new List<CustomerToList>();
               
                foreach (IDAL.DO.Customer item in dl.GetALLCustomers())//מיוי הנתונים ב BL מתוך DAL
                {
                    CustomerToList customer = new CustomerToList();
                    customer.Id = item.Id;
                    customer.Name = item.Name;
                    customer.Phone = item.Phone;
                    customer.numOfParcelDontProvided =0 ;//מספר החבילות שלא סופקו
                    customer.numOfParcelOnTheWay =0;//מספר החבילות במשלוח
                    customer.numOfParcelGet = 0;//מספר החבילות שהתקבלו
                    customer.numOfParcelProvided =0;//מספר החבילות שסופקו
                    foreach (IDAL.DO.Parcel itemParcel in dl.GetALLParcel() )
                    {
                        if (itemParcel.SenderId == item.Id)//אם זאת חבילה שנשלחה עי הלקוח 
                        { 
                            if (itemParcel.Delivered != new DateTime())//החבילה כבר סופקה
                                customer.numOfParcelProvided++;
                            else
                                 if (itemParcel.PickedUp != new DateTime())//החבילה באמצע משלוח
                                customer.numOfParcelDontProvided++;
                        }
                        if (itemParcel.TargetId == item.Id)//חבילה שהתקבלה על ידי הלקוח
                        {
                            if (itemParcel.Delivered != new DateTime())//החבילה כבר סופקה לו
                                customer.numOfParcelGet++;
                            else
                                 if (itemParcel.PickedUp != new DateTime())// אליו החבילה באמצע משלוח
                                customer.numOfParcelOnTheWay++;
                        }
                    }
        
                    customerBl.Add(customer);
                }
                return customerBl;
            }

        }      
    } 
}
