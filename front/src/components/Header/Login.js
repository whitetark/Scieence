import React, { useContext } from 'react';

import Button from '../UI/Button';
import AuthWrapper from './AuthWrapper';
import useInput from '../../hooks/use-input';
import Loading from '../UI/Loading';
import AuthContext from '../../app/store/auth-context';

const isNotEmpty = (value) => value.trim() !== '';

const Login = (props) => {
  const { login } = useContext(AuthContext);

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
        {false ? (
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
