import { createContext, useContext, useState } from 'react';
import { useQuery } from 'react-query';
import { useAddPublication, useRemovePublication } from '../../hooks/use-auth';
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
  const [serverIsOn, setServerIsOn] = useState(true);
  const { mutateAsync: addOnServer } = useAddPublication();
  const { mutateAsync: removeFromServer } = useRemovePublication();

  useQuery('user data', () => UserService.fetchUserData(), {
    onSuccess: ({ data }) => {
      setUserData(data);
    },
    onError: (error) => {
      console.log('Fetching Data error', error.message);
      setUserData(null);
    },
  });

  useQuery('get status', () => UserService.getStatus(), {
    onSuccess: () => {
      setServerIsOn(true);
    },
    onError: () => {
      setServerIsOn(false);
    },
  });

  const addPublicationToUser = (newData) => {
    addOnServer(newData).then((data) => {
      setUserData(data.data);
    });
  };
  const removePublicationFromUser = (newData) => {
    removeFromServer(newData).then((data) => {
      setUserData(data.data);
    });
  };

  return (
    <AuthContext.Provider
      value={{
        userToken,
        userData,
        serverIsOn,
        setUserData,
        setUserToken,
        setServerIsOn,
        addPublicationToUser,
        removePublicationFromUser,
      }}>
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
