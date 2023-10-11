import { createAsyncThunk } from '@reduxjs/toolkit';
import { getToken, removeToken, setToken } from '../../utils/HelperFunctions';
import api from '../../services/api';
import history from '../utils/history';

export const fetchUserData = createAsyncThunk('Acc/getById', async (_, { rejectWithValue }) => {
  try {
    const accessToken = getToken();
    api.defaults.headers.Authorization = `Bearer ${accessToken}`;
    const response = await api.get('/getById');
    return { ...response.data, accessToken };
  } catch (e) {
    removeToken();
    return rejectWithValue('');
  }
});

export const login = createAsyncThunk('Acc/auth', async (payload) => {
  const response = await api.post('/auth', payload);
  setToken(response.data.accessToken);
  history.push('/home');
  return response.data;
});

export const signOut = createAsyncThunk('Acc/signOut', async () => {
  removeToken();
});
