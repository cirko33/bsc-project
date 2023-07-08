import { Route, Routes, Navigate } from "react-router-dom";
import { Suspense, lazy } from "react";
import ProtectedRoute from "./ProtectedRoute";

const Dashboard = lazy(() => import("../components/Dashboard/Dashboard"));
const Login = lazy(() => import("../components/Login/Login"));
const Register = lazy(() => import("../components/Register/Register"));
const Profile = lazy(() => import("../components/Profile/Profile"));

const Router = () => {
  return (
    <>
      <Suspense
        fallback={
          <div style={{ display: "flex", justifyContent: "center", alignItems: "center", height: "100vh" }}>
            <div className="loading-spinner"></div>
          </div>
        }
      >
        <Routes>
          <Route path="/" element={<ProtectedRoute component={<Login />} />} />
          <Route path="/login" element={<ProtectedRoute component={<Login />} />} />
          <Route path="/register" element={<ProtectedRoute component={<Register />} />} />
          <Route path="/dashboard" element={<ProtectedRoute isLogged={true} component={<Dashboard />} />} />
          <Route path="/profile" element={<ProtectedRoute isLogged={true} component={<Profile />} />} />
          <Route path="*" element={<Navigate to="/" />} />
        </Routes>
      </Suspense>
    </>
  );
};

export default Router;
