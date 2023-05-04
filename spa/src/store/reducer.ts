import { deduplicateCartItems } from '../core/deduplicate-cart-items';
import { ActionType, Actions, PayloadAction } from './actions';
import { initialState } from './state';

export const reducer = (state = initialState, action: Actions) => {
  switch (action.type) {
    case ActionType.IncreaseQuantity:
      return Object.assign({}, state, {
        cartItems: state.cartItems.map((item) => ({
          ...item,
          quantity:
            item.sku === (action as PayloadAction<{ sku: string }>).payload.sku
              ? item.quantity + 1
              : item.quantity,
        })),
      });
    case ActionType.DecreaseQuantity:
      return Object.assign({}, state, {
        cartItems: state.cartItems
          .map((item) => ({
            ...item,
            quantity:
              item.sku === (action as PayloadAction<{ sku: string }>).payload.sku
                ? item.quantity - 1
                : item.quantity,
          }))
          .filter(({ quantity }) => quantity > 0),
      });

    case ActionType.AddCartItem:
      return Object.assign({}, state, {
        cartItems: deduplicateCartItems([
          ...state.cartItems,
          {
            ...(action as PayloadAction<Product>).payload,
            quantity: 1,
          },
        ]),
      });

    case ActionType.RemoveFromCart:
      return Object.assign({}, state, {
        cartItems: state.cartItems.filter(
          ({ sku }) => sku !== (action as PayloadAction<{ sku: string }>).payload.sku
        ),
      });

    case ActionType.CheckoutSuccess:
      return Object.assign({}, state, {
        cartItems: [],
      });

    default:
      return state;
  }
};
