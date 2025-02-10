import React, { createContext, useContext, useReducer, useState } from 'react';
import authService from '../services/authService';
import UserReducer from '../reducers/UserReducer';
import { useAuth } from './AuthContext';
import AuthHeader from '../helper/AuthHeader';
import { fireToast } from '../components/fireToast';

const UserContext = createContext();

const initialState = {
    isLoading: false,
    isError: false,
    users: [],
    filteredUsers: [],
    searchTerm: ""
};
export const useUserContext = () => useContext(UserContext);

export const UserProvider = ({ children }) => {

    const { registerUser, getUsers, updateUser, deleteUser } = authService;
    const { logout } = useAuth();

    const headers = AuthHeader();

    const [state, dispatch] = useReducer(UserReducer, initialState);

    const addUser = async (data1, navigate) => {
        dispatch({ type: "SET_LOADING" });
        try {
            const data = await registerUser(data1, headers);
            dispatch({ type: "ADD_USER", payload: data });
            fireToast("user successfully added", "success");
        } catch (error) {
            handleAuthError(error, navigate);
            dispatch({ type: "SET_ERROR" });
        }
    };

    const getAllUsers = async (navigate) => {
        dispatch({type : "SET_LOADING"});
        try {
            const data = await getUsers(headers);
            dispatch({ type: "GET_USERS", payload: data});
        } catch (error) {
            handleAuthError(error, navigate);
            dispatch({ type: "SET_ERROR" });
        }
    }

    const editUser = async (id, data1, navigate) => {     
        try {
            let obj = {
                FullName : data1.FullName,
                Email : data1.Email,
                Phone : data1.Phone,
                Password : data1.Password,
                JoiningDate : data1.JoiningDate,
                Roles : data1.Roles
            }

            const data = await updateUser(id, obj, headers);
            if(data != undefined && data != null) {
                getAllUsers(navigate);
                fireToast("Update user successfully", "success");
            }
        } catch (error) {
            handleAuthError(error, navigate);
        }
    }

    const removeUser = async (id, navigate) => {
        dispatch({type : "SET_LOADING"});
        try {
            const data = await deleteUser(id, headers);
            dispatch({ type: "DELETE_USER", payload: id});
            
        } catch (error) {
            handleAuthError(error, navigate);
            dispatch({ type: "SET_ERROR" });
        }
    }

    const searchUser = (searchTerm) => {
        dispatch({ type: "SEARCH_USER", payload: searchTerm });
    };

    const handleAuthError = (error, navigate) => {
        if (error?.response?.status === 401 || error?.response?.status === 403) {
            logout();
            fireToast("Unauthorized access", "error");
            navigate("/");
        }

        fireToast(error.message, "error");
    };

    return (
        <UserContext.Provider value={{ ...state, addUser, getAllUsers, searchUser, editUser, removeUser }}>
            {children}
        </UserContext.Provider>
    );
};
