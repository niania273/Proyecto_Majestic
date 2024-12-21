using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto_Majestic
{
    public class AuthorizeSessionAtribute:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //verificamos que contenga los datos del usuario
            if (HttpContext.Current.Session["Codigo"] == null)
            {
                //si no hay una sesion activa redirige al Login
                filterContext.Result = new RedirectToRouteResult(
                    new System.Web.Routing.RouteValueDictionary
                    {
                        {"controller","Login" },
                        {"action","Index"}
                    });
            }
            base.OnActionExecuting(filterContext);
        }
    }
}