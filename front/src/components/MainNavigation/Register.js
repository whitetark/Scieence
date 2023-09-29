import React from 'react'
import { LoginHeader, LoginWrapper, LoginMain } from '../styles/Login.styled'

const Register = (props) => {
  return (
    <LoginWrapper onClick={props.onClick}>
      <LoginHeader>
        <h2>Register Form</h2>
      </LoginHeader>
      <LoginMain>
        <form action=''>
          <input type='text' placeholder='Login' />
          <input type='password' placeholder='Password' />
          <input type='password' placeholder='Repeat Password' />
          <button type='submit'>Register</button>
        </form>
        <div className='login-info'>
          Already a member? <button onClick={() => props.onToggle('login')}>Login in</button>
        </div>
      </LoginMain>
    </LoginWrapper>
  )
}

export default Register
