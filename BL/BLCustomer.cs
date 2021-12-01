

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
    } 
}
