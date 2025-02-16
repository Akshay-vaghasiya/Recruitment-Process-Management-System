import { hexToRgb } from '@mui/material';
import axios from 'axios';

const API_URL = import.meta.env.VITE_API_URL + '/Interview';
const API_URL1 = import.meta.env.VITE_API_URL + '/InterviewPanel';
const API_URL2 = import.meta.env.VITE_API_URL + '/InterviewFeedback';

const getInterviewByCandidateAndPosition = async (candidateId, jobId, headers) => {
    const response = await axios.get(`${API_URL}/${candidateId}/${jobId}`, {headers});
    return response?.data;
}

const addInterview = async (data, headers) => {
    const response = await axios.post(`${API_URL}`,data, {headers});
    return response?.data;
}

const getAllInterviews = async (headers) => {
    const response = await axios.get(`${API_URL}`, {headers});
    return response?.data;
}

const updateInterview = async (interviewId, data, headers) => {
    const response = await axios.put(`${API_URL}/${interviewId}`, data, {headers});
    return response?.data;
}

const deleteInterview = async (interviewId, headers) => {
    const response = await axios.delete(`${API_URL}/${interviewId}`, {headers});
    return response?.data;
}

const addInterviewPanel = async (interviewerId, interviewId, headers) => {
    const response = await axios.post(`${API_URL1}/${interviewerId}/${interviewId}`, null, {headers});
    return response?.data;
}

const deleteInterviewPanel = async (panelId, headers) => {
    const response = await axios.delete(`${API_URL1}/${panelId}`, {headers});
    return response?.data;
}

const getInterviewPanelByInterview = async (interviewId, headers) => {
    const response = await axios.get(`${API_URL1}/${interviewId}`, {headers});
    return response?.data;
}

const getInterviewFeedbackByUserAndInterview = async (userId, interviewId, headers) => {
    const response = await axios.get(`${API_URL2}/${userId}/${interviewId}`, {headers});
    return response?.data;
}

const addInterviewFeedback = async (data, headers) => {
    const response = await axios.post(`${API_URL2}`, data, {headers});
    return response?.data;
}

const updateInterviewFeedback = async (feedbackid, data, headers) => {
    const response = await axios.put(`${API_URL2}/${feedbackid}`, data, {headers});
    return response?.data;
}

export default {getInterviewByCandidateAndPosition, addInterview, getAllInterviews,
    deleteInterview, updateInterview, addInterviewPanel, deleteInterviewPanel,
    getInterviewPanelByInterview, getInterviewFeedbackByUserAndInterview,
    addInterviewFeedback, updateInterviewFeedback};