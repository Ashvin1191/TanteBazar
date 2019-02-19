namespace TanteBazar.WebApi.Mappers
{
    public static class BasketItemRequestMapper
    {
        public static Core.Dtos.BasketItemRequest MapToDto(Models.BasketItemRequest basketItemRequest)
        {
            return new Core.Dtos.BasketItemRequest
            {
                ItemId = basketItemRequest.Id,
                ItemQuantity = basketItemRequest.Quantity
            };
        }

        public static Models.BasketItemRequest MapFromDto(Core.Dtos.BasketItemRequest basketItemRequest)
        {
            return new Models.BasketItemRequest
            {
                Id = basketItemRequest.ItemId,
                Quantity = basketItemRequest.ItemQuantity
            };
        }
    }
}
