using System;

namespace TanteBazar.Core.Dtos
{
    public class Basket
    {
        public Guid CustomerId { get; set; }
        public string ItemName { get; set; }
        public decimal  UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateLastModified { get; set; }
    }
}


