using StudyNetMVC.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyNetMVC.BLL
{
    public class Exec
    {

        /// <summary>
        /// 编辑用户
        /// </summary>
        /// <param name="id"></param>
        /// <param name="username"></param>
        /// <param name="email"></param>
        /// <param name="phone"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        public static int EditUser(string id, string username, string email, string phone, string pass)
        {
            String sql = "update UserAccountInfo set username=N'"+username+"',email=N'"+email+"',password=N'"+pass+"',phoneNumber=N'"+phone+"' where Id="+id;
            int res = SqlHelper.ExecuteNonQuery(SqlHelper.GetConnSting(), CommandType.Text, sql);
            return res;
        }


        /// <summary>
        /// 根据id删除多条数据
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public static int delUsers(string ids)
        {
            String sql = "delete from UserAccountInfo where Id in("+ids+")";
            int res = SqlHelper.ExecuteNonQuery(SqlHelper.GetConnSting(), CommandType.Text, sql);
            return res;
        }



        /// <summary>
        /// 查询所有的用户数据
        /// </summary>
        /// <returns></returns>
        public static DataSet QueryAllUser()
        {
            string sql = "select *  from UserAccountInfo ";
            DataSet ds = SqlHelper.ExecuteDataset(SqlHelper.GetConnSting(), CommandType.Text, sql);
            return ds;
        }
        /// <summary>
        /// 根据email和password查询账户，存在则返回true
        /// </summary>
        /// <param name="email"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        public static bool QueryUserAccountInfoByEmailAndPass(string email,string pass)
        {
            string sql = "select count(*) as userNumber from UserAccountInfo where email = N'"+email+"' and password = N'"+pass+"'";
            DataSet ds = SqlHelper.ExecuteDataset(SqlHelper.GetConnSting(), CommandType.Text, sql);
            int res =  (int)ds.Tables[0].Rows[0]["userNumber"];
            if (res >= 1) return true;
            else return false;
        }

        /// <summary>
        /// 根据用户名查询数据
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        internal static DataSet QueryUserByUserNameAndPhone(string username,string phone)
        {
            string sql = "select * from UserAccountInfo where username like N'%"+username+"%'and phoneNumber like N'%"+phone+"%'";
            DataSet ds = SqlHelper.ExecuteDataset(SqlHelper.GetConnSting(), CommandType.Text, sql);
            return ds;
        }


        /// <summary>
        /// 根据phone和password查询账户，存在则返回true
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        public static bool QueryUserAccountInfoByPhoneAndPass(string phone, string pass)
        {
            string sql = "select count(*) as userNumber from UserAccountInfo where phoneNumber = N'" + phone + "' and password = N'" + pass + "'";
            DataSet ds = SqlHelper.ExecuteDataset(SqlHelper.GetConnSting(), CommandType.Text, sql);
            int res = (int)ds.Tables[0].Rows[0]["userNumber"];
            if (res >= 1) return true;
            else return false;
        }

        /// <summary>
        /// 向用户表插入一条信息
        /// </summary>
        /// <param name="email"></param>
        /// <param name="phone"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        public  static  bool InsertUserAccount(string username,string email,string phone, string pass,string createDate)
        {
            string sql = "insert into UserAccountInfo(username,email,phoneNumber,password,createDate) values(N'"+username+"',N'"+email+"',N'"+phone+"',N'"+pass+"',N'"+createDate+"')";
            int res = SqlHelper.ExecuteNonQuery(SqlHelper.GetConnSting(), CommandType.Text, sql);
            if (res >= 1) return true;
            else return false;
        }

    }
}
