using Cart.Contracts;
using MassTransit;

namespace Cart.Messaging.Consumers.Ping;

public class PingConsumerDefinition : ConsumerDefinition<PingConsumer>
{
    public PingConsumerDefinition()
    {
        EndpointName = CartEndpoints.Ping;
    }
    
    protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<PingConsumer> consumerConfigurator)
    {
        base.ConfigureConsumer(endpointConfigurator, consumerConfigurator);
        
        // Setup a retry policy
        consumerConfigurator.UseMessageRetry(
            x => x.Incremental(
                retryLimit: 3,
                TimeSpan.FromSeconds(1),
                TimeSpan.FromSeconds(1)));
    }
}