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
    public class HomeController : Controller
    {
        IUserService userService = null;
        public HomeController()
        {
            userService = new UserService();
        }
        public bool Login(string username, string pass,string loginType)
        {
            bool res = false;
            switch (loginType)
            {
                case "email":
                    res = BLL.Exec.QueryUserAccountInfoByEmailAndPass(username, pass);
                    break;
                case "phone":
                    res =  BLL.Exec.QueryUserAccountInfoByPhoneAndPass(username, pass);
                    break;
                default:
                    res =  false;
                    break;
            }

            return res;
           
        }

        public bool EditUser(string id,string username,string email,string phone,string pass)
        {
            return userService.EditUser( id,  username,  email,  phone,  pass);
        }

        public bool Register(string email,string phone,string pass)
        {
            return userService.createUser(email,phone,pass);
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Main()
        {
            return View("Main");
        }

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