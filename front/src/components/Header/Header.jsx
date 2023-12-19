import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import React, { useContext, useState } from 'react';
import { NavLink } from 'react-router-dom';

import AuthContext from '../../app/store/auth-context';
import logo from '../../assets/logo.png';
import useModal from '../../hooks/use-modal';
import * as Styled from '../../styles/Header.styled';
import Modal from '../UI/Modal';
import ChangePasswordPreview from './ChangePasswordPreview';
import Login from './Login';
import Register from './Register';
import Settings from './Settings';

const MainNavigation = () => {
  const { userData } = useContext(AuthContext);
  const { isShowing: modalIsShowing, toggle: toggleModal } = useModal();
  const [authModalName, setAuthModalName] = useState('login');
  const [userModalName, setUserModalName] = useState('settings');

  let nonTokenModalContent =
    authModalName === 'login' ? (
      <Login onToggle={setAuthModalName} onHide={toggleModal} />
    ) : (
      <Register onToggle={setAuthModalName} onHide={toggleModal} />
    );

  let tokenModalContent =
    userModalName === 'settings' ? (
      <Settings onToggle={setUserModalName} onHide={toggleModal} />
    ) : (
      <ChangePasswordPreview onToggle={setUserModalName} onHide={toggleModal} />
    );

  return (
    <Styled.Header>
      <Styled.Nav>
        <Styled.NavLogo to='/' end>
          <img src={logo} alt='Scieence Logo' />
          <h1>Scieence</h1>
        </Styled.NavLogo>
        <Styled.Actions>
          {userData ? (
            <NavLink to='/favorite' className={({ isActive }) => (isActive ? 'active' : undefined)}>
              <FontAwesomeIcon icon='fa-solid fa-heart' fixedWidth />
            </NavLink>
          ) : undefined}
          <button onClick={toggleModal} className={modalIsShowing ? 'active' : undefined}>
            <FontAwesomeIcon icon='fa-solid fa-user' fixedWidth />
          </button>
          <Modal isShowing={modalIsShowing} hide={toggleModal} className='login-modal'>
            {userData ? tokenModalContent : nonTokenModalContent}
          </Modal>
        </Styled.Actions>
      </Styled.Nav>
    </Styled.Header>
  );
};

export default MainNavigation;
