using Dapper;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TanteBazar.Core.Dtos;

namespace TanteBazar.Core.DataServices
{
    public class ItemDataService : IItemDataService
    {
        private ILogger _logger;
        private AppConfiguration _appConfiguration;

        public ItemDataService(ILogger logger, AppConfiguration appConfiguration)
        {
            _logger = logger;
            _appConfiguration = appConfiguration;
        }

        public async Task<Item> GetItem(int itemId)
        {
            var query = $"SELECT * FROM [dbo].[Item] Where [IsAvailable] = 'Y' AND ItemId = {itemId}";

            try
            {
                using (var sqlcon = new SqlConnection(_appConfiguration.TantaBazarDBConnectionString))
                {
                    var queryResult = await sqlcon.QueryAsync<Item>(query);

                    if (!queryResult.Any()) return null;

                    return queryResult.ToList().FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occurred in QuerryAllItems method.");

                // TODO: should throw to allow the Execton handler to build a response message.
            }

            return null;
        }

        public async Task<List<Item>> QueryAllItems()
        {
            var items = new List<Item>();

            var query = "SELECT * FROM [dbo].[Item] Where [IsAvailable] = 'Y'";

            try
            {
                using (var sqlcon = new SqlConnection(_appConfiguration.TantaBazarDBConnectionString))
                {
                    var queryResult = await sqlcon.QueryAsync<Item>(query);

                    items = queryResult.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occurred in QuerryAllItems method.");

                // TODO: should throw to allow the Execton handler to build a response message.
            }

            return items;
        }
    }
}
