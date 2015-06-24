using OctoBotSharp.Data.Identity;
using OctoBotSharp.Data.Repository;
using OctoBotSharp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace OctoBotSharp.Service
{
    public interface IShopService
    {
        IEnumerable<ItemDefinition> GetAvailableItems();
        ServiceOperationResult PurchaseItem(int userId, int itemId);
    }

    public class ShopService : IShopService
    {
        private readonly ITransactionService _trnxService;
        private readonly IRepository<ItemInstance> _itemInstRepo;
        private readonly IRepository<ItemDefinition> _itemRepo;
        private readonly OctoUserManager _userManager;

        public ShopService(ITransactionService trnxService, IRepository<ItemInstance> itemInstRepo,
            IRepository<ItemDefinition> itemRepo, OctoUserManager userManager)
        {
            _trnxService = trnxService;
            _itemInstRepo = itemInstRepo;
            _itemRepo = itemRepo;
            _userManager = userManager;
        }

        public IEnumerable<ItemDefinition> GetAvailableItems()
        {
            return _itemRepo.GetMany(x => x.CanPurchase);
        }

        public ServiceOperationResult PurchaseItem(int userId, int itemId)
        {
            var user = _userManager.FindById(userId);
            if (user == null)
                return ServiceOperationResult.Fail("Could not find user");

            var itemDef = _itemRepo.Find(itemId);
            if (itemDef == null)
                return ServiceOperationResult.Fail("Count not find item");

            var instance = ItemInstance.Create(user, itemDef, "Purchase");

            _itemInstRepo.Insert(instance);

            return ServiceOperationResult.Ok;
        }
    }
}
