using System;
using System.Collections.Generic;
using System.Text;

namespace TanteBazar.Core.Dtos
{
    public class BasketItemRequest
    {
        public int ItemId { get; set; }

        public int ItemQuantity { get; set; }
    }
}
