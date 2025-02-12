import React, { createContext, useContext, useReducer, useState } from 'react';
import { useAuth } from './AuthContext';
import AuthHeader from '../helper/AuthHeader';
import { fireToast } from '../components/fireToast';
import jobPositionService from '../services/jobPositionService';
import JobPositionReducer from '../reducers/JobPositionReducer';

const JobPositionContext = createContext();

const initialState = {
    isLoading: false,
    isError: false,
    jobPositions: [],
    filteredJobPositions: [],
    searchTerm: ""
};
export const useJobPositionContext = () => useContext(JobPositionContext);

export const JobPositionProvider = ({ children }) => {

    const { addJobPosition, getAllJobPosition, updateJobPosition, deleteJobPosition } = jobPositionService;
    const { logout } = useAuth();

    const headers = AuthHeader();

    const [state, dispatch] = useReducer(JobPositionReducer, initialState);

    const addJob = async (data1, navigate) => {
        dispatch({ type: "SET_LOADING" });
        try {
            const data = await addJobPosition(data1, headers);
            dispatch({ type: "ADD_JOB_POSITION", payload: data });
            fireToast("Job successfully added", "success");
        } catch (error) {
            handleAuthError(error, navigate);
            dispatch({ type: "SET_ERROR" });
        }
    };

    const getAllJob = async (navigate) => {
        dispatch({ type: "SET_LOADING" });
        try {
            const data = await getAllJobPosition(headers);
            dispatch({ type: "GET_JOB_POSITONS", payload: data });
        } catch (error) {
            handleAuthError(error, navigate);
            dispatch({ type: "SET_ERROR" });
        }
    }

    const editJob = async (id, data1, navigate) => {
        try {
            const obj = {
                Title: data1?.Title,
                Description: data1?.Description,
                FkStatusId: data1?.FkStatusId,
                FkReviewerId: data1?.FkReviewerId,
                RequireSkills: data1?.RequireSkills,
                Skills: data1?.Skills,
                ClosureReason: data1?.ClosureReason,
                FkSelectedCandidateId: data1?.FkSelectedCandidateId
            }

            const data = await updateJobPosition(id, obj, headers);
            if (data != undefined && data != null) {
                getAllJob(navigate);
                fireToast("Update job position successfully", "success");
            }
        } catch (error) {
            handleAuthError(error, navigate);
        }
    }

    const removeJob = async (id, navigate) => {
        dispatch({ type: "SET_LOADING" });
        try {
            const data = await deleteJobPosition(id, headers);
            dispatch({ type: "DELETE_JOB_POSITION", payload: id });

            if (data != null) {
                await getAllJob(navigate);
            }
        } catch (error) {
            handleAuthError(error, navigate);
            dispatch({ type: "SET_ERROR" });
        }
    }

    const searchJob = (searchTerm) => {
        dispatch({ type: "SEARCH_JOB_POSITION", payload: searchTerm });
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
        <JobPositionContext.Provider value={{ ...state, addJob, getAllJob, searchJob, editJob, removeJob }}>
            {children}
        </JobPositionContext.Provider>
    );
};
