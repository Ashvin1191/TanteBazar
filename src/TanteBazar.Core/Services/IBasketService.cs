using System.Collections.Generic;
using System.Threading.Tasks;
using TanteBazar.Core.Dtos;

namespace TanteBazar.Core.Services
{
    public interface IBasketService
    {
        Task<List<Basket>> GetBasket(string customerId);

        Task AddItemToBasket(string customerId, Dtos.BasketItemRequest basketItemRequest);

        Task RemoveFromBasket(string customerId, int itemId);

        Task CheckoutBasket(string customerId);
    }
}
