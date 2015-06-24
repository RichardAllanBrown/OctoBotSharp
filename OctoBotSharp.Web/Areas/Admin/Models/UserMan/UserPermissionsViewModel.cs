using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OctoBotSharp.Web.Areas.Admin.Models
{
    public class UserPermissionsViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public string UserName { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int UserId { get; set; }

        public Dictionary<string, bool> Roles { get; set; }
    }
}
