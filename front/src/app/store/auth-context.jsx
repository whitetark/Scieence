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
  const [userData, setUserData] = useState(() => {
    api
      .get('/Acc/getByUsername')
      .then((response) => {
        return response.data.user;
      })
      .catch((error) => {
        console.log('Fetching Data error ', error.message);
        return null;
      });
  });

  // useEffect(() => {
  //   const updateData = async (payload) => {
  //     await api
  //       .put('/Acc/update', payload, {
  //         withCredentials: true,
  //       })
  //       .catch((error) => {
  //         console.log('Update User Data error', error.message);
  //       });
  //   };
  //   updateData(userData);
  // }, [userData]);

  const login = async (payload) => {
    await api
      .post('/Acc/login', payload)
      .then((response) => {
        localStorage.setItem('token', JSON.stringify(response.data.token));
        setUserToken(response.data.token);
        setUserData(response.data.user);
      })
      .catch((error) => {
        console.log('Login error: ' + error);
      });
  };

  const logout = async () => {
    await api
      .post('/Acc/logout')
      .then(() => {
        localStorage.removeItem('token');
        setUserToken(null);
      })
      .catch((error) => {
        console.log('Logout error: ' + error.message);
      });
  };

  const register = async (payload) => {
    await api
      .post('/Acc/register', payload, {
        withCredentials: true,
      })
      .then((response) => {
        setUserData(response.data.user);
        setUserToken(response.data.token);
      })
      .catch((error) => {
        console.log('Register error', error.message);
      });
  };

  return (
    <AuthContext.Provider value={{ userToken, userData, login, logout, register, setUserData }}>
      {children}
    </AuthContext.Provider>
  );
};

export default AuthContext;
