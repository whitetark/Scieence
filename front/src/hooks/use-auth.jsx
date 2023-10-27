import { useMutation } from 'react-query';
import { UserService } from '../app/services/api';
import { useAuthContext } from '../app/store/auth-context';

export const useLogin = () => {
  const { setUserData, setUserToken } = useAuthContext();
  return useMutation('user login', (payload) => UserService.login(payload), {
    onSuccess: ({ data }) => {
      console.log(data);
      localStorage.setItem('token', JSON.stringify(data.token));
      setUserToken(data.token);
      setUserData(data.user);
    },
    onError: (error) => {
      console.log('Login error: ' + error);
    },
  });
};

export const useRegister = () => {
  const { setUserData, setUserToken } = useAuthContext();
  return useMutation('user register', (payload) => UserService.register(payload), {
    onSuccess: ({ data }) => {
      setUserData(data.user);
      setUserToken(data.token);
    },
    onError: (error) => {
      console.log('Register error: ' + error);
    },
  });
};

export const useLogout = () => {
  const { setUserToken } = useAuthContext();
  return useMutation('user logout', () => UserService.logout(), {
    onSuccess: () => {
      localStorage.removeItem('token');
      setUserToken(null);
    },
    onError: (error) => {
      console.log('Logout error: ' + error.message);
    },
  });
};
