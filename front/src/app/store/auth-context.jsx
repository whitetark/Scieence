import { createContext, useContext, useState } from 'react';
import { useQuery } from 'react-query';
import { UserService } from '../services/api';

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

  useQuery('user data', () => UserService.fetchUserData(), {
    onSuccess: ({ data }) => {
      setUserData(data);
    },
    onError: (error) => {
      console.log('Fetching Data error', error.message);
      setUserData(null);
    },
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

  return (
    <AuthContext.Provider value={{ userToken, userData, setUserData, setUserToken }}>
      {children}
    </AuthContext.Provider>
  );
};

export const useAuthContext = () => {
  const context = useContext(AuthContext);
  if (context === undefined) {
    throw new Error('useAuthContext must be used within a ThemeContextProvider');
  }
  return context;
};
export default AuthContext;
