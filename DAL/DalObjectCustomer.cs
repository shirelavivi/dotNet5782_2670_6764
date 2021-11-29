using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IDAL.DO;
namespace DalObject
{

    public partial class DalObject : Idal
    {
        public Customer GetCustomer(int id)
        {
            if (!CheckCusromer(id))
                throw new MissingIdException(id, "Customer");

            Customer st = IDAL.DataSource.customers.Find(st => st.Id == id);
            return st;
        }
        public void AddCustomer(Customer c)
        {

            if (CheckCusromer(c.Id))

                throw new IDAL.DO.DuplicateIdException(c.Id, "Customer");


            IDAL.DataSource.customers.Add(c);

        }

        public bool CheckCusromer(int id)
        {
            return IDAL.DataSource.customers.Any(st => st.Id == id);
        }

        public IEnumerable<Customer> GetALLCustomers()
        {
            return from st in IDAL.DataSource.customers select st;
            //return DataSource.Customers;
        }



        public void UpdCustomer(Customer st)
        {
            int count = IDAL.DataSource.customers.RemoveAll(st => st.Id == st.Id);

            if (count == 0)
                throw new MissingIdException(st.Id, "Student");

            IDAL.DataSource.customers.Add(st);
        }

        public void DelCustomer(int id)
        {
            int count = IDAL.DataSource.customers.RemoveAll(st => st.Id == id);

            if (count == 0)
                throw new MissingIdException(id, "Student");
        }

        public IEnumerable<Customer> GetCustomertsByPerdicate(Predicate<Customer> predicate)
        {
            return from st in IDAL.DataSource.customers
                   where predicate(st)
                   select st;
        }
        public Customer ShowCustomer(int s)
        {
            List<Customer> run = IDAL.DataSource.customers;
            Customer temp = new Customer();
            for (int i = 0; i < run.Count; i++)
            {
                if (run[i].Id == s)
                {
                    temp = run[i];
                }
            }
            return temp;

        }
        public IEnumerable<Customer> ShowCustomerList()//כפילות בפונציה יש אותה פעמיים בשמות שונים 
        {

            List<Customer> temp = new List<Customer>();
            foreach (Customer item in IDAL.DataSource.customers)
            {
                temp.Add(item);
            }

            return temp;
        }
    }
}
