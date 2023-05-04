namespace Order.Contracts;

public record OrderItemOutOfStock(Guid CartId, Guid OrderId, Guid OrderItemId, string ProductName, string ProductPrice);