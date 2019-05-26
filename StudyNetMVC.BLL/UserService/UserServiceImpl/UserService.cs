
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
            string username = "User_" + Utils.StringHelper.getRandomString();
            string md5pass = Utils.EncryptUtil.Md532(password, username);
            string dateTime = DateTime.Now.ToString("g");
            //username不存在即可添加
            return Exec.QueryUserNameByUserName(username) == null ?
                Exec.InsertUserAccount(username, email, phone, md5pass, dateTime) : false;


        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="email"></param>
        /// <param name="phone"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool createUser(string username, string email, string phone, string password)
        {
            username = (username == null || "".Equals(username)) ? "User_" + Utils.StringHelper.getRandomString() : username;
            string md5pass = Utils.EncryptUtil.Md532(password, username);
            string dateTime = DateTime.Now.ToString("g");
            //username不存在即可添加
            return Exec.QueryUserNameByUserName(username) == null ?
                Exec.InsertUserAccount(username, email, phone, md5pass, dateTime):false;
        }

        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="loginName">登录名，可能是email，也可能是phone</param>
        /// <param name="pass"></param>
        /// <param name="loginType"></param>
        /// <returns></returns>
        public bool checkLogin(string loginName, string pass, string loginType)
        {
            bool res = false;
            string username;
            string md5pass;
            switch (loginType)
            {
                case "email":
                    username = Exec.QueryUserNameByEmail(loginName);//查询用户名，用来当作加密盐值
                    if (username == null) break;
                    md5pass = Utils.EncryptUtil.Md532(pass,username);
                    res = Exec.QueryUserAccountInfoByEmailAndPass(loginName, md5pass);
                    break;
                case "phone":
                    username = Exec.QueryUserNameByEmail(loginName);
                    if (username == null) break;
                    md5pass = Utils.EncryptUtil.Md532(pass, username);
                    res = Exec.QueryUserAccountInfoByPhoneAndPass(loginName, md5pass);
                    break;
                default:
                    res = false;
                    break;
            }

            return res;
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
            string md5pass = Utils.EncryptUtil.Md532(pass,username);
            //username不存在即可添加
            return Exec.QueryUserNameByUserName(username) == null ?
             (Exec.EditUser(id, username, email, phone, md5pass)>=1?true:false):false;

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

        public bool modifyPass(string username, string newPass, string newPass2) {

            //根据登录名（email或phone）查找用户名
          username =Exec.QueryUserNameByEmail(username);
          if (username == null) username = Exec.QueryUserNameByPhone(username);
          if (Exec.QueryUserNameByUserName(username) == null || newPass == null || !newPass.Equals(newPass2)){
              return false;
          }else{
              string md5pass = Utils.EncryptUtil.Md532(newPass, username);
              return Exec.modifyPass(username, md5pass);
          }
        }
    }
}
