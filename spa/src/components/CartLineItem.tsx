export interface CartLineItemProps {
  name: string;
  quantity: number;
  sku: string;
  onDelete: (sku: string) => void;
  onIncreaseQuantity: (sku: string) => void;
  onDecreaseQuantity: (sku: string) => void;
}

export function CartLineItem({
  name,
  quantity,
  sku,
  onDelete,
  onIncreaseQuantity,
  onDecreaseQuantity,
}: CartLineItemProps) {
  return (
    <section className="card">
      <h3>{name}</h3>

      <div className="row centre">
        <div className="w-100">
          <button onClick={() => onDecreaseQuantity(sku)}>-</button>
          <span className="label">{quantity}</span>
          <button className="" onClick={() => onIncreaseQuantity(sku)}>
            +
          </button>
        </div>
        <button className="d-block text-red mt-5" onClick={() => onDelete(sku)}>
          Delete
        </button>
      </div>
    </section>
  );
}
