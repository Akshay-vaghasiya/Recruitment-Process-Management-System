import axios from 'axios';

const API_URL = import.meta.env.VITE_API_URL + '/ResumeReview';

const updateReview = async (userid, reviewid, data, headers) => {
    const response = await axios.put(`${API_URL}/${userid}/${reviewid}`, data, { headers });
    return response?.data;
}

const addReview = async (userid, data, headers) => {
    const response = await axios.post(`${API_URL}/${userid}`, data, { headers});
    return response?.data;
}

export default {updateReview, addReview}