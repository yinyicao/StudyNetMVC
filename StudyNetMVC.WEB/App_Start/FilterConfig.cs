using StudyNetMVC.WEB.Controllers;
using System.Web;
using System.Web.Mvc;

namespace StudyNetMVC.WEB
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new MyActionFilter());//注册全局过滤器
        }
    }
}
