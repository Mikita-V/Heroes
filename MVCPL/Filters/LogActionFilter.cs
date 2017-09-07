using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using NLog;

namespace MVCPL.Filters
{
    public class LogActionFilter : ActionFilterAttribute
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Logger.Trace($"Action {filterContext.ActionDescriptor.ActionName.ToUpper()} from controller {filterContext.ActionDescriptor.ControllerDescriptor.ControllerName.ToUpper()} is being executed...");
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            Logger.Trace($"Action {filterContext.ActionDescriptor.ActionName.ToUpper()} from controller {filterContext.ActionDescriptor.ControllerDescriptor.ControllerName.ToUpper()} is executed...");
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            //Logger.Trace($"OnResultExecuted: {filterContext.RouteData}");
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            //Logger.Trace($"OnResultExecuted: {filterContext.RouteData}");
        }
    }
}