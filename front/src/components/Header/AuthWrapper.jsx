import React from 'react';
import * as Styled from '../../styles/Auth.styled';

const AuthWrapper = ({ children, onClick, onToggle, type }) => {
  return (
    <Styled.AuthWrapper onClick={onClick}>
      <Styled.AuthHeader>
        <h2>{type} Form</h2>
      </Styled.AuthHeader>
      <Styled.AuthMain>
        {children}
        <Styled.AuthInfo>
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
        </Styled.AuthInfo>
      </Styled.AuthMain>
    </Styled.AuthWrapper>
  );
};

export default AuthWrapper;
