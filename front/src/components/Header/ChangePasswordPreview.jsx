import React, { useState } from 'react';

import { useAuthContext } from '../../app/store/auth-context';
import * as Styled from '../../styles/Modal.styled';
import ChangePassword from './ChangePassword';
import ConfirmPassword from './ConfirmPassword';

const ChangePasswordPreview = ({ onClick, onToggle }) => {
  const { userData } = useAuthContext();

  const [isConfirmed, setIsConfirmed] = useState(false);
  const [isSuccess, setIsSuccess] = useState(false);

  const isSuccessContent = <Styled.Success>You succesfully changed password!</Styled.Success>;
  const isNotSuccessContent = isConfirmed ? (
    <ChangePassword setIsSuccess={setIsSuccess} username={userData.username} />
  ) : (
    <ConfirmPassword setIsConfirmed={setIsConfirmed} username={userData.username} />
  );

  return (
    <Styled.ModalWrapper onClick={onClick}>
      <Styled.ModalHeader>
        <h2>Change Password</h2>
      </Styled.ModalHeader>
      <Styled.ModalMain>
        {isSuccess ? isSuccessContent : isNotSuccessContent}
        <Styled.ModalInfo>
          <button onClick={() => onToggle('settings')}>Go Back</button>
        </Styled.ModalInfo>
      </Styled.ModalMain>
    </Styled.ModalWrapper>
  );
};

export default ChangePasswordPreview;
