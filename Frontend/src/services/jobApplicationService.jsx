import axios from 'axios';

const API_URL = import.meta.env.VITE_API_URL + '/JobApplication';
const API_URL1 = import.meta.env.VITE_API_URL + '/ApplicationStatus';

const updateApplicationStatus = async (applicationId, statusId, headers) => {
    const response = await axios.put(`${API_URL}/${applicationId}/${statusId}`,null, {headers});
    return response?.data;
}

const createJobApplication = async (candidateId, jobPositionId, headers) => {
    const response = await axios.post(`${API_URL}/${candidateId}/${jobPositionId}`, null, {headers});
    return response?.data;
}

const deleteJobAppliction = async (applicationId, headers) => {
    const response = await axios.delete(`${API_URL}/${applicationId}`, {headers});
    return response?.data;
}

const getApplicationStatus = async () => {
    const response = await axios.get(`${API_URL1}`);
    return response?.data;
}

export default {updateApplicationStatus, createJobApplication, deleteJobAppliction, getApplicationStatus}