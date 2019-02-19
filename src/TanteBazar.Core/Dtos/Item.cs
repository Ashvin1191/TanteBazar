using System;

namespace TanteBazar.Core.Dtos
{
    public class Item
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Decimal UnitPrice { get; set; }
        public string IsAvailable { get; set; }
    }
}
