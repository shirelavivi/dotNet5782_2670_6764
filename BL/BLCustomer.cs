

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
                catch (IDAL.DO.MissingIdException ex)//חריגה לא טובה !!!!!!!!!
                {
                    throw new MissingIdException(ex.ID, ex.EntityName);
                }
            }
            public IEnumerable<BO.CustomerToList> GetALLCostumerToList()
            {
                CustomerToList customer = new CustomerToList();
                foreach (IDAL.DO.Customer item in dl.GetALLCustomers())//מיוי הנתונים ב BL מתוך DAL
                {
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
                            if (itemParcel.Delivered != default(DateTime))//החבילה כבר סופקה
                                customer.numOfParcelProvided++;
                            else
                                 if (itemParcel.PickedUp != default(DateTime))//החבילה באמצע משלוח
                                customer.numOfParcelDontProvided++;
                        }
                        if (itemParcel.TargetId == item.Id)//חבילה שהתקבלה על ידי הלקוח
                        {
                            if (itemParcel.Delivered != default(DateTime))//החבילה כבר סופקה לו
                                customer.numOfParcelGet++;
                            else
                                 if (itemParcel.PickedUp != default(DateTime))// אליו החבילה באמצע משלוח
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
