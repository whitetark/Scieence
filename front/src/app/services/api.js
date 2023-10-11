import axios from 'axios';

const baseURL = 'http://localhost:7235/';

const api = axios.create({
  baseURL,
});

export default api;
