import axios from 'axios';

const API_URL = import.meta.env.VITE_API_URL + '/JobApplication';

const updateApplicationStatus = async (applicationId, statusId, headers) => {
    const response = await axios.put(`${API_URL}/${applicationId}/${statusId}`,null, {headers});
    return response?.data;
}

export default {updateApplicationStatus}