using AutoMapper;
using OctoBotSharp.Domain;
using OctoBotSharp.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using OctoBotSharp.Web.Models;
using OctoBotSharp.Data.UnitOfWork;

namespace OctoBotSharp.Web.Controllers
{
    public class ShopController : Controller
    {
        private readonly IShopService _shopService;
        private readonly IUnitOfWork _uow;

        public ShopController(IShopService shopService, IUnitOfWork uow)
        {
            _shopService = shopService;
            _uow = uow;
        }

        public ActionResult Index()
        {
            var items = _shopService.GetAvailableItems();

            var vm = items.Select(Mapper.Map<ItemDefinition, AvailableItemViewModel>);

            return View(vm);
        }

        [HttpPost]
        public ActionResult Purchase(int itemId)
        {
            var purchaseResult = _shopService.PurchaseItem(GetUserId(), itemId);

            if (!purchaseResult.Success)
                return Json(new { Success = false, Errors = purchaseResult.Errors });

            _uow.SaveChanges();

            return Json(new { Success = true });
        }

        private int GetUserId()
        {
            return Convert.ToInt32(User.Identity.GetUserId());
        }
    }
}
