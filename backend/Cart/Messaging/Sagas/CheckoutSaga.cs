using Cart.Contracts;
using MassTransit;
using Order.Contracts;
using Shipping.Contracts;
using Shipping.Contracts.Commands;

namespace Cart.Messaging.Sagas;

public class CheckoutSaga : ISaga,
    InitiatedBy<CheckoutCart>, // This is the event that TRIGGERS the saga
    Orchestrates<OrderCancelled>, // These are events which happen as a result of a saga's orchestration
    Orchestrates<OrderPlaced>, // this one too
    ISagaVersion // This is needed for persistence
{
    public int Version { get; set; } = 1;
    
    private readonly ILogger logger;

    public CheckoutSaga()
    {
        // You typically want your saga to be pure
        // Just doing this so we can see what is going on
        logger = LoggerFactory.Create(options =>
        {
            options.AddConsole();
        }).CreateLogger(typeof(CheckoutSaga));
    }

    /// <summary>
    /// The ID used to identify the saga - it ties all the messages together
    /// </summary>
    public Guid CorrelationId { get; set; }

    // Some internal state we are tracking for the saga
    public DateTime? CartCheckedOutOn { get; set; }
    public DateTime? OrderCreatedOn { get; set; }
    public DateTime? OrderShippedOn { get; set; }

    public async Task Consume(ConsumeContext<CheckoutCart> context)
    {
        logger.LogInformation("Started checkout process for cart {CartId}", context.Message.CartId);

        CartCheckedOutOn = DateTime.UtcNow; // Saga state

        var command = new PlaceOrderCommand(context.Message.CartId);
        var endpoint = await context.GetSendEndpoint(new Uri($"queue:{OrderEndpoints.PlaceOrder}"));
        await endpoint.Send(command);
    }

    public Task Consume(ConsumeContext<OrderCancelled> context)
    {
        logger.LogInformation("Order was cancelled for cart {CartId}", context.Message.CartId);

        return Task.CompletedTask;
    }

    public async Task Consume(ConsumeContext<OrderPlaced> context)
    {
        logger.LogInformation(
            "Order placed for cart {CartId}. Shipping order {OrderId}",
            context.Message.CartId,
            context.Message.OrderId);

        OrderCreatedOn = DateTime.UtcNow;

        var command = new ShipItemsCommand(context.Message.CartId);
        var endpoint = await context.GetSendEndpoint(new Uri($"queue:{ShippingQueueEndpoints.ShipItems}"));
        await endpoint.Send(command);
    }
}