using OctoBotSharp.Data.Identity;
using OctoBotSharp.Data.UnitOfWork;
using OctoBotSharp.Domain;
using OctoBotSharp.Service;
using OctoBotSharp.Web.Areas.Admin.Models;
using OctoBotSharp.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OctoBotSharp.Web.Areas.Admin.Controllers
{
    [Auth(AuthRole.UserFinancesAdjustment)]
    public class UserBalanceController : Controller
    {
        private readonly ITransactionService _trnxService;
        private readonly OctoUserManager _userManager;
        private readonly IUnitOfWork _uow;

        public UserBalanceController(ITransactionService trnxService, OctoUserManager userManager, IUnitOfWork uow)
        {
            _trnxService = trnxService;
            _userManager = userManager;
            _uow = uow;
        }

        public async Task<ActionResult> Adjust(int id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return HttpNotFound();

            var model = new UserBalanceAdjust()
            {
                UserId = user.Id,
                UserName = user.UserName,
                CurrentBalance = user.Money,
            };

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Adjust(UserBalanceAdjust adjustment)
        {
            if (!ModelState.IsValid)
                return View(adjustment);

            var user = await _userManager.FindByIdAsync(adjustment.UserId);
            if (user == null)
            {
                ModelState.AddModelError("UserId", "Cannot find user");
                return View(adjustment);
            }

            var result = _trnxService.AdjustBalance(user, adjustment.Amount, adjustment.Reason);

            if (!result.Success)
            {
                foreach (var err in result.Errors)
                    ModelState.AddModelError("", err);

                return View(adjustment);
            }

            _uow.SaveChanges();

            return RedirectToAction("Detail", "UserManagement", new { Id = adjustment.UserId });
        }
    }
}
