using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dal.DataSource;
using DO;
using DalApi;



namespace Dal
{
    sealed partial class DalObject : IDal
    {
       
        public void AddCustomer(Customer c)
        {

            if (CheckCusromer(c.Id))

                throw new DO.DuplicateIdException(c.Id, "Customer");


            Dal.DataSource.customers.Add(c);

        }

        public bool CheckCusromer(int id)
        {
            return Dal.DataSource.customers.Any(st => st.Id == id);
        }

        public IEnumerable<Customer> GetALLCustomers()
        {
            return from st in Dal.DataSource.customers select st;
            //return DataSource.Customers;
        }



        public void UpdCustomer(Customer st)
        {
            int count = Dal.DataSource.customers.RemoveAll(st => st.Id == st.Id);

            if (count == 0)
                throw new MissingIdException(st.Id, "Customer");

            Dal.DataSource.customers.Add(st);
        }

        public void DelCustomer(int id)
        {
            int count = Dal.DataSource.customers.RemoveAll(st => st.Id == id);

            if (count == 0)
                throw new MissingIdException(id, "Customer");
        }

        public IEnumerable<Customer> GetCustomertsByPerdicate(Predicate<Customer> predicate)
        {
            return from st in Dal.DataSource.customers
                   where predicate(st)
                   select st;
        }
        public Customer GetCustomer(int s)
        {
            if (!CheckCusromer(s))
                throw new MissingIdException(s, "Customer");

            Customer st = Dal.DataSource.customers.Find(st => st.Id == s);
            return st;

        }

    }
}
