using MassTransit;
using MongoDB.Driver;
using Order.Contracts;

namespace Orders.Consumers.PlaceOrder;

public class PlaceOrderConsumer : IConsumer<PlaceOrderCommand>
{
    private readonly IMongoClient mongoClient;
    private readonly ILogger<PlaceOrderConsumer> logger;

    public PlaceOrderConsumer(IMongoClient mongoClient, ILogger<PlaceOrderConsumer> logger)
    {
        this.mongoClient = mongoClient;
        this.logger = logger;
    }
    
    public async Task Consume(ConsumeContext<PlaceOrderCommand> context)
    {
        var orderId = Guid.NewGuid();
        var order = new Models.Order(context.Message.CartId, orderId);

        await mongoClient.GetDatabase("orders")
            .GetCollection<Models.Order>("orders")
            .InsertOneAsync(order, new InsertOneOptions(), default);

        await context.Publish(new OrderPlaced(order.CartId, orderId));
        
        logger.LogInformation("Placing order for cart {CartId}", order.CartId);
    }
}