using ClientAPI.Orders.Models;
using ClientAPI.Network;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace ClientAPI.Orders
{
    public class OrderService
    {
        public static readonly string API_ENDPOINT = "order";
        private readonly IHttpRequestHandler httpRequestHandler;

        public OrderService(IHttpRequestHandler requestHandler)
        {
            httpRequestHandler = requestHandler;
        }

        public async Task<OrderVO> CreateOrder()
        {
            var result = await httpRequestHandler.SendPostRequest(API_ENDPOINT, new object());
            return JsonConvert.DeserializeObject<OrderVO>(result);
        }

        public async Task<OrderVO> AddItemToOrder(string orderID, ItemVO item)
        {
            var result = await httpRequestHandler.SendPutRequest(API_ENDPOINT + "/" + orderID, item);
            return JsonConvert.DeserializeObject<OrderVO>(result);
        }

        public async Task<OrderVO> RemoveItemFromOrder(string orderID, ItemVO itemToRemove)
        {
            var result = await httpRequestHandler.SendDeleteRequest(API_ENDPOINT + "/" + orderID + "/item", itemToRemove);
            return JsonConvert.DeserializeObject<OrderVO>(result);
        }

        public async Task<OrderVO> ClearOrder(string orderID)
        {
            var result = await httpRequestHandler.SendDeleteRequest(API_ENDPOINT + "/" + orderID, new object());
            return JsonConvert.DeserializeObject<OrderVO>(result);
        }

        public async Task<OrderVO> GetOrder(string orderID)
        {
            var result = await httpRequestHandler.SendGetRequest(API_ENDPOINT + "/" + orderID, new object());
            return JsonConvert.DeserializeObject<OrderVO>(result);
        }
    }
}
