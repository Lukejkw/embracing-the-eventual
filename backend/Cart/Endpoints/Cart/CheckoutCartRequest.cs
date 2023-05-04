namespace Cart.Endpoints.Cart;

public record CheckoutCartRequest(Guid CartId, string Address, string CreditCardNumber);