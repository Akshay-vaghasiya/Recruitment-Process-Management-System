import React, { createContext, useContext, useReducer } from 'react';
import CandidateReducer from '../reducers/CandidateReducer';
import { useAuth } from './AuthContext';
import AuthHeader from '../helper/AuthHeader';
import { fireToast } from '../components/fireToast';
import candidateService from '../services/candidateService';

const CandidateContext = createContext();

const initialState = {
    isLoading: false,
    isError: false,
    candidates: [],
    filteredCandidates: [],
    searchTerm: ""
};

export const useCandidateContext = () => useContext(CandidateContext);

export const CandidateProvider = ({ children }) => {

    const { registerCandidate, getCandidates, updateCandidate, deleteCandidate } = candidateService;
    const { logout } = useAuth();

    const headers = AuthHeader();

    const [state, dispatch] = useReducer(CandidateReducer, initialState);

    const addCandidate = async (data1, file, navigate) => {
        dispatch({ type: "SET_LOADING" });
        try {
            let bodyFormData = new FormData();
            bodyFormData.append("FullName", data1.FullName);
            bodyFormData.append("Password", data1.Password);
            bodyFormData.append("Email", data1.Email);
            bodyFormData.append("Phone", data1.Phone);
            bodyFormData.append("YearsOfExperience", data1.YearsOfExperience);
            bodyFormData.append("Resume", file);

            let headers1 = headers;
            headers1['Content-Type'] = "multipart/form-data";

            const data = await registerCandidate(bodyFormData, headers1);
            dispatch({ type: "ADD_CANDIDATE", payload: data });
            fireToast("Candidate successfully added", "success");
        } catch (error) {
            handleAuthError(error, navigate);
            dispatch({ type: "SET_ERROR" });
        }
    };

    const getAllCandidates = async (navigate) => {
        dispatch({type : "SET_LOADING"});
        try {
            const data = await getCandidates(headers);
            dispatch({ type: "GET_CANDIDATES", payload: data});
        } catch (error) {
            handleAuthError(error, navigate);
            dispatch({ type: "SET_ERROR" });
        }
    }

    const editCandidate = async (id, data1, file, navigate) => {     
        try {
            
            let bodyFormData = new FormData();
            bodyFormData.append("FullName", data1.FullName);
            bodyFormData.append("Password", data1.Password);
            bodyFormData.append("Email", data1.Email);
            bodyFormData.append("Phone", data1.Phone);
            bodyFormData.append("YearsOfExperience", data1.YearsOfExperience);
            bodyFormData.append("Resume", file);

            let headers1 = headers;
            headers1['Content-Type'] = "multipart/form-data";

            const data = await updateCandidate(id, bodyFormData, headers1);
            if(data != undefined && data != null) {
                getAllCandidates(navigate);
                fireToast("Update Candidate successfully", "success");
            }
        } catch (error) {
            handleAuthError(error, navigate);
        }
    }

    const removeCandidate = async (id, navigate) => {
        dispatch({type : "SET_LOADING"});
        try {
            const data = await deleteCandidate(id, headers);
            dispatch({ type: "DELETE_CANDIDATE", payload: id});
            
        } catch (error) {
            handleAuthError(error, navigate);
            dispatch({ type: "SET_ERROR" });
        }
    }

    const searchCandidate = (searchTerm) => {
        dispatch({ type: "SEARCH_CANDIDATE", payload: searchTerm });
    };

    const handleAuthError = (error, navigate) => {
        if (error?.response?.status === 401 || error?.response?.status === 403) {
            logout();
            fireToast("Unauthorized access", "error");
            navigate("/");
        } else {
            fireToast(error?.response?.data, "error");
        }

    };

    return (
        <CandidateContext.Provider value={{ ...state, addCandidate, getAllCandidates, searchCandidate, editCandidate, removeCandidate }}>
            {children}
        </CandidateContext.Provider>
    );
};
