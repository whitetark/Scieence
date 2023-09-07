import React from 'react'
import logo from '../assets/logo.png'
import { styled } from 'styled-components'

const MainNavigation = () => {
  const Header = styled.header`
    display: flex;
    flex-direction: row;
  `

  const Logo = styled.div`
    display: flex;
    flex-direction: row;
    align-self: flex-start;
  `

  return (
    <header>
      <Logo>
        <img src={logo} alt='' />
        <h1>Scieence</h1>
      </Logo>
      <nav></nav>
    </header>
  )
}

export default MainNavigation
