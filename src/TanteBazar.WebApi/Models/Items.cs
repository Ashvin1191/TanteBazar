using System;

namespace TanteBazar.WebApi.Models
{
    public class Item
    {
        public int Ref { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Decimal UnitPrice { get; set; }
        public bool IsAvailble { get; set; }

    }
}
