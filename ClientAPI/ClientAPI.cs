using ClientAPI.Network;
using ClientAPI.Orders;

namespace ClientAPI
{
    public class ClientAPI
    {
        public OrderService orderService { get; }

        public ClientAPI() : this("http://localhost:51295/api/")
        {            
        }

        public ClientAPI(string baseURL)
        {
            orderService = new OrderService(new HttpRequestHandler(baseURL));
        }
    }
}
