using System;
using System.Collections.Generic;
using TanteBazar.WebApi.Models;

namespace TanteBazar.WebApi.Mappers
{
    public static class BasketMapper
    {
        public static Basket MapFromDto(List<Core.Dtos.Basket> basketDtos)
        {
            var basketItems = new List<BasketItem>();
            decimal total = 0m;

            foreach (var dto in basketDtos)
            {
                basketItems.Add(new BasketItem
                {
                    ItemName = dto.ItemName,
                    Quantity = dto.Quantity,
                    UnitPrice = dto.UnitPrice
                });

                total += (dto.Quantity * dto.UnitPrice);
            }

            return new Basket
            {
                Items = basketItems,
                TotalItems = basketItems.Count,
                TotalPrice = total
            };
        }
    }
}
