using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCore.Helpers
{
    public class AccessDeniedAuthorizeAttribute : AuthorizeAttribute
    {
        //public override void OnAuthorization(AuthorizationContext filterContext)
        //{
        //    base.OnAuthorization(filterContext);
        //    if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
        //    {
        //        filterContext.Result = new RedirectResult("~/Account/Logon");
        //        return;
        //    }

        //    if (filterContext.Result is HttpUnauthorizedResult)
        //    {
        //        filterContext.Result = new RedirectResult("~/Account/Denied");
        //    }
        //}
    }
}
