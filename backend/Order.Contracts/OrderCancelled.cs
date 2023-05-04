using MassTransit;

namespace Order.Contracts;

public record OrderCancelled(Guid CartId, Guid OrderId) : CorrelatedBy<Guid>
{
    public Guid CorrelationId => CartId;
}