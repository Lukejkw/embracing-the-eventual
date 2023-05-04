using MassTransit;

namespace Orders.Consumers.PlaceOrder;

public class PlaceOrderConsumerDefinition : ConsumerDefinition<PlaceOrderConsumer>
{
    protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<PlaceOrderConsumer> consumerConfigurator)
    {
        base.ConfigureConsumer(endpointConfigurator, consumerConfigurator);
        
        // Try twice immediately before failing
        consumerConfigurator.UseMessageRetry(o => o.Immediate(2));
    }
}