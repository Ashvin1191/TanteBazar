using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TanteBazar.WebApi.Models;
using TanteBazar.Core.Services;
using System.Security.Claims;
using TanteBazar.WebApi.Mappers;

namespace TanteBazar.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class BasketController : Controller
    {
        private IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService ?? throw new ArgumentNullException(nameof(_basketService));
        }

        [HttpGet]
        public async Task<IActionResult> GetItemsFromBasket()
        {
            var apiUser = BuildApiUser();

            if (apiUser == null) return Unauthorized("Invalid or no X_API_SECRET");
            var basketResult = await _basketService.GetBasket(apiUser.CustomerKey);

            if (basketResult == null)
            {
                return NotFound("No item found in this basket");
            }

            return Ok(BasketMapper.MapFromDto(basketResult));
        }

        [HttpPost]
        public async Task<IActionResult> AddItemToBasket([FromBody] BasketItemRequest basketItemRequest)
        {
            var apiUser = BuildApiUser();

            if (apiUser == null) return Unauthorized("Invalid or no X_API_SECRET");

            await _basketService.AddItemToBasket(apiUser.CustomerKey, BasketItemRequestMapper.MapToDto(basketItemRequest));

            return Ok(basketItemRequest);
        }

        [HttpPost("remove")]
        public async Task<IActionResult> RemoveBasketItem([FromBody] BasketItemRequest basketItemRequest)
        {

            var apiUser = BuildApiUser();

            if (apiUser == null) return Unauthorized("Invalid or no X_API_SECRET");

            try
            {
                await _basketService.RemoveFromBasket(apiUser.CustomerKey, basketItemRequest.Id);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

            return Ok("Item has been removed");
        }


        [HttpPost("checkout")]
        public async Task<IActionResult> CheckoutBasket([FromBody] BasketItemRequest basketItemRequest)
        {
            var apiUser = BuildApiUser();

            if (apiUser == null) return Unauthorized("Invalid or no X_API_SECRET");

            try
            {
                await _basketService.CheckoutBasket(apiUser.CustomerKey);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            

            return Ok("Item has been checkout");
        }

        private ApiUser BuildApiUser()
        {
            // TODO: Add Role for users
            var role = string.Empty;

            var customerId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid)?.Value;

            if (string.IsNullOrEmpty(customerId)) return null;

            return new ApiUser
            {
                CustomerKey = customerId,
                Role = role
            };
        }
    }
}
;