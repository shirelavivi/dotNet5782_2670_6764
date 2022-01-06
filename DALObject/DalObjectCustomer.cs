using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DAL.DataSource;
using DO;

namespace Dal
{
    sealed partial class DalObject : Idal
    {
       
        public void AddCustomer(Customer c)
        {

            if (CheckCusromer(c.Id))

                throw new DO.DuplicateIdException(c.Id, "Customer");


            DAL.DataSource.customers.Add(c);

        }

        public bool CheckCusromer(int id)
        {
            return DAL.DataSource.customers.Any(st => st.Id == id);
        }

        public IEnumerable<Customer> GetALLCustomers()
        {
            return from st in DAL.DataSource.customers select st;
            //return DataSource.Customers;
        }



        public void UpdCustomer(Customer st)
        {
            int count = DAL.DataSource.customers.RemoveAll(st => st.Id == st.Id);

            if (count == 0)
                throw new MissingIdException(st.Id, "Customer");

            DAL.DataSource.customers.Add(st);
        }

        public void DelCustomer(int id)
        {
            int count = DAL.DataSource.customers.RemoveAll(st => st.Id == id);

            if (count == 0)
                throw new MissingIdException(id, "Customer");
        }

        public IEnumerable<Customer> GetCustomertsByPerdicate(Predicate<Customer> predicate)
        {
            return from st in DAL.DataSource.customers
                   where predicate(st)
                   select st;
        }
        public Customer GetCustomer(int s)
        {
            if (!CheckCusromer(s))
                throw new MissingIdException(s, "Customer");

            Customer st = DAL.DataSource.customers.Find(st => st.Id == s);
            return st;

        }
        public IEnumerable<Customer> ShowCustomerList()//כפילות בפונציה יש אותה פעמיים בשמות שונים 
        {

            List<Customer> temp = new List<Customer>();
            foreach (Customer item in DAL.DataSource.customers)
            {
                temp.Add(item);
            }

            return temp;
        }
    }
}
