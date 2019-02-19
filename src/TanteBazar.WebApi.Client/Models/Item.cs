using System;

namespace TanteBazar.WebApi.Client.Models
{
    public class Item
    {
        public int Ref { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public string IsAvailble { get; set; }
    }
}
