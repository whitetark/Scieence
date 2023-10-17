import { createContext, useState } from 'react';
import jwt_decode from 'jwt-decode';
import { useNavigate } from 'react-router-dom';
import api from '../services/api';

const AuthContext = createContext();

export const AuthContextProvider = ({ children }) => {
  const [userToken, setUserToken] = useState(() => {
    if (localStorage.getItem('tokens')) {
      let tokens = JSON.parse(localStorage.getItem('tokens'));
      return jwt_decode(tokens.access_token);
    }
    return null;
  });

  const navigate = useNavigate();

  const login = async (payload) => {
    const response = await api.post('/auth/login', payload);
    localStorage.setItem('tokens', JSON.stringify(response.data));
    setUserToken(jwt_decode(response.data.access_token));
    navigate('/');
  };

  const logout = async () => {
    localStorage.removeItem('tokens');
    setUserToken(null);
    navigate('/');
  };
  return (
    <AuthContext.Provider value={{ userToken, login, logout }}>{children}</AuthContext.Provider>
  );
};

export default AuthContext;
