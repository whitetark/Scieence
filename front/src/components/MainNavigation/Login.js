import React from 'react'
import { LoginHeader, LoginWrapper, LoginMain } from '../styles/Login.styled'

const Login = (props) => {
  return (
    <LoginWrapper onClick={props.onClick}>
      <LoginHeader>
        <h2>Login Form</h2>
      </LoginHeader>
      <LoginMain>
        <form action=''>
          <input type='text' placeholder='Login' />
          <input type='text' placeholder='Password' />
          <button type='submit'>Login</button>
        </form>
        <div className='login-info'>
          Not a member? <button>Signup now</button>
        </div>
      </LoginMain>
    </LoginWrapper>
  )
}

export default Login
