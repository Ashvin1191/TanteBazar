using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TanteBazar.Core.Dtos;

namespace TanteBazar.Core.Services
{
    public interface IItemService
    {
        Task<List<Item>> GetItems();

        Task<Item> GetItem(int itemId);
    }
}
