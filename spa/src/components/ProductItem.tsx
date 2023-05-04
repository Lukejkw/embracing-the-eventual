export interface ProductItemProps {
  sku: string;
  name: string;
  onAddToCart: (sku: string, name: string) => void;
}

export function ProductItem({ sku, name, onAddToCart }: ProductItemProps) {
  return (
    <div className="card">
      <h3>{name}</h3>
      <small>SKU: {sku}</small>

      <button onClick={() => onAddToCart(sku, name)}>Add To Cart</button>
    </div>
  );
}
