import { createContext, useEffect, useState } from 'react';

import api from '../services/api';

const AuthContext = createContext();

export const AuthContextProvider = ({ children }) => {
  const [userToken, setUserToken] = useState(() => {
    if (localStorage.getItem('token')) {
      let token = JSON.parse(localStorage.getItem('token'));
      return token;
    }
    return null;
  });
  const [userData, setUserData] = useState([]);

  useEffect(() => {
    const fetchData = async () => {
      const response = await api.get('/Acc/getByUsername').catch((error) => {
        console.log(error);
        console.log('There has been a problem with your fetch operation: ' + error.message);
      });
    };
    fetchData();
  }, []);
  //const navigate = useNavigate();

  const login = async (payload) => {
    const response = await api.post('/Acc/login', payload).catch((error) => {
      console.log('There has been a problem with your fetch operation: ' + error.message);
    });
    localStorage.setItem('token', JSON.stringify(response.data.token));
    setUserToken(response.data.token);
    setUserData(response.data.user);
    //navigate('/');
  };

  const logout = async () => {
    localStorage.removeItem('token');
    setUserToken(null);
    //navigate('/');
  };

  const updateUserData = async (payload) => {
    setUserData(payload);
    const response = await api.put('/Acc/update', payload, {
      withCredentials: true,
    });
    console.log(response);
  };

  return (
    <AuthContext.Provider
      value={{ userToken, userData, login, logout, updateUserData, setUserData }}>
      {children}
    </AuthContext.Provider>
  );
};

export default AuthContext;
