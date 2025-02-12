import axios from 'axios';

const API_URL = import.meta.env.VITE_API_URL + '/Skill';

const addSkill = async (data, headers) => {
    const response = await axios.post(`${API_URL}`, data, {headers});
    return response?.data;
}

const getSkills = async (headers) => {
    const response = await axios.get(`${API_URL}`, {headers});
    return response?.data;
}

const updateSkill = async (id, data, headers) => {
    const response = await axios.put(`${API_URL}/${id}`, data, {headers});
    return response?.data;
}

const deleteSkill = async (id, headers) => {
    const response = await axios.delete(`${API_URL}/${id}`, {headers});
    return response?.data;
}

export default {addSkill, getSkills, updateSkill, deleteSkill}