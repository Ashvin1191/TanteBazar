using Serilog;

namespace TanteBazar.Core.Services
{
    public class OrderService : IOrderService
    {
        private ILogger _logger;

        public OrderService(ILogger logger)
        {
            _logger = logger;
        }

        public void LogHello()
        {
            _logger.Information("Hello from OrderService");
        }
    }
}
