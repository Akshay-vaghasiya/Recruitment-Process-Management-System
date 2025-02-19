import axios from 'axios';

const API_URL = import.meta.env.VITE_API_URL + '/Candidate';
const API_URL1 = import.meta.env.VITE_API_URL + '/Document';
const API_URL2 = import.meta.env.VITE_API_URL + '/DocumentType';
const API_URL3 = import.meta.env.VITE_API_URL + '/DocumentStatus';

const registerCandidate = async (data, headers) => {
    const response = await axios.post(`${API_URL}`, data, {headers});
    return response?.data;
}

const getCandidates = async (headers) => {
    const response = await axios.get(`${API_URL}`, {headers});
    return response?.data;
}

const updateCandidate = async (id, data, headers) => {
    const response = await axios.put(`${API_URL}/${id}`, data, {headers});
    return response?.data;
}

const deleteCandidate = async (id, headers) => {
    const response = await axios.delete(`${API_URL}/${id}`, {headers});
    return response?.data;
}

const updateDocumentStatus = async (documentId, statusId, headers) => {
    const response = await axios.put(`${API_URL1}/${documentId}/${statusId}`,null, {headers});
    return response?.data;
}

const addDocument = async (candidateId, typeId, data, headers) => {
    const response = await axios.post(`${API_URL1}/${candidateId}/${typeId}`, data, {headers});
    return response?.data;
}

const updateDocument = async (documentId, data, headers) => {
    const response = await axios.put(`${API_URL1}/${documentId}`, data, {headers});
    return response?.data;
}

const deleteDocument = async (documentId, headers) => {
    const response = await axios.delete(`${API_URL1}/${documentId}`, {headers});
    return response?.data;
}

const getCandidateDocument = async (candidateId, headers) => {
    const response = await axios.get(`${API_URL1}/${candidateId}`, {headers});
    return response?.data;
}

const getDocumentType = async (headers) => {
    const response = await axios.get(`${API_URL2}`, {headers});
    return response?.data;
}

const bulkCandidateUpload = async (data, headers) => {
    const response = await axios.post(`${API_URL}/upload`, data, {headers});
    return response?.data;
}

const candidateLogin = async (data) => {
    const response = await axios.post(`${API_URL}/login`, data);
    return response?.data;
}

const getCandidateSkills = async (candidateId, headers) => {
    const response = await axios.get(`${API_URL}/getCandidateSkill/${candidateId}`, {headers});
    return response?.data;
}

const addCandidateSkill = async (candidateId, skillId, yearOfExperience, headers) => {
    const response = await axios.post(`${API_URL}/addCandidateSkill/${candidateId}/${skillId}/${yearOfExperience}`, null, {headers})
    return response?.data;
}

const deleteCandidateSkill = async (candidateSkillId, headers) => {
    const response = await axios.delete(`${API_URL}/deleteCandidateSkill/${candidateSkillId}`, {headers});
    return response?.data; 
}

const getDocumentStatus = async () => {
    const response = await axios.get(`${API_URL3}`);
    return response?.data;
}

export default {registerCandidate, getCandidates, updateCandidate, deleteCandidate,
    updateDocumentStatus, bulkCandidateUpload, candidateLogin, getDocumentType,
    addCandidateSkill, deleteCandidateSkill, addDocument, updateDocument, deleteDocument,
    getCandidateDocument, getCandidateSkills, getDocumentStatus}