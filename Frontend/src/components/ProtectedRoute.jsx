import React from 'react';
import { Navigate, Outlet } from 'react-router-dom';
import { useAuth } from '../contexts/AuthContext';

const ProtectedRoute = ({allowedRoles}) => {
    const auth = useAuth();
  
    if (!auth.isAuthenticated) {
      return <Navigate to="/" replace />;
    }

    const hasAccess = auth?.roles?.some(role => allowedRoles.includes(role));
       
    return hasAccess ? <Outlet /> : <Navigate to="/unauthorized" />;
    // return <Outlet />
  };
  
  export default ProtectedRoute;