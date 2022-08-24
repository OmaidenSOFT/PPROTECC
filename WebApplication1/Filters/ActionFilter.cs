using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WebApplication1.Models;
namespace WebApplication1.Filters
{
  
        public class AccessAttribute : AuthorizeAttribute
        {
            protected override bool AuthorizeCore(HttpContextBase httpContext)
            {
                var session = httpContext.Session["SessionUser"] as SessionModels;
                if (session==null)
                    return false;

                return true;
            }

        }
    
}