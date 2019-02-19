using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TanteBazar.Core.DataServices;
using TanteBazar.Core.Dtos;

namespace TanteBazar.Core.Services
{
    public class ItemService : IItemService
    {
        private IItemDataService _itemDataService;
        public ItemService(IItemDataService itemDataService)
        {
            _itemDataService = itemDataService;
        }

        public async Task<List<Item>> GetItems()
        {
            return await _itemDataService.QueryAllItems();
        }

        public Task<Item> GetItem(int itemId)
        {
            throw new NotImplementedException();
        }
    }
}
