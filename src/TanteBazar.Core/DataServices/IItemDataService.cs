using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TanteBazar.Core.Dtos;

namespace TanteBazar.Core.DataServices
{
    public interface IItemDataService
    {
        Task<List<Item>> QueryAllItems();

        Task<Item> GetItem(int itemId);
    }
}
