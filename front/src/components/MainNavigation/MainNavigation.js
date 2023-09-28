import React from 'react'
import logo from '../../assets/logo.png'
import { Header, Logo, Nav, Actions } from '../styles/MainNavigation.styled'
import { ContainerWrapper } from '../styles/UI.styled'
import { NavLink, useLocation } from 'react-router-dom'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import Modal from '../UI/Modal'
import Login from './Login'
import useModal from '../../hooks/modal'

const MainNavigation = () => {
  const { isShowing, toggle } = useModal()
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
            <button onClick={toggle} className={isShowing ? 'active' : undefined}>
              <FontAwesomeIcon icon='fa-solid fa-user' fixedWidth />
            </button>
            <Modal isShowing={isShowing} hide={toggle} className='login-modal'>
              <Login />
            </Modal>
          </Actions>
        </Nav>
      </ContainerWrapper>
    </Header>
  )
}

export default MainNavigation
