namespace Order.Contracts;

public record PlaceOrderCommand(Guid CartId);