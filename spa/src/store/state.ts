import { CartItem } from '../models/cart-item';

export interface Store {
  cartItems: CartItem[];
}

export const initialState: Store = { cartItems: [] };
