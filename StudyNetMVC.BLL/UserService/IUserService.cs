using StudyNetMVC.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyNetMVC.BLL.UserService
{
    public interface IUserService
    {
        bool createUser(string email,string phone,string password);

        List<User> getAllUserInfo(string username,string phone);

        int delUseres(string ids);

        bool EditUser(string id, string username, string email, string phone, string pass);
    }
}
