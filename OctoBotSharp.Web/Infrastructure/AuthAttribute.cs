using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OctoBotSharp.Web.Infrastructure
{
    public class AuthAttribute : AuthorizeAttribute
    {
        public AuthAttribute(params string[] roles)
            : base()
        {
            Roles = string.Join(",", roles);
        }
    }
}