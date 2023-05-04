using MassTransit;
using MongoDB.Driver;
using Order.Contracts;

namespace Orders.Consumers.PlaceOrder;

public class PlaceOrderConsumer : IConsumer<PlaceOrderCommand>
{
    private readonly IMongoClient mongoClient;

    public PlaceOrderConsumer(IMongoClient mongoClient)
    {
        this.mongoClient = mongoClient;
    }
    
    public async Task Consume(ConsumeContext<PlaceOrderCommand> context)
    {
        var orderId = Guid.NewGuid();
        var order = new Models.Order(context.Message.CartId, orderId);

        await mongoClient.GetDatabase("orders")
            .GetCollection<Models.Order>("orders")
            .InsertOneAsync(order, new InsertOneOptions(), default);

        await context.Publish(new OrderPlaced(order.CartId, orderId));
    }
}