import { useReducer } from 'react';
import './App.css';
import { reducer } from './store/reducer';
import { initialState } from './store/state';
import {
  addCartItem,
  increaseQuantity,
  decreaseQuantity,
  removeFromCart,
  checkoutSuccess,
  checkoutError,
} from './store/actions';
import { Products } from './containers/Products';
import { Cart } from './containers/Cart';
import { patch, post } from './core/api';

// seCurItY
const userId = 1;

function App() {
  const [state, dispatch] = useReducer(reducer, initialState);

  const checkout = () => {
    post('cart', { userId, items: state.cartItems }, { Accept: 'application/text' })
      .then((response) => response.text())
      .then((cartId) =>
        patch('cart', {
          cartId,
          address: '42 Wallaby Way, Sydney',
          creditCardNumber: 'CARD NUMBER: 1234 5678 1234 4567, CVV: 123',
        })
      )
      .then(
        () => {
          dispatch(checkoutSuccess());
          console.info('Checkout success!');
        },
        (error) => {
          console.error(error);
          dispatch(checkoutError((error instanceof Error).toString()));
        }
      );
  };

  return (
    <main>
      <h1>Online Shop</h1>
      <p>
        This is a <strong>fake</strong> online shop to demonstrate an event-driven distributed
        application. All it does is create and checkout carts.
      </p>

      <div className="row">
        <div className="col-50">
          <h2>Product Catalog</h2>

          <Products onAddToCart={(product) => dispatch(addCartItem(product))} />
        </div>
        <div style={{ flexBasis: '49%', borderLeft: '1px solid white' }}>
          <h2>Cart</h2>

          <Cart
            cartItems={state.cartItems}
            onIncreaseQuantity={(sku) => dispatch(increaseQuantity(sku))}
            onRemoveFromCart={(sku) => {
              dispatch(removeFromCart(sku));
            }}
            onDecreaseQuantity={(sku) => dispatch(decreaseQuantity(sku))}
            onCheckout={checkout}
          />
        </div>
      </div>
    </main>
  );
}

export default App;
