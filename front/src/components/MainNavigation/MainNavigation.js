import React, { useState } from 'react'
import logo from '../../assets/logo.png'
import { Header, Logo, Nav, Actions } from '../styles/MainNavigation.styled'
import { ContainerWrapper } from '../styles/UI.styled'
import { NavLink } from 'react-router-dom'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import Modal from '../UI/Modal'
import Login from './Login'
import Register from './Register'
import useModal from '../../hooks/modal'

const MainNavigation = () => {
  const { isShowing, toggle } = useModal()
  const [modalName, setModalName] = useState('login')

  const toggleModalName = (name) => {
    setModalName(name)
  }

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
              {modalName === 'login' ? (
                <Login onToggle={toggleModalName} />
              ) : (
                <Register onToggle={toggleModalName} />
              )}
            </Modal>
          </Actions>
        </Nav>
      </ContainerWrapper>
    </Header>
  )
}

export default MainNavigation
