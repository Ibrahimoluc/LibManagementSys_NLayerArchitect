using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IUserService
    {
        User GetUserBl(string username, string password);

        User GetUserBl(string username);

        void AddUser(User user);
    }
}
