import React from 'react';
import { useDispatch, useSelector } from 'react-redux';

import Button from '../UI/Button';
import AuthWrapper from './AuthWrapper';
import useInput from '../../hooks/use-input';
import { getToken } from '../../app/utils/helper';
import { default as history } from '../../app/utils/history';
import { login } from '../../app/store/slices/authThunk';
import Loading from '../UI/Loading';

const isNotEmpty = (value) => value.trim() !== '';

const Login = (props) => {
  const dispatch = useDispatch();
  const { token, loading } = useSelector((state) => state.auth);

  // if (token || getToken()) {
  //   history.push('/');
  // }

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

  const formSubmitHandler = (event) => {
    event.preventDefault();

    if (!formIsValid) {
      return;
    }

    const user = {
      username: loginValue,
      password: passwordValue,
    };

    dispatch(login(user));

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
        {loading ? (
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
