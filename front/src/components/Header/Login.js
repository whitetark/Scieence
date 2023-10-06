import React from 'react';

import * as Styled from '../../styles/Login.styled';

const Login = (props) => {
  return (
    <Styled.LoginWrapper onClick={props.onClick}>
      <Styled.LoginHeader>
        <h2>Login Form</h2>
      </Styled.LoginHeader>
      <Styled.LoginMain>
        <form action=''>
          <input type='text' placeholder='Login' />
          <input type='password' placeholder='Password' />
          <button type='submit'>Login</button>
        </form>
        <div className='login-info'>
          Not a member? <button onClick={() => props.onToggle('reg')}>Signup now</button>
        </div>
      </Styled.LoginMain>
    </Styled.LoginWrapper>
  );
};

export default Login;
