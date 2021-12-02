

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
            public void AddCustomer(BO.Customer customer)
            {
                try
                {

                    IDAL.DO.Customer customer_do = new IDAL.DO.Customer();
                    customer_do.Id = customer.Id;
                    customer_do.Name = customer.Name;
                    customer_do.Name = customer.Phone;
                    customer_do.Lattitude = customer.location.Lattitude;
                    customer_do.Longitude = customer.location.Longitude;
                    dl.AddCustomer(customer_do);
                }
                catch (IDAL.DO.DuplicateIdException ex)
                {
                    throw new DuplicateIdException(ex.ID, ex.EntityName);
                }
            }
            public void UpdCustomer(int idCustomer, string nameCustomer = "", string newNumPhone = "")
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
                        parcelfromCustomer = (from p in dalObject.GetPackages()
                                                where p.SenderId == id
                                                select getPackageInCustomer(p, p.TargetId)).ToList(),
                        parcelsToCustomer = (from p in dalObject.GetPackages()
                                              where p.TargetId == id
                                              select getPackageInCustomer(p, p.SenderId)).ToList()
                    };
                    return myCustomer;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }

            //private ParcelAtCustomer getPackageInCustomer(IDAL.DO.Parcel p, int customerId)//לעשות
            //{
            //    try
            //    {
            //        return new ParcelAtCustomer
            //        {
            //            Idnumber = p.Id,
            //            Weightcategories = (Weightcategories)p.Weight,
            //            Priorities = (Priorities)p.Priority,
            //            PacketStatuses = (p.Scheduled == default(DateTime) ? PackageStatuses.REQUESTED :
            //                                                p.PickedUp == default(DateTime) ? PackageStatuses.SCHEDULED :
            //                                                p.Delivered == default(DateTime) ? PackageStatuses.PICKEDUP :
            //                                                PackageStatuses.DELIVERED),
            //            CustomerAtParcels = new CustomerInPackage
            //            {
            //                Id = customerId,
            //                CustomerName = dl.GetCustomer(customerId).Name
            //            }
            //        };
            //    }
            //    catch (Exception e)
            //    {
            //        throw e;
            //    }
            //}
            public IEnumerable<BO.CustomerToList> GetALLCostumerToList()
            {
                CustomerToList customer = new CustomerToList();
                foreach (IDAL.DO.Customer item in dl.GetALLCustomers())//מיוי הנתונים ב BL מתוך DAL
                {
                    customer.Id = item.Id;
                    customer.Name = item.Name;
                    customer.Phone = item.Phone;
                    customer.numOfParcelDontProvided = ;// איך אני ממלא אותם
                    customer.numOfParcelGet = ;
                    customer.numOfParcelOnTheWay =;
                    customer.numOfParcelProvided =;
                    customerBl.Add(customer);
                }
                return customerBl;
            }

        }
      


            }
        }
    } 
}
