import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import React, { useState } from 'react';

import * as Styled from '../../styles/Searchbar.styled';

const Searchbar = (props) => {
  const [type, setType] = useState(true);
  const [query, setQuery] = useState('');
  const toggleButton = () => setType(!type);

  const handleButton = (e) => {
    e.preventDefault();
    props.handleSubmit({ query, type });
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
          value={query}
          placeholder='What you need to find?'
          autoComplete='off'
          onChange={(e) => setQuery(e.target.value)}
        />
        <Styled.TypeButton type='text' onClick={toggleButton}>
          {type ? 'Keyword' : 'Authors'}
        </Styled.TypeButton>
      </Styled.SearchbarDiv>
    </Styled.Searchbar>
  );
};

export default Searchbar;
