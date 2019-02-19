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
    public class BasketDataService : IBasketDataService
    {
        private ILogger _logger;
        private AppConfiguration _appConfig;
        private object itemDto;

        public BasketDataService(ILogger logger, AppConfiguration appConfiguration)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(_logger));
            _appConfig = appConfiguration ?? throw new ArgumentNullException(nameof(_appConfig));
        }

        public async Task AddItemToBasket(string customerId, BasketItemRequest basketItemRequestDto, Dtos.Item itemDto)
        {
            var param = new
            {
                CustomerId = customerId,
                ItemId = itemDto.ItemId,
                Quantity = basketItemRequestDto.ItemQuantity,
                TotalPrice = (itemDto.UnitPrice * basketItemRequestDto.ItemQuantity),
                IsRemove = false,
                IsCheckout = false,
                DateAdded = DateTime.Now,
                DateLastModified = DateTime.Now
            };

            var query = $@"INSERT INTO Basket(CustomerId, ItemId, Quantity, TotalPrice, IsRemove, IsCheckout, DateAdded, DateLastModified)
                           VALUES(@CustomerId, @ItemId, @Quantity, @TotalPrice, @IsRemove, @IsCheckout, @DateAdded, @DateLastModified)";

            using (var connection = new SqlConnection(_appConfig.TantaBazarDBConnectionString))
            {
                try
                {
                    await connection.ExecuteAsync(query, param);
                }
                catch (Exception ex)
                {
                    _logger
                        .Error(ex, "Error while saving basket item");
                }
            }
        }

        public async Task<List<Basket>> QueryBasket(string customerId)
        {
            var query = $@"
                        Select 
	                        C.CustomerId,
	                        B.DateAdded,
	                        B.DateLastModified,
	                        B.Quantity,
	                        I.UnitPrice,
	                        B.TotalPrice,
	                        I.Name AS ItemName 
                        from Basket B With(NOLOCK)
                        inner join Item I With(NOLOCK) On B.ItemId = I.ItemId
                        inner join Customer C With(NOLOCK) On B.CustomerId = C.CustomerId
                        WHERE 
	                        B.IsRemove = 0 AND
	                        B.IsCheckout = 0 AND
                            C.CustomerId = '{customerId}'";

            using (var connection = new SqlConnection(_appConfig.TantaBazarDBConnectionString))
            {
                try
                {
                    var queryResult = await connection.QueryAsync<Basket>(query);

                    if (!queryResult.Any())
                    {
                        return null;
                    }

                    return queryResult.ToList();
                }
                catch (Exception ex)
                {
                    _logger
                        .ForContext<BasketDataService>()
                        .Error(ex, "Error while loading user basket from the DB.");
                }

                return null;
            }
        }

        public async Task<Basket> QueryBasketItem(string customerId, int itemId)
        {
            var querry = $@"select * from Basket 
                            where customerId = '{customerId}' and itemId = {itemId} and isCheckout = 0 and isRemove = 0";

            using (var connection = new SqlConnection(_appConfig.TantaBazarDBConnectionString))
            {
                try
                {
                    var queryResult = await connection.QueryAsync<Basket>(querry);

                    if (!queryResult.Any())
                    {
                        return new Basket
                        {
                            Quantity = 0
                        };
                    }

                    return queryResult.FirstOrDefault();
                }
                catch (Exception ex)
                {
                    _logger
                        .ForContext<BasketDataService>()
                        .Error(ex, "Error when reading a single basket item.");
                }

                return null;
            }
        }

        public async Task UpdateBasketItem(string customerId, BasketItemRequest basketItemRequestDto, Item itemDto)
        {
            var query = $@"UPDATE Basket
                           SET DateLastModified = @DateLastModified,
                           Quantity = @Quantity,
                           TotalPrice = @TotalPrice
                           where customerId = '{customerId}' and itemId = {itemDto.ItemId} and isCheckout = 0 and isRemove = 0";

            var param = new
            {
                DateLastModified = DateTime.Now,
                Quantity = basketItemRequestDto.ItemQuantity,
                TotalPrice = (itemDto.UnitPrice * basketItemRequestDto.ItemQuantity)
            };

            using (var connection = new SqlConnection(_appConfig.TantaBazarDBConnectionString))
            {
                try
                {
                    var quertResult = await connection.ExecuteAsync(query, param);
                }
                catch (Exception ex)
                {
                    _logger
                        .ForContext<BasketDataService>()
                        .Error(ex, "Error while updating BasketService");
                }
            }
        }

        public async Task RemoveBasketItem(string customerId, int itemId)
        {
            var query = $@"UPDATE Basket 
                          SET isRemove = 1,
                          DateLastModified = @Date
                          Where  customerId = '{customerId}' and itemId = {itemId}
                          AND  isCheckout = 0";

            var param = new { Date = DateTime.Now };

            using (var connection = new SqlConnection(_appConfig.TantaBazarDBConnectionString))
            {
                try
                {
                    var quertResult = await connection.ExecuteAsync(query, param);
                }
                catch (Exception ex)
                {
                    _logger
                        .ForContext<BasketDataService>()
                        .Error(ex, "Error while Remove item from Basket");
                }
            }
        }


        public async Task CheckoutBasket(string customerId)
        {
            var query = $@"UPDATE Basket 
                          SET IsCheckOut = 1,
                          DateLastModified = @Date
                          Where  customerId = '{customerId}'
                          AND  isRemove = 0";

            var param = new { Date = DateTime.Now };


            using (var connection = new SqlConnection(_appConfig.TantaBazarDBConnectionString))
            {
                try
                {
                    var quertResult = await connection.ExecuteAsync(query, param);
                }
                catch (Exception ex)
                {
                    _logger
                        .ForContext<BasketDataService>()
                        .Error(ex, "Error while Occured during checkout");
                }
            }
        }


    }
}