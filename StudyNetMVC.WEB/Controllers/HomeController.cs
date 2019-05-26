using StudyNetMVC.BLL.UserService;
using StudyNetMVC.BLL.UserService.UserServiceImpl;
using StudyNetMVC.Entity;
using StudyNetMVC.WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudyNetMVC.WEB.Controllers
{
    /// <summary>
    /// 控制器，在FilterConfig中定义全局过滤器，过滤本控制器所有方法
    /// 在方法上加上[MyActionFilter(IsLogin = true)]表示本方法不过滤
    /// </summary>
    public class HomeController : Controller
    {
        IUserService userService = null;
        public HomeController()
        {
            userService = new UserService();
        }

        /// <summary>
        /// 登录方法-不需要过滤
        /// </summary>
        /// <param name="username"></param>
        /// <param name="pass"></param>
        /// <param name="loginType"></param>
        /// <returns></returns>
        [MyActionFilter(IsLogin = true)]
        public bool Login(string username, string pass,string loginType)
        {
            bool res = userService.checkLogin(username, pass, loginType);
            if (res)
            {
                Session["UserName"] = username;
            }

            return res;
           
        }

        public ActionResult ModifyPass()
        {

            return View();

        }

        public bool ModifyPassAPI(string newPass, string newPass2)
        {
            string username = Session["UserName"].ToString();
            return userService.modifyPass(username , newPass, newPass2)? Logout():false;

        }



        /// <summary>
        /// 登出方法-可以过滤也可以不过滤
        /// </summary>
        /// <returns></returns>
        public bool Logout()
        {
            Session["UserName"] = "";
            return true;
        }

        public bool EditUser(string id,string username,string email,string phone,string pass)
        {
            return userService.EditUser( id,  username,  email,  phone,  pass);
        }


        /// <summary>
        /// 注册页-不需要过滤
        /// </summary>
        /// <returns></returns>
        [MyActionFilter(IsLogin = true)]
        public bool Register(string email,string phone,string pass)
        {
            return userService.createUser(email,phone,pass);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="email"></param>
        /// <param name="phone"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        public bool Add(string username,string email, string phone, string pass)
        {
            return userService.createUser(username,email, phone, pass);
        }

        /// <summary>
        /// 登录页-不需要过滤
        /// </summary>
        /// <returns></returns>
        [MyActionFilter(IsLogin = true)]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 主页-需要过滤
        /// </summary>
        /// <returns></returns>
        public ActionResult Main()
        {
            return View();
        }

        [MyActionFilter(IsLogin = true)]
        public ActionResult Contact()
        {
            ViewBag.Name = "尹以操";
            ViewBag.ID = "91621380217";

            return View();
        }

        [HttpPost]
        public int Delete(string ids)
        {
            return userService.delUseres(ids);
        }

        public JsonResult GetAllUserInfo(int limit, int offset, string username, string phone)
        {          
            List<User> users = userService.getAllUserInfo(username,phone);
            var total = users.Count;
            var rows = users.Skip(offset).Take(limit).ToList();
            return Json(new { total = total, rows = rows }, JsonRequestBehavior.AllowGet);
        }
    }
}