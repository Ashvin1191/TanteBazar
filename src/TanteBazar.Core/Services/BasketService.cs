using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TanteBazar.Core.DataServices;
using TanteBazar.Core.Dtos;

namespace TanteBazar.Core.Services
{
    public class BasketService : IBasketService
    {
        private ILogger _logger;
        private IBasketDataService _basketDataService;
        private IItemDataService _itemDataService;

        public BasketService(ILogger logger, IBasketDataService basketDataService, IItemDataService itemDataService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(_logger));
            _basketDataService = basketDataService ?? throw new ArgumentNullException(nameof(_basketDataService));
            _itemDataService = itemDataService ?? throw new ArgumentNullException(nameof(_itemDataService));
        }

        public async Task AddItemToBasket(string customerId, Dtos.BasketItemRequest basketItemRequest)
        {
            var item = await _itemDataService.GetItem(basketItemRequest.ItemId);

            if (item == null) throw new Exception("Invalid Item");

            var existingItem = await _basketDataService.QueryBasketItem(customerId, basketItemRequest.ItemId);

            if (existingItem == null)
                throw new Exception("Error while looking for previously added item in the basket");

            if (existingItem.Quantity > 0)
            {
                basketItemRequest.ItemQuantity += existingItem.Quantity;
                await _basketDataService.UpdateBasketItem(customerId, basketItemRequest, item);

            }
            else
            {
                await _basketDataService.AddItemToBasket(customerId, basketItemRequest, item);
            }
        }

        public async Task<List<Basket>> GetBasket(string customerId)
        {
            if (string.IsNullOrEmpty(customerId))
            {
                _logger
                    .ForContext<BasketDataService>()
                    .ForContext("MethodName", nameof(GetBasket))
                    .Error("customerId not specified");
                return null;
            }

            return await _basketDataService.QueryBasket(customerId);
        }

        public async Task RemoveFromBasket(string customerId, int itemId)
        {
            var getBasket = await _basketDataService.QueryBasket(customerId);
            var item = await _itemDataService.GetItem(itemId);

            if (getBasket == null)
            {
                _logger
                    .ForContext<BasketDataService>()
                    .Information("No basket found for this user.");

            }
            else if(getBasket.Where(x => x.ItemName.Equals(item.Name)) == null)
            {
                _logger
                    .ForContext<BasketDataService>()
                    .Information("No such Item present in the basket");
            }
            else
            {
                await _basketDataService.RemoveBasketItem(customerId, itemId);
            }
        }

        public async Task CheckoutBasket(string customerId)
        {
            var getBasket = await _basketDataService.QueryBasket(customerId);

            if (getBasket == null)
            {
                _logger
                    .ForContext<BasketDataService>()
                    .Information("NO Item present for check out");
            }
            else
            {
                await _basketDataService.CheckoutBasket(customerId);
            }
            
        }
    }
}
