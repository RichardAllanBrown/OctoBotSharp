using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using OctoBotSharp.Domain;
using OctoBotSharp.Web.Infrastructure;
using OctoBotSharp.Service;
using AutoMapper;
using Microsoft.AspNet.Identity;
using OctoBotSharp.Web.Areas.Admin.Models;
using OctoBotSharp.Data.Identity;

namespace OctoBotSharp.Web.Areas.Admin.Controllers
{
    [Auth(AuthRole.UserManagement)]
    public class UserManagementController : Controller
    {
        private readonly IService<OctoUser> _userService;
        private readonly OctoUserManager _userManager;
        private readonly OctoRoleManager _roleManager;

        public UserManagementController(IService<OctoUser> userService, OctoUserManager userManager, OctoRoleManager roleManager)
        {
            _userService = userService;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public ActionResult Index()
        {
            var summary = _userService.GetAll()
                .Select(Mapper.Map<OctoUser, UserSummaryViewModel>)
                .ToList();

            return View(summary);
        }

        public ActionResult Detail(int id)
        {
            var user = _userManager.FindById(id);
            if (user == null)
                return HttpNotFound();

            var viewModel = Mapper.Map<OctoUser, UserDetailViewModel>(user);

            return View(viewModel);
        }

        public ActionResult Permissions(int id)
        {
            var user = _userManager.FindById(id);

            if (user == null)
                return HttpNotFound();

            var availableRoles = _roleManager.Roles.Select(x => x.Name).ToList();
            var givenRoles = _userManager.GetRoles(id);

            var viewModel = new UserPermissionsViewModel()
            {
                UserId = id,
                UserName = user.UserName,
                Roles = availableRoles.ToDictionary(key => key, value => givenRoles.Contains(value))
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult SetPermissions(UserPermissionsViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = _userManager.FindById(model.UserId);
            if (user == null)
                return HttpNotFound();

            var currentRoles = _userManager.GetRoles(model.UserId);
            var rolesToRemove = model.Roles.Where(x => !x.Value && currentRoles.Contains(x.Key)).Select(x => x.Key).ToArray();
            var rolesToAdd = model.Roles.Where(x => x.Value && !currentRoles.Contains(x.Key)).Select(x => x.Key).ToArray();

            var removeResult = _userManager.RemoveFromRoles(model.UserId, rolesToRemove);
            if (!removeResult.Succeeded)
            {
                ModelState.AddModelError("", removeResult.Errors.First());
                return View(model);
            }

            var addResult = _userManager.AddToRoles(model.UserId, rolesToAdd);
            if (!addResult.Succeeded)
            {
                ModelState.AddModelError("", removeResult.Errors.First());
                return View(model);
            }

            return RedirectToAction("Index");
        }
    }
}
