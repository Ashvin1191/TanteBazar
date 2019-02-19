using System.Threading.Tasks;

namespace TanteBazar.Core.DataServices
{
    public interface ICustomerService
    {
        Task<bool> IsUserValid(string customerId);
    }
}
