import { CartItem } from '../models/cart-item';

export function deduplicateCartItems(items: CartItem[]) {
  const categorised = items.reduce((acc, current) => {
    const existingItem = acc[current.sku];

    if (existingItem) {
      return {
        ...acc,
        [current.sku]: {
          ...existingItem,
          quantity: existingItem.quantity + current.quantity,
        },
      };
    }

    return {
      ...acc,
      [current.sku]: current,
    };
  }, {} as { [k: string]: CartItem });
  return Object.values(categorised);
}
