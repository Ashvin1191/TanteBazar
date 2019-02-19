
using System.Collections.Generic;

namespace TanteBazar.WebApi.Models
{
    public class Basket
    {
        public List<BasketItem> Items { get; set; }
        public decimal TotalPrice { get; set; }
        public int TotalItems { get; set; }
    }
}


