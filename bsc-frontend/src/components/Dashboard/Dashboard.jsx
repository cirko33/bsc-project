import { userInRole } from "../../services/userService";
import AdminDashboard from "../admin-components/AdminDashboard/AdminDashboard";
import BuyerDashboard from "../buyer-components/BuyerDashboard/BuyerDashboard";
import SellerDashboard from "../seller-components/SellerDashboard/SellerDashboard";

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
