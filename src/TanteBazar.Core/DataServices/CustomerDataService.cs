using Dapper;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TanteBazar.Core.Dtos;

namespace TanteBazar.Core.DataServices
{
    public class CustomerDataService : ICustomerDataService
    {
        private AppConfiguration _appConfig;
        private ILogger _logger;

        public CustomerDataService(ILogger logger, AppConfiguration appConfiguration)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(_logger));
            _appConfig = appConfiguration ?? throw new ArgumentNullException(nameof(_appConfig));
        }

        public async Task<bool> IsUserValid(string customerId)
        {
            var  query = $"Select * from Customer where customerId = '{customerId}'" ;

            using (var connection = new SqlConnection(_appConfig.TantaBazarDBConnectionString))
            {

                try
                {
                    var queryResult = await connection.QueryAsync<Customer>(query);

                    if (!queryResult.Any())
                    {
                        return false;
                    }

                    return true;
                }
                catch (Exception ex)
                {
                    _logger
                       .Error(ex, "User Not Found.");
                }
            }

            return false;
        }

    }
}
