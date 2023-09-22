import React from 'react'
import logo from '../../assets/logo.png'
import { Header, Logo, Nav, Actions } from '../styles/MainNavigation.styled'
import { ContainerWrapper } from '../styles/UI.styled'
import { NavLink, useLocation } from 'react-router-dom'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'

const MainNavigation = () => {
  const location = useLocation()
  return (
    <Header>
      <ContainerWrapper>
        <Nav>
          <NavLink to='/' end>
            <Logo>
              <img src={logo} alt='Scieence Logo' />
              <h1>Scieence</h1>
            </Logo>
          </NavLink>
          <Actions>
            <NavLink to='fav' className={({ isActive }) => (isActive ? 'active' : undefined)}>
              <FontAwesomeIcon icon='fa-solid fa-heart' fixedWidth />
            </NavLink>
            <NavLink
              to='login'
              className={({ isActive }) => (isActive ? 'active' : undefined)}
              state={{ previousLocation: location }}
            >
              <FontAwesomeIcon icon='fa-solid fa-user' fixedWidth />
            </NavLink>
          </Actions>
        </Nav>
      </ContainerWrapper>
    </Header>
  )
}

export default MainNavigation
