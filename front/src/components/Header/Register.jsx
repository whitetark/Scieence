import { useContext } from 'react';

import AuthContext from '../../app/store/auth-context';
import Button from '../UI/Button';
import AuthWrapper from './AuthWrapper';
import useInput from '../../hooks/use-input';

const isNotEmpty = (value) => value.trim() !== '';

const Register = (props) => {
  const { register } = useContext(AuthContext);
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

  const repeatPasswordValidation = (value, passwordValue) => {
    return value === passwordValue;
  };

  const {
    value: repeatPasswordValue,
    isValid: repeatPasswordIsValid,
    hasError: repeatPasswordHasError,
    valueBlurHandler: repeatPasswordBlurHandler,
    valueChangeHandler: repeatPasswordChangeHandler,
    reset: repeatPasswordReset,
  } = useInput(repeatPasswordValidation.bind(null, passwordValue));

  let formIsValid = loginIsValid && passwordIsValid && repeatPasswordIsValid;

  const formSubmitHandler = async (event) => {
    event.preventDefault();

    if (!formIsValid) {
      return;
    }

    const user = {
      login: loginValue,
      password: passwordValue,
    };

    await register(user);

    props.onHide();

    loginReset();
    passwordReset();
    repeatPasswordReset();
  };
  return (
    <AuthWrapper onClick={props.onClick} onToggle={props.onToggle} type='Register'>
      <form onSubmit={formSubmitHandler}>
        <input
          type='text'
          className={loginHasError ? 'input-error' : undefined}
          placeholder='Login'
          value={loginValue}
          onChange={loginChangeHandler}
          onBlur={loginBlurHandler}
          name='login'
        />
        <input
          type='password'
          placeholder='Password'
          className={passwordHasError ? 'input-error' : undefined}
          value={passwordValue}
          onChange={passwordChangeHandler}
          onBlur={passwordBlurHandler}
          name='password'
        />
        <input
          disabled={!passwordIsValid}
          className={repeatPasswordHasError ? 'input-error' : undefined}
          type='password'
          placeholder='Repeat Password'
          value={repeatPasswordValue}
          onChange={repeatPasswordChangeHandler}
          onBlur={repeatPasswordBlurHandler}
          name='repeatPassword'
        />
        <Button disabled={!formIsValid} type='submit'>
          Register
        </Button>
      </form>
    </AuthWrapper>
  );
};

export default Register;
