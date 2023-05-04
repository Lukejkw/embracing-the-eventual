using MassTransit;
using Shipping.Contracts;

namespace Shipping.Consumers.ShipItemsConsumer;

public class ShipItemsConsumerDefinition : ConsumerDefinition<ShipItemsConsumer>
{
    public ShipItemsConsumerDefinition()
    {
        EndpointName = ShippingQueueEndpoints.ShipItems;
    }
    
    protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<ShipItemsConsumer> consumerConfigurator)
    {
        base.ConfigureConsumer(endpointConfigurator, consumerConfigurator);
        
        consumerConfigurator.UseMessageRetry(
            x => x.Incremental(
                retryLimit: 3,
                TimeSpan.FromSeconds(1),
                TimeSpan.FromSeconds(1)));
    }
}