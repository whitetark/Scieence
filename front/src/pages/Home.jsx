import React from 'react';
import { useNavigate } from 'react-router-dom';

import Hero from '../components/Home/Hero';
import Background from '../components/UI/Background';
import Searchbar from '../components/UI/Searchbar';
import * as Styled from '../styles/Home.styled';
import { Main } from '../styles/UI.styled';

const HomePage = () => {
  const navigate = useNavigate();

  const handleSubmit = (payload) => {
    navigate('/search', { state: { type: payload.type, value: payload.value } });
  };

  return (
    <Main>
      <Background />
      <Styled.Home>
        <Hero />
        <Searchbar handleSubmit={handleSubmit} />
      </Styled.Home>
    </Main>
  );
};

export default HomePage;
