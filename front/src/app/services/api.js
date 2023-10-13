import axios from 'axios';

const baseURL = 'http://localhost:7253/';

const api = axios.create({
  baseURL,
  headers: { 'Content-Type': 'application/json' },
});

export default api;
