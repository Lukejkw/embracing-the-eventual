using MassTransit;
using Shipping.Contracts.Commands;

namespace Shipping.Consumers.ShipItemsConsumer;

public class ShipItemsConsumer : IConsumer<ShipItemsCommand>
{
    private readonly ILogger<ShipItemsConsumer> logger;

    public ShipItemsConsumer(ILogger<ShipItemsConsumer> logger)
    {
        this.logger = logger;
    }
    
    public Task Consume(ConsumeContext<ShipItemsCommand> context)
    {
        // This is a scam...
        // Keep the money and don't ship anything
        logger.LogInformation("Shipping items for cart {CartId}...", context.Message.CartId);
        logger.LogInformation("Totally sending order items for cart {CartId}...", context.Message.CartId);
        
        return Task.CompletedTask;
    }
}