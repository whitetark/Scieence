import React from 'react';
import { useAuthContext } from '../../app/store/auth-context';
import { useLogout } from '../../hooks/use-auth';
import * as Styled from '../../styles/Modal.styled';
import Button from '../UI/Button';

const Settings = ({ onClick, onToggle }) => {
  const { userData } = useAuthContext();
  const { mutateAsync: logout } = useLogout();
  const changePasswordHandler = () => {
    onToggle('changepassword');
  };

  return (
    <Styled.ModalWrapper onClick={onClick}>
      <Styled.ModalHeader>
        <h2>{userData.username}</h2>
      </Styled.ModalHeader>
      <Styled.ModalMain>
        <Button onClick={changePasswordHandler}>Change Password</Button>
        <Button onClick={async () => await logout()}>Log out</Button>
      </Styled.ModalMain>
    </Styled.ModalWrapper>
  );
};

export default Settings;
