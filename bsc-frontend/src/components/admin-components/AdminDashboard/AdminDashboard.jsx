import { lazy, useEffect, useState } from "react";
import { getUsers } from "../../../services/userService";
import { getOrders } from "../../../services/orderService";

const UsersTable = lazy(() => import("../UsersTable/UsersTable"));
const OrderTable = lazy(() => import("../OrderTable/OrderTable"));

const AdminDashboard = () => {
  const [users, setUsers] = useState(null);
  const [orders, setOrders] = useState(null);

  useEffect(() => {
    updateUsers();
    updateOrders();
  }, []);

  const updateUsers = () => {
    getUsers().then((res) => setUsers(res));
  };
  const updateOrders = () => {
    getOrders().then((res) => setOrders(res));
  };

  return (
    <>
      <div className="mb20c" style={{ width: "90%" }}>
        <UsersTable users={users} updateUsers={updateUsers} />
        <OrderTable orders={orders} updateOrders={updateOrders} />
      </div>
    </>
  );
};

export default AdminDashboard;
