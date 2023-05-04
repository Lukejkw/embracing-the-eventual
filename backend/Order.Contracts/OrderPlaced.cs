using MassTransit;

namespace Order.Contracts;

public record OrderPlaced(Guid CartId, Guid OrderId) : CorrelatedBy<Guid>
{
    public Guid CorrelationId => CartId;
}