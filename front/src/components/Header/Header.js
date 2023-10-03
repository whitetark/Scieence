import React, { useState } from 'react'
import logo from '../../assets/logo.png'
import * as Styled from '../../styles/Header.styled'
import { NavLink } from 'react-router-dom'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import Modal from '../UI/Modal'
import Login from './Login'
import Register from './Register'
import useModal from '../../hooks/use-modal'

const MainNavigation = () => {
  const { isShowing, toggle } = useModal()
  const [modalName, setModalName] = useState('login')

  const toggleModalName = (name) => {
    setModalName(name)
  }

  return (
    <Styled.Header>
      <Styled.Nav>
        <Styled.NavLogo to='/' end>
          <img src={logo} alt='Scieence Logo' />
          <h1>Scieence</h1>
        </Styled.NavLogo>
        <Styled.Actions>
          <NavLink to='/favorite' className={({ isActive }) => (isActive ? 'active' : undefined)}>
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
        </Styled.Actions>
      </Styled.Nav>
    </Styled.Header>
  )
}

export default MainNavigation
