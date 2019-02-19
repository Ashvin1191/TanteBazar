using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TanteBazar.Core.Dtos;

namespace TanteBazar.Core.DataServices
{
    public interface IBasketDataService
    {
        Task<List<Basket>> QueryBasket(string customerId);

        Task<Basket> QueryBasketItem(string customerId, int itemId);

        Task AddItemToBasket(string customerId, Dtos.BasketItemRequest basketItemRequestDto, Dtos.Item itemDto);

        Task UpdateBasketItem(string customerId, Dtos.BasketItemRequest basketItemRequestDto, Dtos.Item itemDto);

        Task RemoveBasketItem(string customerId, int ItemId);

        Task CheckoutBasket(string customerId);
    }
}
