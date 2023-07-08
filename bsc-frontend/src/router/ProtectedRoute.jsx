import { Navigate } from "react-router-dom";

const ProtectedRoute = ({ component, isLogged }) => {
  const ret = () => {
    if (isLogged && localStorage.token) return component;
    if (!isLogged && !localStorage.token) return component;
    if (isLogged && !localStorage.token) return <Navigate to="/login" />;
    return <Navigate to="/dashboard" />;
  };

  return ret();
};

export default ProtectedRoute;
