using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudyNetMVC.WEB.Controllers
{
    public class MyActionFilter:ActionFilterAttribute
    {

        #region 是否登录

        public bool IsLogin { get; set; }

        #endregion

        #region 执行action前执行这个方法

        public override void OnActionExecuting(ActionExecutingContext filterContext)

        {

            var varget = filterContext.HttpContext.Session["UserName"];

            if (IsLogin == false)
            {

                if (varget == null || "".Equals(varget))
                {

                    filterContext.Result = new RedirectResult("/Home/Index");

                }

                return;

            }

        }

        #endregion
    }
}