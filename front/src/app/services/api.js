import axios from 'axios';

const baseURL = 'http://localhost:7253';

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
      let apiResponse = await UserService.refreshToken({
        withCredentials: true,
      });
      localStorage.setItem('token', JSON.stringify(apiResponse.data.token));
      error.config.headers['Authorization'] = `Bearer ${apiResponse.data.token}`;
      return axios.request(error.config);
    }
    return Promise.reject(error);
  },
);

export const UserService = {
  async login(payload) {
    return api.post('/Auth/login', payload);
  },
  async register(payload) {
    return api.post('/Auth/register', payload);
  },
  async logout() {
    return api.post('/Auth/logout');
  },
  async fetchUserData() {
    return api.get('/Acc/getByUsername');
  },
  async removePublication(payload) {
    return api.put('/Acc/removePublicationFromAccount', payload);
  },
  async addPublication(payload) {
    return api.put('/Acc/addPublicationToAccount', payload);
  },
  async checkCredentials(payload) {
    return api.post('/Auth/checkCredentials', payload);
  },
  async changePassword(payload) {
    return api.patch('/Acc/changePassword', payload);
  },
  async getStatus() {
    return api.get('/Auth/getStatus');
  },
  async refreshToken(payload) {
    return api.post('/Auth/refresh-token', payload);
  },
};

export const PubService = {
  async getPubsByKeyword(payload) {
    return api.get(
      '/Aggregation/getByKeyword' +
        `?query=${payload.Query}` +
        `&lang=${payload.Language}` +
        `&year=${payload.Year[0]}` +
        `&year=${payload.Year[1]}`,
    );
  },
  async getPubsByAuthor(payload) {
    return api.get(
      '/Aggregation/getByAuthor' +
        `?query=${payload.Query}` +
        `&lang=${payload.Language}` +
        `&year=${payload.Year[0]}` +
        `&year=${payload.Year[1]}`,
    );
  },
  async getPubsBySubject(payload) {
    return api.get(
      '/Aggregation/getBySubject' +
        `?query=${payload.Query}` +
        `&lang=${payload.Language}` +
        `&year=${payload.Year[0]}` +
        `&year=${payload.Year[1]}`,
    );
  },
};
export default api;
