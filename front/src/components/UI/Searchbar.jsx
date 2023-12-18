import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import React, { useRef, useState } from 'react';

import * as Styled from '../../styles/Searchbar.styled';

const Searchbar = (props) => {
  const queryRef = useRef();
  const [type, setType] = useState('keyword');

  const handleChange = (e) => {
    setType(e.target.value);
  };

  const handleButton = (e) => {
    e.preventDefault();
    const value = queryRef.current.value;
    props.handleSubmit({ value, type });
  };

  return (
    <Styled.Searchbar>
      <Styled.SearchbarDiv>
        <Styled.SearchButton type='submit' onClick={handleButton}>
          <FontAwesomeIcon icon='fa-solid fa-magnifying-glass' fixedWidth />
        </Styled.SearchButton>
        <input
          type='text'
          name='search'
          ref={queryRef}
          placeholder='What you need to find?'
          autoComplete='off'
        />
        <Styled.TypeSelect value={type} onChange={handleChange}>
          <option value='keyword'>Keyword</option>
          <option value='author'>Author</option>
          <option value='subject'>Subject</option>
        </Styled.TypeSelect>
      </Styled.SearchbarDiv>
    </Styled.Searchbar>
  );
};

export default Searchbar;
