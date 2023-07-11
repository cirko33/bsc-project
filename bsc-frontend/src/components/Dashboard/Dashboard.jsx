import { lazy } from "react";
import { userInRole } from "../../services/userService";

const AdminDashboard = lazy(() => import("../admin-components/AdminDashboard/AdminDashboard"));
const SellerDashboard = lazy(() => import("../seller-components/SellerDashboard/SellerDashboard"));
const BuyerDashboard = lazy(() => import("../buyer-components/BuyerDashboard/BuyerDashboard"));

const Dashboard = () => {
  return (
    <>
      {userInRole("Administrator") && <AdminDashboard />}
      {userInRole("Seller") && <SellerDashboard />}
      {userInRole("Buyer") && <BuyerDashboard />}
    </>
  );
};

export default Dashboard;
