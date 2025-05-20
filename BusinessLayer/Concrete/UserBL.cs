using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace BusinessLayer.Concrete
{
    public class UserBL : IUserService
    {
        IUserDal userDal;
        
        public UserBL(IUserDal adminDal)
        {
            this.userDal = adminDal;
        }
        public User GetUserBl(string username, string password)
        {
            return userDal.Get(x => x.UserName == username && x.UserPassword == password);
        }

        public User GetUserBl(string username)
        {
            return userDal.Get(x => x.UserName == username);
        }

        public void AddUser(User user)
        {
            userDal.Insert(user);
        }

    }
}
