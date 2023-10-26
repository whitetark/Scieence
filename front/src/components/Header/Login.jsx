import React from 'react';

import Button from '../UI/Button';
import AuthWrapper from './AuthWrapper';
import useInput from '../../hooks/use-input';
import Loading from '../UI/Loading';
import { useLogin } from '../../hooks/use-auth';

const isNotEmpty = (value) => value.trim() !== '';

const Login = (props) => {
  const { mutateAsync: login, isLoading, error } = useLogin();

  const {
    value: loginValue,
    isValid: loginIsValid,
    hasError: loginHasError,
    valueBlurHandler: loginBlurHandler,
    valueChangeHandler: loginChangeHandler,
    reset: loginReset,
  } = useInput(isNotEmpty);

  const {
    value: passwordValue,
    isValid: passwordIsValid,
    hasError: passwordHasError,
    valueBlurHandler: passwordBlurHandler,
    valueChangeHandler: passwordChangeHandler,
    reset: passwordReset,
  } = useInput(isNotEmpty);

  let formIsValid = loginIsValid && passwordIsValid;

  const formSubmitHandler = async (event) => {
    event.preventDefault();

    if (!formIsValid) {
      return;
    }

    const user = {
      username: loginValue,
      password: passwordValue,
    };

    await login(user);

    props.onHide();
    loginReset();
    passwordReset();
  };

  return (
    <AuthWrapper onClick={props.onClick} onToggle={props.onToggle} type='Login'>
      <form onSubmit={formSubmitHandler}>
        <input
          type='text'
          placeholder='Login'
          value={loginValue}
          onChange={loginChangeHandler}
          onBlur={loginBlurHandler}
          name='login'
        />
        <input
          type='password'
          placeholder='Password'
          value={passwordValue}
          onChange={passwordChangeHandler}
          onBlur={passwordBlurHandler}
          name='password'
        />
        {isLoading ? (
          <Loading />
        ) : (
          <Button type='submit' disabled={!formIsValid}>
            Login
          </Button>
        )}
      </form>
    </AuthWrapper>
  );
};

export default Login;
