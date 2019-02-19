using Moq;
using Serilog;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;
using TanteBazar.Core.DataServices;
using TanteBazar.Core.Services;
using Xunit;

namespace TanteBazar.Tests
{
    public class BasketServiceTests
    {
        private IBasketService _basketservice;

        private Mock<IBasketDataService> basketDataServiceMoq;
        private Mock<IItemDataService> itemdataServiceMoq;
        private Mock<ILogger> loggerMoq;

        public BasketServiceTests()
        {
            basketDataServiceMoq = new Mock<IBasketDataService>();
            itemdataServiceMoq = new Mock<IItemDataService>();
            loggerMoq = new Mock<ILogger>();

            loggerMoq.Setup(x => x.ForContext(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<bool>())).Returns(loggerMoq.Object);

            _basketservice = new BasketService(loggerMoq.Object, basketDataServiceMoq.Object, itemdataServiceMoq.Object);
        }

        [Fact]
        public async void IfCustomerIdIsNullThenGetBasketShouldbeNull()
        {
            var result = await _basketservice.GetBasket(null);
            result.ShouldBe(null);
        }

        [Fact]
        public async void IfNewBasketThen()
        {
            var result = await _basketservice.CheckoutBasket()
        }
    }
}
