using ClientAPI.Orders;
using ClientAPI.Orders.Models;
using System;
using System.Threading.Tasks;

namespace ClientConsoleSample
{
    class Sample
    {

        static void Main()
        {
            RunAsync().Wait();

            Console.ReadLine();
        }

        static async Task RunAsync()
        {
            try
            {
                OrderService orderService = new ClientAPI.ClientAPI().orderService;
                var orderVO = await orderService.CreateOrder();
                Console.WriteLine(orderVO.orderId);
                orderVO = await orderService.AddItemToOrder(orderVO.orderId, new ItemVO { id = "ItemId", quantity = 3 });
                Console.WriteLine("Items In Order: " + orderVO.items.Count);
                orderVO = await orderService.RemoveItemFromOrder(orderVO.orderId, orderVO.items[0]);
                Console.WriteLine("Items In Order: " + orderVO.items.Count);
                orderVO = await orderService.AddItemToOrder(orderVO.orderId, new ItemVO { id = "ItemId2", quantity = 3 });
                orderVO = await orderService.GetOrder(orderVO.orderId);
                Console.WriteLine(orderVO.orderId);
                Console.WriteLine("Items In Order: " + orderVO.items.Count);
                orderVO = await orderService.ClearOrder(orderVO.orderId);
                Console.WriteLine("Items In Order: " + orderVO.items.Count);
            } catch (Exception exception)
            {
                Console.Out.WriteLine(exception.Message);
            }
        }

    }
}
