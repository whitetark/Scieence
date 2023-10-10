import React from 'react';

import Button from '../UI/Button';
import AuthWrapper from './AuthWrapper';

const Login = (props) => {
  return (
    <AuthWrapper onClick={props.onClick} onToggle={props.onToggle} type='Login'>
      <form action=''>
        <input type='text' placeholder='Login' />
        <input type='password' placeholder='Password' />
        <Button type='submit'>Login</Button>
      </form>
    </AuthWrapper>
  );
};

export default Login;
