using Cart.Contracts;
using MassTransit;
using Order.Contracts;

namespace Cart.Messaging.Sagas;

public class CheckoutSagaDefinition : SagaDefinition<CheckoutSaga>
{
    private const int ConcurrencyLimit = 20;
    
    protected override void ConfigureSaga(IReceiveEndpointConfigurator endpointConfigurator, ISagaConfigurator<CheckoutSaga> sagaConfigurator)
    {
        base.ConfigureSaga(endpointConfigurator, sagaConfigurator);

        endpointConfigurator.UseInMemoryOutbox();
        
        var partition = endpointConfigurator.CreatePartitioner(ConcurrencyLimit);
        sagaConfigurator.Message<CheckoutCart>(
            x => x.UsePartitioner(partition, m => m.Message.CartId));
        sagaConfigurator.Message<OrderCancelled>(
            x => x.UsePartitioner(partition, m => m.Message.CartId));
        sagaConfigurator.Message<OrderPlaced>(
            x => x.UsePartitioner(partition, m => m.Message.CartId));
    }
}