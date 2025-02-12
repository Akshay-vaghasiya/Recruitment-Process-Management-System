import axios from 'axios';

const API_URL = import.meta.env.VITE_API_URL + '/JobPosition';

const addJobPosition = async (data, headers) => {
    const response = await axios.post(`${API_URL}`, data, {headers});
    return response?.data;
}

const getAllJobPosition = async (headers) => {
    const response = await axios.get(`${API_URL}`, {headers});
    return response?.data;
}

const updateJobPosition = async (id, data, headers) => {
    const response = await axios.put(`${API_URL}/${id}`, data, {headers});
    return response?.data;
}

const deleteJobPosition = async (id, headers) => {
    const response = await axios.delete(`${API_URL}/${id}`, {headers});
    return response?.data;
}

export default {addJobPosition, getAllJobPosition,updateJobPosition, deleteJobPosition};