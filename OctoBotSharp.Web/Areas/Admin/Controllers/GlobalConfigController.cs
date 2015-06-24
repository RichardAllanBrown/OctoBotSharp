using OctoBotSharp.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OctoBotSharp.Domain;
using OctoBotSharp.Service;
using AutoMapper;
using OctoBotSharp.Web.Areas.Admin.Models;
using OctoBotSharp.Data.UnitOfWork;

namespace OctoBotSharp.Web.Areas.Admin.Controllers
{
    [Auth(AuthRole.GlobalConfig)]
    public class GlobalConfigController : Controller
    {
        private readonly IGlobalConfigService _configService;
        private readonly IUnitOfWork _uow;

        public GlobalConfigController(IGlobalConfigService configService, IUnitOfWork uow)
        {
            _configService = configService;
            _uow = uow;
        }

        public ActionResult Edit()
        {
            var config = _configService.GetConfig();

            var vm = Mapper.Map<GlobalConfig, GlobalConfigViewModel>(config);

            return View(vm);
        }

        [HttpPost]
        public ActionResult Edit(GlobalConfigViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var existingDomain = _configService.GetConfig();

            var domain = Mapper.Map<GlobalConfigViewModel, GlobalConfig>(vm, existingDomain);

            _configService.UpdateConfig(domain);
            _uow.SaveChanges();

            return RedirectToAction("Edit");
        }
    }
}