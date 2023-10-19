import React, { useContext, useState } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { NavLink } from 'react-router-dom';

import * as Styled from '../../styles/Header.styled';
import logo from '../../assets/logo.png';
import Modal from '../UI/Modal';
import Login from './Login';
import Register from './Register';
import useModal from '../../hooks/use-modal';
import AuthContext from '../../app/store/auth-context';

const MainNavigation = () => {
  const { userToken, logout } = useContext(AuthContext);
  const { isShowing, toggle } = useModal();
  const [modalName, setModalName] = useState('login');

  const toggleModalName = (name) => {
    setModalName(name);
  };

  return (
    <Styled.Header>
      <Styled.Nav>
        <Styled.NavLogo to='/' end>
          <img src={logo} alt='Scieence Logo' />
          <h1>Scieence</h1>
        </Styled.NavLogo>
        <Styled.Actions>
          {userToken ? (
            <NavLink to='/favorite' className={({ isActive }) => (isActive ? 'active' : undefined)}>
              <FontAwesomeIcon icon='fa-solid fa-heart' fixedWidth />
            </NavLink>
          ) : undefined}
          {!userToken ? (
            <button onClick={toggle} className={isShowing ? 'active' : undefined}>
              <FontAwesomeIcon icon='fa-solid fa-user' fixedWidth />
            </button>
          ) : (
            <button onClick={logout}>Logout</button>
          )}
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
  );
};

export default MainNavigation;
