import axios from 'axios';

const baseURL = 'https://localhost:7253';

const api = axios.create({
  baseURL,
  headers: {
    'Content-Type': 'application/json',
  },
  withCredentials: true,
});

api.interceptors.request.use((config) => {
  let token = JSON.parse(localStorage.getItem('token'));
  config.headers['Authorization'] = `Bearer ${token}`;
  return config;
});

api.interceptors.response.use(
  (response) => {
    return response;
  },
  async (error) => {
    if (error.response.status === 401) {
      let apiResponse = await api.post('/Acc/refresh-token', {
        withCredentials: true,
      });
      localStorage.setItem('token', JSON.stringify(apiResponse.data.token));
      error.config.headers['Authorization'] = `Bearer ${apiResponse.data.token}`;
      return axios.request(error.config);
    } else {
      return Promise.reject(error);
    }
  },
);
export default api;
