using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TanteBazar.WebApi.Client.Models
{
    public class BasketItemRequest
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
    }
}
