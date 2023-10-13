import { createAsyncThunk } from '@reduxjs/toolkit';
import { getToken, removeToken, setToken } from '../../utils/helper';
import api from '../../services/api';
import history from '../../utils/history';

export const fetchUserData = createAsyncThunk(
  'auth/fetchUserData',
  async (_, { rejectWithValue }) => {
    try {
      const token = getToken();
      api.defaults.headers.Authorization = `Bearer ${token}`;
      const response = await api.get('Acc/getById');
      return { ...response.data, token };
    } catch (e) {
      removeToken();
      return rejectWithValue('');
    }
  },
);

export const login = createAsyncThunk('auth/login', async (payload) => {
  try {
    const response = await api.post('Acc/auth', payload);
    console.log(response);
    setToken(response.data.token);
    //history.push('/');
    return response.data;
  } catch (e) {
    console.log(e);
  }
});

export const signOut = createAsyncThunk('auth/signOut', async () => {
  removeToken();
});
