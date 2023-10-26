import axios from 'axios';

const baseURL = 'http://localhost:7253';

const api = axios.create({
  baseURL,
  headers: {
    'Content-Type': 'application/json',
  },
  withCredentials: true,
});

export const UserService = {
  async login(payload) {
    return api.post('/Acc/login', payload);
  },
  async register(payload) {
    return api.post('/Acc/register', payload);
  },
  async logout() {
    return api.post('/Acc/logout');
  },
  async fetchUserData() {
    return api.get('/Acc/getByUsername');
  },
};

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
