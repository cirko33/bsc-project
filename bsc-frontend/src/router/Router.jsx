import { Route, Routes, Navigate } from 'react-router-dom';
import { Suspense, lazy } from "react";
import { loggedIn } from '../services/userService';

const Dashboard = lazy(() => import('../components/Dashboard/Dashboard'));
const Login = lazy(() => import('../components/Login/Login'));
const Register = lazy(() => import('../components/Register/Register'));

const Router = () => {
  return (
    <>
      <Suspense fallback={<div>Loading...</div>}>
        <Routes>
          <Route path="/" element={loggedIn() ? <Navigate to='/dashboard'/> : <Login />} />
          <Route path="/login" element={loggedIn() ? <Navigate to='/dashboard'/> : <Login />} />
          <Route path="/register" element={loggedIn() ? <Navigate to='/dashboard'/> : <Register />} />
          <Route path="/dashboard" element={!loggedIn() ? <Navigate to='/'/> : <Dashboard />} />
          <Route path="*" element={<Navigate to='/'/>} />
        </Routes>
      </Suspense>
    </>
  );
};

export default Router;
