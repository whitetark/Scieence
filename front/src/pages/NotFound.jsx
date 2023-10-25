import React from 'react';
import { useNavigate } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';

import * as Styled from '../styles/NotFound.styled';

const NotFound = () => {
  const navigate = useNavigate();
  const clickHandler = () => {
    navigate('/');
  };

  return (
    <Styled.NotFound>
      <span>404. Not Found</span>
      <button onClick={clickHandler}>
        <FontAwesomeIcon icon='fa-solid fa-arrow-left' fixedWidth />
        Go Back
      </button>
    </Styled.NotFound>
  );
};

export default NotFound;
