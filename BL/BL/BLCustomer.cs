using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using BlApi;
using DalApi;
using DO;

namespace BL
{
    sealed partial class BL : IBL
    {
        public void AddCustomer(BO.Customer customer)//הוספת לקוח לשכבת הנתונים
        {
            try
            {

                DO.Customer customer_do = new DO.Customer();
                customer_do.Id = customer.Id;
                customer_do.Name = customer.Name;
                customer_do.Phone = customer.Phone;
                customer_do.Lattitude = customer.location.Lattitude;
                customer_do.Longitude = customer.location.Longitude;
                dal.AddCustomer(customer_do);
            }
            catch (DO.DuplicateIdException ex)
            {
                throw new BO.DuplicateIdException(ex.ID, ex.EntityName);
            }
        }
        public void UpdCustomer(int idCustomer, string nameCustomer = "", string newNumPhone = "")//עידכון לקוח
        {
            try
            {
                DO.Customer customer = dal.GetCustomer(idCustomer);
                customer.Id = idCustomer;

                if (nameCustomer != "")
                {
                    customer.Name = nameCustomer;
                }

                if (newNumPhone != "")
                {
                    customer.Phone = newNumPhone;
                }

                dal.DelCustomer(idCustomer);
                dal.AddCustomer(customer);
            }
            catch (DO.MissingIdException ex)
            {
                throw new BO.MissingIdException(ex.ID, ex.EntityName);
            }
        }
        public BO.Customer GetCustomer(int id)
        {
            try
            {
                var customer = dal.GetCustomer(id);
                BO.Customer myCustomer = new BO.Customer
                {
                    Id = id,
                    Name = customer.Name,
                    Phone = customer.Phone,
                    location = new Location
                    {
                        Lattitude = customer.Lattitude,
                        Longitude = customer.Longitude
                    },
                    parcelfromCustomer = (from p in dal.GetALLParcel()
                                          where p.SenderId == id
                                          select getPackageInCustomer(p, p.TargetId)).ToList(),
                    parcelsToCustomer = (from p in dal.GetALLParcel()
                                         where p.TargetId == id
                                         select getPackageInCustomer(p, p.SenderId)).ToList()
                };
                return myCustomer;
            }
            catch (DO.MissingIdException ex)
            {
                throw new BO.MissingIdException(ex.ID, ex.EntityName);
            }
        }

        private ParcelAtCustomer getPackageInCustomer(DO.Parcel p, int customerId)//פונקצית עזר למילוי חבילה בלקוח
        {
            try
            {
                return new ParcelAtCustomer
                {
                    Idnumber = p.Id,
                    Weightcategories = (BO.Weightcategories)p.Weight,
                    Priorities = (BO.Priorities)p.Priority,
                    PacketStatuses = (p.Scheduled == default(DateTime) ? PacketStatuses.Created :
                                                        p.PickedUp == default(DateTime) ? PacketStatuses.associated :
                                                        p.Delivered == default(DateTime) ? PacketStatuses.collected :
                                                        PacketStatuses.provided),
                    CustomerInPackage = new CustomerAtParcels
                    {
                        Id = customerId,
                        Name = dal.GetCustomer(customerId).Name
                    }
                };
            }
            catch (DO.MissingIdException ex)
            {
                throw new BO.MissingIdException(ex.ID, ex.EntityName);
            }
        }
        public IEnumerable<BO.CustomerToList> GetALLCostumerToList()//הצגת רשימת הלקוחות
        {
            List<CustomerToList> customerBl = new List<CustomerToList>();

            foreach (DO.Customer item in dal.GetALLCustomers())//מיוי הנתונים ב BL מתוך DAL
            {
                CustomerToList customer = new CustomerToList();
                customer.Id = item.Id;
                customer.Name = item.Name;
                customer.Phone = item.Phone;
                customer.numOfParcelDontProvided = 0;//מספר החבילות שלא סופקו
                customer.numOfParcelOnTheWay = 0;//מספר החבילות במשלוח
                customer.numOfParcelGet = 0;//מספר החבילות שהתקבלו
                customer.numOfParcelProvided = 0;//מספר החבילות שסופקו
                foreach (DO.Parcel itemParcel in dal.GetALLParcel())
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
