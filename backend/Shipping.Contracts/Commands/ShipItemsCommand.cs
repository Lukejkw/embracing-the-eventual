namespace Shipping.Contracts.Commands;

public record ShipItemsCommand(Guid ItemId, Guid CartId, int Quantity, string DeliveryAddress);