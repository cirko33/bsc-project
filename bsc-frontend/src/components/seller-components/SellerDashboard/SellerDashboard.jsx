import { lazy, useEffect, useState } from "react";
import { getSellerProducts } from "../../../services/productService";
import { getSellerOrders } from "../../../services/orderService";

const Orders = lazy(() => import("../../../reusable/Orders/Orders"));
const Products = lazy(() => import("../../../reusable/Products/Products"));

const SellerDashboard = () => {
  const [products, setProducts] = useState([]);
  const [orders, setOrders] = useState([]);

  useEffect(() => {
    updateProducts();
    updateOrders();
  }, []);

  const updateProducts = () => {
    getSellerProducts()
      .then((res) => setProducts(res))
      .catch((err) => console.log(err));
  };

  const updateOrders = () => {
    getSellerOrders()
      .then((res) => setOrders(res))
      .catch((err) => console.log(err));
  };
  return (
    <div className="dashboard-container">
      <div className="moved-left">
        <Products
          products={products}
          updateProducts={updateProducts}
          title="My products"
        />
      </div>
      <div className="moved-right">
        <Orders orders={orders} updateOrders={updateOrders} />
      </div>
    </div>
  );
};

export default SellerDashboard;
