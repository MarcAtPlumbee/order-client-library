using ClientAPI.Orders.Models;
using ClientAPI.Network;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ClientAPI.Orders
{
    [TestClass]
    public class OrderServiceTest
    {
        private string END_POINT = OrderService.API_ENDPOINT;
        private OrderService orderService;
        private IHttpRequestHandler requestHandler;

        [TestInitialize]
        public void Setup()
        {
            requestHandler = Substitute.For<IHttpRequestHandler>();
            orderService = new OrderService(requestHandler);
        }

        [TestMethod]
        public async Task CreateOrderReturnsOrderFromHttpRequest()
        {
            requestHandler.SendPostRequest(END_POINT, Arg.Any<object>()).Returns(Task.Run<string>(() => ConvertToJson(new OrderVO { orderId = "orderID", items = new List<ItemVO>() })));

            var expectedResult = new OrderVO { orderId = "orderID", items = new List<ItemVO>() };
            var actualResult = await orderService.CreateOrder();
            Assert.AreEqual(expectedResult.orderId, actualResult.orderId);
            Assert.AreEqual(expectedResult.items.Count, actualResult.items.Count);
        }

        [TestMethod]
        public async Task GetOrderReturnsOrder()
        {
            var orderID = "orderID";
            requestHandler.SendGetRequest(END_POINT + "/" + orderID, Arg.Any<object>()).Returns(Task.Run<string>(() => ConvertToJson(new OrderVO { orderId = orderID, items = new List<ItemVO>() })));

            var expectedResult = new OrderVO { orderId = orderID, items = new List<ItemVO>() };
            var actualResult = await orderService.GetOrder(orderID);
            Assert.AreEqual(expectedResult.orderId, actualResult.orderId);
            Assert.AreEqual(expectedResult.items.Count, actualResult.items.Count);
        }

        [TestMethod]
        public async Task UpdateItemInOrderReturnsOrderWithUpdatedItem()
        {
            var orderID = "orderID";
            var itemToAddToOrder = new ItemVO { id = "item1", quantity = 2 };
            requestHandler.SendPutRequest(END_POINT+ "/"+orderID, itemToAddToOrder).Returns(Task.Run<string>(() => ConvertToJson(new OrderVO { orderId=orderID, items = new List<ItemVO> {itemToAddToOrder } })));

            var expectedResult = new OrderVO { orderId = orderID, items = new List<ItemVO> {itemToAddToOrder} };
            var actualResult = await orderService.AddItemToOrder(orderID, itemToAddToOrder);
            Assert.AreEqual(expectedResult.orderId, actualResult.orderId);
            Assert.AreEqual(expectedResult.items.Count, actualResult.items.Count);
        }

        [TestMethod]
        public async Task DeleteItemInOrderReturnsOrderWithUpdatedItem()
        {
            var orderID = "orderID";
            var itemToAddToOrder = new ItemVO { id = "item1", quantity = 2 };
            requestHandler.SendDeleteRequest(END_POINT + "/" + orderID + "/item", itemToAddToOrder).Returns(Task.Run<string>(() => ConvertToJson(new OrderVO { orderId = orderID, items = new List<ItemVO> {  } })));

            var expectedResult = new OrderVO { orderId = orderID, items = new List<ItemVO> {  } };
            var actualResult = await orderService.RemoveItemFromOrder(orderID, itemToAddToOrder);
            Assert.AreEqual(expectedResult.orderId, actualResult.orderId);
            Assert.AreEqual(expectedResult.items.Count, actualResult.items.Count);
        }

        [TestMethod]
        public async Task ClearOrderReturnsEmptyOrder()
        {
            var orderID = "orderID";
            requestHandler.SendDeleteRequest(END_POINT + "/" + orderID, Arg.Any<object>()).Returns(Task.Run<string>(() => ConvertToJson(new OrderVO { orderId = "orderID", items = new List<ItemVO>() })));

            var expectedResult = new OrderVO { orderId = orderID, items = new List<ItemVO>() };
            var actualResult = await orderService.ClearOrder(orderID);
            Assert.AreEqual(expectedResult.orderId, actualResult.orderId);
            Assert.AreEqual(expectedResult.items.Count, actualResult.items.Count);
        }

        private string ConvertToJson(object objectToConvert)
        {
            return JsonConvert.SerializeObject(objectToConvert);
        }
    }
}
