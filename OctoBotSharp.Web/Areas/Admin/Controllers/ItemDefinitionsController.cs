using AutoMapper;
using OctoBotSharp.Data.UnitOfWork;
using OctoBotSharp.Domain;
using OctoBotSharp.Service;
using OctoBotSharp.Web.Areas.Admin.Models;
using OctoBotSharp.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OctoBotSharp.Web.Areas.Admin.Controllers
{
    [Auth(AuthRole.ItemDefinitions)]
    public class ItemDefinitionsController : Controller
    {
        private readonly IService<ItemDefinition> _service;
        private readonly IUnitOfWork _uow;

        public ItemDefinitionsController(IService<ItemDefinition> service, IUnitOfWork uow)
        {
            _service = service;
            _uow = uow;
        }

        public ActionResult Index()
        {
            var items = _service.GetAll()
                .Select(Mapper.Map<ItemDefinition, ItemDefinitionSummaryViewModel>);

            return View(items);
        }

        public ActionResult Detail(int id)
        {
            var item = _service.Find(id);

            if (item == null)
                return HttpNotFound();

            var model = Mapper.Map<ItemDefinition, ItemDefinitionDetailViewModel>(item);

            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ItemDefinitionViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var domainModel = Mapper.Map<ItemDefinitionViewModel, ItemDefinition>(model, ItemDefinition.Create());

            _service.Insert(domainModel);
            _uow.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var item = _service.Find(id);
            if (item == null)
                return HttpNotFound();

            var model = Mapper.Map<ItemDefinition, ItemDefinitionViewModel>(item);

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(ItemDefinitionViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var existingItem = _service.Find(model.Id);
            if (existingItem == null)
                return HttpNotFound();

            var updatedModel = Mapper.Map<ItemDefinitionViewModel, ItemDefinition>(model, existingItem);

            _service.Update(updatedModel);
            _uow.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}