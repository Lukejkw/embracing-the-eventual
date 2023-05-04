import { CartLineItem } from '../components/CartLineItem';
import { CartItem } from '../models/cart-item';

export interface CartProps {
  cartItems: CartItem[];
  onIncreaseQuantity: (sku: string) => void;
  onDecreaseQuantity: (sku: string) => void;
  onRemoveFromCart: (sku: string) => void;
  onCheckout: () => void;
}

export function Cart({
  cartItems,
  onIncreaseQuantity,
  onRemoveFromCart,
  onDecreaseQuantity,
  onCheckout,
}: CartProps) {
  const isCartEmpty = !cartItems || cartItems.length === 0;

  return (
    <section>
      {isCartEmpty && <p>Your cart is empty. Add some totally real items below.</p>}

      {!isCartEmpty && (
        <>
          <div className="row">
            {cartItems.map((item) => (
              <div key={item.sku} className="col-1_3">
                <CartLineItem
                  {...item}
                  onDelete={() => onRemoveFromCart(item.sku)}
                  onIncreaseQuantity={() => onIncreaseQuantity(item.sku)}
                  onDecreaseQuantity={() => onDecreaseQuantity(item.sku)}
                />
              </div>
            ))}
          </div>

          <button onClick={onCheckout}>Checkout</button>
        </>
      )}

      {}
    </section>
  );
}
