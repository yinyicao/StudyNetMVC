
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudyNetMVC.Entity;

namespace StudyNetMVC.BLL.UserService.UserServiceImpl
{
    public class UserService : IUserService
    {
        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="email"></param>
        /// <param name="phone"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool createUser(string email, string phone, string password)
        {
            //生成一个用户名
            string username = "User_"+Utils.StringHelper.getRandomString();
            string dateTime = DateTime.Now.ToString("g");

            return Exec.InsertUserAccount(username, email, phone, password,dateTime);
        }

        /// <summary>
        /// 删除指定id的数据
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int delUseres(string ids)
        {
            return Exec.delUsers(ids);
        }

        /// <summary>
        /// 编辑用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="username"></param>
        /// <param name="email"></param>
        /// <param name="phone"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        public bool EditUser(string id, string username, string email, string phone, string pass)
        {
           int res =  Exec.EditUser(id, username, email, phone, pass);
            if (res >= 1) return true;
            else return false;
        }

        /// <summary>
        /// 获取所有信息
        /// </summary>
        /// <returns></returns>
        public List<User> getAllUserInfo(string username,string phone)
        {
           if("".Equals(username) && "".Equals(phone)) {
                DataSet ds = Exec.QueryAllUser();
                DataTable dt = ds.Tables[0];
                return Utils.DataTableHelper.ToListEntity<User>(dt);
            }
            else
            {
                DataSet ds = Exec.QueryUserByUserNameAndPhone(username,phone);
                DataTable dt = ds.Tables[0];
                return Utils.DataTableHelper.ToListEntity<User>(dt);
            }
           
        }
    }
}
