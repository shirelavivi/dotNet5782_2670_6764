using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using DalApi;
using Dal;
using static Dal.DataSource;



namespace Dal
{
    sealed partial class DalObject : IDal
    {

        public void AddUser(User c)
        {

            if (CheckUser(c.PassWord))

                throw new DO.DuplicateIdException(c.PassWord, "Pass Word is exsit");


            Dal.DataSource.Users.Add(c);

        }
        public bool CheckUser(int passWord)
        {
            return Dal.DataSource.Users.Any(st => st.PassWord == passWord);
        }

        
        public User GetUser(int s)
        {
            if (!CheckUser(s))
                throw new MissingIdException(s, "User");

            User st = Dal.DataSource.Users.Find(st => st.PassWord == s);
            return st;

        }
        public IEnumerable<User> GetALLUsers()
        {

            return from st in Dal.DataSource.Users select st;
        }
    }
}

