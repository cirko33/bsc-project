import { lazy, useEffect, useState } from "react";
import { getProducts } from "../../../services/productService";
import { getBuyerOrders } from "../../../services/orderService";

const Orders = lazy(() => import("../../../reusable/Orders/Orders"));
const Products = lazy(() => import("../../../reusable/Products/Products"));

const BuyerDashboard = () => {
  const [products, setProducts] = useState([]);
  const [orders, setOrders] = useState([]);

  useEffect(() => {
    updateProducts();
    updateOrders();
  }, []);

  const updateProducts = () => {
    getProducts()
      .then((res) => setProducts(res))
      .catch((err) => console.log(err));
  };

  const updateOrders = () => {
    getBuyerOrders()
      .then((res) => setOrders(res))
      .catch((err) => console.log(err));
  };
  return (
    <div className="dashboard-container">
      <div className="moved-left">
        <Products
          products={products}
          updateProducts={updateProducts}
          title="Products"
        />
      </div>

      <div className="moved-right">
        <Orders orders={orders} updateOrders={updateOrders} />
      </div>
    </div>
  );
};

export default BuyerDashboard;
