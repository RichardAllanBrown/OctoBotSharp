using AutoMapper;
using OctoBotSharp.Data.Identity;
using OctoBotSharp.Domain;
using OctoBotSharp.Web.Models.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OctoBotSharp.Web.Controllers
{
    public class MembersController : Controller
    {
        private readonly OctoUserManager _userManager;

        public MembersController(OctoUserManager userManager)
        {
            _userManager = userManager;
        }

        public ActionResult Index()
        {
            var vm = _userManager.Users.Select(Mapper.Map<OctoUser, UserSummary>);

            return View(vm);
        }
    }
}
