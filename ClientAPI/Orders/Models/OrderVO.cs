using System.Collections.Generic;

namespace ClientAPI.Orders.Models
{
    public class OrderVO
    {
        public string orderId;
        public List<ItemVO> items;
    }
}
