import React from 'react';
import * as Styled from '../../styles/Modal.styled';

const AuthWrapper = ({ children, onClick, onToggle, type }) => {
  return (
    <Styled.ModalWrapper onClick={onClick}>
      <Styled.ModalHeader>
        <h2>{type} Form</h2>
      </Styled.ModalHeader>
      <Styled.ModalMain>
        {children}
        <Styled.ModalInfo>
          {type === 'Login' ? (
            <>
              <span>Not a member? </span>
              <button onClick={() => onToggle('reg')}>Signup now</button>
            </>
          ) : (
            <>
              <span>Already a member? </span>
              <button onClick={() => onToggle('login')}>Login in</button>
            </>
          )}
        </Styled.ModalInfo>
      </Styled.ModalMain>
    </Styled.ModalWrapper>
  );
};

export default AuthWrapper;
