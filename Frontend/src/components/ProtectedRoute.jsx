import React from 'react';
import { Navigate, Outlet } from 'react-router-dom';
import { useAuth } from '../contexts/AuthContext';

const ProtectedRoute = () => {
    const auth = useAuth();
  
    if (!auth.isAuthenticated) {
      return <Navigate to="/" replace />;
    }
       
    return <Outlet />;
  };
  
  export default ProtectedRoute;