using Cart.CartAggregate;

namespace Cart.Endpoints.Cart;

public record CreateCartRequest(int UserId, List<CartItemRequestModel> Items);

public record CartItemRequestModel(string Sku, string Name, int Quantity);