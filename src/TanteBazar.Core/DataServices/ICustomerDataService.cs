using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TanteBazar.Core.DataServices
{
    public interface ICustomerDataService
    {
        Task<bool> IsUserValid(string customerId);
    }
}
