import React from 'react';

import Button from '../UI/Button';
import AuthWrapper from './AuthWrapper';

const Register = (props) => {
  return (
    <AuthWrapper onClick={props.onClick} onToggle={props.onToggle} type='Register'>
      <form action=''>
        <input type='text' placeholder='Login' />
        <input type='password' placeholder='Password' />
        <input type='password' placeholder='Repeat Password' />
        <Button type='submit'>Register</Button>
      </form>
    </AuthWrapper>
  );
};

export default Register;
