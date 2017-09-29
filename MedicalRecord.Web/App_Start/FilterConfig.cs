using log4net;
using log4net.Repository.Hierarchy;
using System;
using System.Web;
using System.Web.Mvc;

namespace MedicalRecord.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new ErrorAttribute());
        }

        public class ErrorAttribute : HandleErrorAttribute
        {
            private readonly log4net.ILog log = log4net.LogManager.GetLogger("Root");

            public override void OnException(ExceptionContext filterContext)
            {
                Exception ex = filterContext.Exception;
                filterContext.ExceptionHandled = true;
                var model = new HandleErrorInfo(filterContext.Exception, filterContext.Controller.ToString(), "Action");
                log.Fatal("Global Exception Handler", ex);
                filterContext.Result = new ViewResult()
                {
                    ViewName = "Error",
                    ViewData = new ViewDataDictionary(model)
                };
            }
        }
    }
}
