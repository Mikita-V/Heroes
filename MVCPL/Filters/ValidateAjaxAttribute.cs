using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MVCPL.Filters
{
    public class ValidateAjaxAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.HttpContext.Request.IsAjaxRequest())
            {
                return;
            }

            var modelState = filterContext.Controller.ViewData.ModelState;
            if (!modelState.IsValid)
            {
                var errorModel = modelState.Keys
                    .Where(x => modelState[x].Errors.Count > 0)
                    .Select(k => new { key = k, errors = modelState[k].Errors.Select(y => y.ErrorMessage).ToArray() });

                filterContext.Result = new JsonResult()
                {
                    Data = errorModel
                };

                //filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
        }
    }
}