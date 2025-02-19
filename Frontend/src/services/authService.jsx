import axios from 'axios';

const API_URL = import.meta.env.VITE_API_URL + '/Authentication';
const API_URL1 = import.meta.env.VITE_API_URL + '/Role';

const loginUser1 = async (data) => {
  const response = await axios.post(`${API_URL}/login`, data);
  return response?.data; 
};

const registerUser = async (date, headers) => {
  const response = await axios.post(`${API_URL}/register`, date, { headers });
  return response?.data;
}

const getUsers = async (headers) => {
  const response = await axios.get(`${API_URL}/getAllUsers`, { headers });
  return response?.data;
}

const updateUser = async (id, data, headers) => {
  const response = await axios.put(`${API_URL}/updateUser/${id}`, data, {headers});
  return response?.data;
}

const deleteUser = async (id, headers) => {
  const response = await axios.delete(`${API_URL}/deleteUser/${id}`, {headers});
  return response?.data;
}

const getAllRoles = async (headers) => {
  const response = await axios.get(`${API_URL1}`, {headers});
  return response?.data;
}

export default { loginUser1, registerUser, getUsers, updateUser, deleteUser, getAllRoles};