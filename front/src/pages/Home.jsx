import React from 'react';
import { useNavigate } from 'react-router-dom';

import Hero from '../components/Home/Hero';
import Background from '../components/UI/Background';
import Searchbar from '../components/UI/Searchbar';
import * as Styled from '../styles/Home.styled';
import { Main } from '../styles/UI.styled';

const HomePage = () => {
  const navigate = useNavigate();
  const onClickHandler = () => {
    navigate('/search');
  };

  return (
    <Main>
      <Background />
      <Styled.Home>
        <Hero />
        <button className='advanced' onClick={onClickHandler}>
          Advanced Search
        </button>
        <Searchbar />
      </Styled.Home>
    </Main>
  );
};

export default HomePage;