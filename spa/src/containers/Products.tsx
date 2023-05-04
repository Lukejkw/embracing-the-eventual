import { ProductItem } from '../components/ProductItem';

const productsDb: Product[] = [
  {
    name: 'Macbook Pro',
    sku: 'macbook-pro-2023',
  },
  {
    name: 'Macbook Air',
    sku: 'macbook-air-2023',
  },
  {
    name: 'iPad Pro',
    sku: 'ipad-pro-2023',
  },
  {
    name: 'iPhone 14',
    sku: 'iphone-14-2023',
  },
  {
    name: 'Bag of hammers',
    sku: 'bag-of-hammers-2023',
  },
  {
    name: 'Bag of Samsungs',
    sku: 'bag-of-samsungs-2023',
  },
  {
    name: 'Bucket of cheese',
    sku: 'bag-of-cheese-2023',
  },
  {
    name: 'Glass of Whiskey',
    sku: 'glass-of-whiskey-2023',
  },
];

export interface ProductListProps {
  onAddToCart: (product: Product) => void;
}

export function Products({ onAddToCart }: ProductListProps) {
  return (
    <section className="row">
      {productsDb.map((product) => (
        <div key={product.sku} className="col-1_3">
          <ProductItem
            name={product.name}
            sku={product.sku}
            onAddToCart={() => onAddToCart(product)}
          />
        </div>
      ))}
    </section>
  );
}
