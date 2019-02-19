using TanteBazar.Core.Dtos;

namespace TanteBazar.WebApi.Mappers
{
    public static class ItemMapper
    {
        public static Item MapToDto(Models.Item itemModel)
        {
            return new Item
            {
                Description = itemModel.Description,
                IsAvailable = itemModel.IsAvailble,
                ItemId = itemModel.Ref,
                Name = itemModel.Name,
                UnitPrice = itemModel.UnitPrice
            };
        }

        public static Models.Item MapFromDto(Item itemDto)
        {
            return new Models.Item
            {
                Description = itemDto.Description,
                IsAvailble = itemDto.IsAvailable,
                Ref = itemDto.ItemId,
                Name = itemDto.Name,
                UnitPrice = itemDto.UnitPrice
            };
        }
    }
}
