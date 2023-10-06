import React from 'react';

import * as Styled from '../../styles/Login.styled';

const Register = (props) => {
  return (
    <Styled.LoginWrapper onClick={props.onClick}>
      <Styled.LoginHeader>
        <h2>Register Form</h2>
      </Styled.LoginHeader>
      <Styled.LoginMain>
        <form action=''>
          <input type='text' placeholder='Login' />
          <input type='password' placeholder='Password' />
          <input type='password' placeholder='Repeat Password' />
          <button type='submit'>Register</button>
        </form>
        <div className='login-info'>
          Already a member? <button onClick={() => props.onToggle('login')}>Login in</button>
        </div>
      </Styled.LoginMain>
    </Styled.LoginWrapper>
  );
};

export default Register;
