export enum ActionType {
  IncreaseQuantity = 'INCREASE_QTY',
  DecreaseQuantity = 'DECREASE_QTY',
  AddCartItem = 'ADD_CART_ITEM',
  RemoveFromCart = 'REMOVE_CART_ITEM',
  CheckoutSuccess = 'CHECKOUT_SUCCESS',
  CheckoutError = 'CHECKOUT_ERROR',
}

export interface Action {
  type: ActionType;
}

export interface PayloadAction<T> extends Action {
  payload: T;
}

export const addCartItem = (product: Product) => ({
  type: ActionType.AddCartItem,
  payload: product,
});

export const increaseQuantity: (sku: string) => PayloadAction<{ sku: string }> = (sku: string) => ({
  type: ActionType.IncreaseQuantity,
  payload: {
    sku,
  },
});

export const decreaseQuantity: (sku: string) => PayloadAction<{ sku: string }> = (sku: string) => ({
  type: ActionType.DecreaseQuantity,
  payload: {
    sku,
  },
});

export const removeFromCart: (sku: string) => PayloadAction<{ sku: string }> = (sku: string) => ({
  type: ActionType.RemoveFromCart,
  payload: {
    sku,
  },
});

export const checkoutError: (error: string) => PayloadAction<{ error: string }> = (
  error: string
) => ({
  type: ActionType.CheckoutError,
  payload: {
    error,
  },
});

export const checkoutSuccess: () => Action = () => ({
  type: ActionType.CheckoutSuccess,
});

export type Actions = PayloadAction<{ sku: string }> | Action;
