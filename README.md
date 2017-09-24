# Client API Library for Order Web API

In this repository there are three projects.  
* The Client API Library
* The Client API Library Tests
* A very simple consoloe application using the Client API Library

## Client API Library Usage

```
 // Retrieves the Order Service
OrderService orderService = new ClientAPI.ClientAPI("http://baseurl/api").orderService;

// Create an Order
OrderVO orderVO = await orderService.CreateOrder();

// Add an item to your order
OrderVO orderVO = await orderService.AddItemToOrder(orderVO.orderId, new ItemVO { id = "ItemId", quantity = 3 });

// Remove an item from your order
OrderVO orderVO = await orderService.RemoveItemFromOrder(orderVO.orderId, orderVO.items[0]);

// Retrieve an existing order
orderVO = await orderService.GetOrder(orderVO.orderId);

// Clear your order of all items.
orderVO = await orderService.ClearOrder(orderVO.orderId);
```
