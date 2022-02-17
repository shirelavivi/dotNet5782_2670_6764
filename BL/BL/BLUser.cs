using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using BlApi;
using DO;


namespace BL
{
    sealed partial class BL : IBL
    {
        public void AddUser(BO.User user)
        {
            try
            {
                DO.User newuser = new DO.User();
                newuser.Name = user.Name;
                newuser.PassWord = user.PassWord;
                dal.AddUser(newuser);

            }
            catch (DO.DuplicateIdException ex)
            {
                throw new BO.DuplicateIdException(ex.ID, ex.EntityName);
            }
        }
        public BO.User GetUser(int pass)
        {
            if (pass < 0)
                throw new BO.MissingIdException(pass,"This user not exsit");

            try
            {
                BO.User user = new BO.User();
                user.Name= dal.GetUser(pass).Name;
                user.PassWord = dal.GetUser(pass).PassWord;
                return user;
            }
            catch (DO.MissingIdException ex)
            {
                throw new BO.MissingIdException(ex.ID, ex.EntityName);
            }
            catch (DO.DuplicateIdException ex)
            {
                throw new BO.DuplicateIdException(ex.ID, ex.EntityName);
            }
        }
        public IEnumerable<BO.User> GetALLusers()
        {
           
                List<BO.User> users = new List<BO.User>();
                foreach (DO.User item in dal.GetALLUsers())//מיוי הנתונים ב BL מתוך DAL
                {
                    BO.User user = new BO.User();
                    user.Name = item.Name;
                    user.PassWord = item.PassWord;

                    users.Add(user);
                }
                return users;
           
          
        }
    }
}
