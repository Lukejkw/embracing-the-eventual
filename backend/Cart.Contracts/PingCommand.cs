using MassTransit;

namespace Cart.Contracts;

public record PingCommand(string Message, bool ShouldFail);

public record CheckoutCart(Guid CartId, string Address, string CreditCardNumber) : CorrelatedBy<Guid>
{
    public Guid CorrelationId => CartId;
}