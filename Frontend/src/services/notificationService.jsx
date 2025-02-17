import axios from 'axios';

const API_URL = import.meta.env.VITE_API_URL + '/Notification';
const API_URL1 = import.meta.env.VITE_API_URL + '/CandidateNotification';

const getCandidateNotification = async (candidateId, headers) => {
    const response = await axios.get(`${API_URL1}/${candidateId}`, { headers} );
    return response?.data;
}

const updateCandidateNotification = async (notificationId, data, headers) => {
    const response = await axios.put(`${API_URL1}/${notificationId}`, data, {headers});
    return response?.data;
}


const updateNotification = async (notificationId, data, headers) => {
    const response = await axios.put(`${API_URL}/${notificationId}`, data, {headers});
    return response?.data;
}

const getNotification = async (userId, headers) => {
    const response = await axios.get(`${API_URL}/${userId}`, { headers} );
    return response?.data;
}

export default {getCandidateNotification, getNotification, updateNotification, updateCandidateNotification};