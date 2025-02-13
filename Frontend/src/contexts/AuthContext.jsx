import React, { createContext, useContext, useReducer, useState } from 'react';
import AuthReducer from '../reducers/AuthReducer';
import authService from '../services/authService';
import { data } from 'react-router-dom';
import { fireToast } from '../components/fireToast';

const AuthContext = createContext();

const user1 = localStorage.getItem("user")
const token1 = localStorage.getItem("token")
const roles1 = localStorage.getItem("roles")

const initialState = {
  isLoading: false,
  isError: false,
  user: user1 ? JSON.parse(user1) : null,
  token: token1 ? token1 : null,
  roles: roles1 ? JSON.parse(roles1) : []
};
export const useAuth = () => useContext(AuthContext);

export const AuthProvider = ({ children }) => {

  const { loginUser1 } = authService;

  const [state, dispatch] = useReducer(AuthReducer, initialState);

  const [isAuthenticated, setIsAuthenticated] = useState(
    !!localStorage.getItem('token')
  );

  const loginUser = async (data1, navigate) => {
    dispatch({ type: "SET_LOADING" });
    try {
      const data = await loginUser1(data1);

      let roles = [];

      roles = data?.user?.UserRoles?.map((role) => {
        return role?.FkRole?.Name;
      })

      localStorage.setItem("roles", JSON.stringify(roles));
      localStorage.setItem("token", data?.token);
      localStorage.setItem("user", JSON.stringify(data?.user));
      setIsAuthenticated(true);
      fireToast("Sucessfully login", "success");
      dispatch({ type: "USER_LOGIN", payload: data });
    } catch (error) {
      handleAuthError(error, navigate);
      dispatch({ type: "SET_ERROR" });
    }
  };

  const logout = () => {
    dispatch({ type: "LOGOUT" })
    localStorage.clear();
    setIsAuthenticated(false);
  };

  const handleAuthError = (error, navigate) => {
    if (error?.response?.status === 401 || error?.response?.status === 403) {
      logout();
      navigate("/");
      fireToast("Unauthorized access", "error");
    } else {
      fireToast(error?.response?.data, "error");
    }
  };

  return (
    <AuthContext.Provider value={{ ...state, isAuthenticated, loginUser, logout }}>
      {children}
    </AuthContext.Provider>
  );
};
