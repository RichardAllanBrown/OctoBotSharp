using AutoMapper;
using OctoBotSharp.Data.Identity;
using OctoBotSharp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;

namespace OctoBotSharp.Web.Areas.Admin.Mappings
{
    public class UserPermissionsListResolver : ValueResolver<OctoUser, string[]>
    {
        private readonly OctoUserManager _userManager;

        public UserPermissionsListResolver(OctoUserManager userManager)
        {
            _userManager = userManager;
        }

        protected override string[] ResolveCore(OctoUser source)
        {
            return _userManager.GetRoles(source.Id).ToArray();
        }
    }
}