import React, { createContext, useContext, useReducer } from 'react';
import SkillReducer from '../reducers/SkillReducer';
import { useAuth } from './AuthContext';
import AuthHeader from '../helper/AuthHeader';
import { fireToast } from '../components/fireToast';
import skillService from '../services/skillService';

const SkillContext = createContext();

const initialState = {
    isLoading: false,
    isError: false,
    skills: [],
    filteredSkills: [],
    searchTerm: ""
};

export const useSkillContext = () => useContext(SkillContext);

export const SkillProvider = ({ children }) => {

    const { addSkill, getSkills, updateSkill, deleteSkill } = skillService;
    const { logout } = useAuth();

    const headers = AuthHeader();

    const [state, dispatch] = useReducer(SkillReducer, initialState);

    const addSkill1 = async (data1, navigate) => {
        dispatch({ type: "SET_LOADING" });
        try {
            const data = await addSkill(data1, headers);
            dispatch({ type: "ADD_SKILL", payload: data });
            fireToast("Skill successfully added", "success");
        } catch (error) {
            handleAuthError(error, navigate);
            dispatch({ type: "SET_ERROR" });
        }
    };

    const getAllSkills = async (navigate) => {
        dispatch({ type: "SET_LOADING" });
        try {
            const data = await getSkills(headers);
            dispatch({ type: "GET_SKILLS", payload: data });
        } catch (error) {
            handleAuthError(error, navigate);
            dispatch({ type: "SET_ERROR" });
        }
    }

    const editSkill = async (id, data1, navigate) => {
        dispatch({ type: "SET_LOADING" });
        try {
            const data = await updateSkill(id, data1, headers);
            if (data != undefined && data != null) {
                getAllSkills(navigate);
                fireToast("Update Candidate successfully", "success");
            }
        } catch (error) {
            handleAuthError(error, navigate);
            dispatch({ type: "SET_ERROR" });
        }
    }

    const removeSkill = async (id, navigate) => {
        dispatch({ type: "SET_LOADING" });
        try {
            const data = await deleteSkill(id, headers);
            dispatch({ type: "DELETE_SKILL", payload: id });

        } catch (error) {
            handleAuthError(error, navigate);
            dispatch({ type: "SET_ERROR" });
        }
    }

    const searchSkill = (searchTerm) => {
        dispatch({ type: "SEARCH_SKILL", payload: searchTerm });
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
        <SkillContext.Provider value={{ ...state, addSkill1, getAllSkills, searchSkill, editSkill, removeSkill }}>
            {children}
        </SkillContext.Provider>
    );
};
