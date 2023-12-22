import React from 'react';

import { useAuthContext } from '../app/store/auth-context';
import PublicationList from '../components/Publications/PublicationList';
import Background from '../components/UI/Background';
import * as Styled from '../styles/Results.styled';
import { Main } from '../styles/UI.styled';

const FavoritePage = () => {
  const { userData } = useAuthContext();
  return (
    <Main>
      <Styled.MainSearchbar>
        <Background />
      </Styled.MainSearchbar>
      <Styled.MainWrapper>
        <Styled.MainContent>
          <h2>Favourite Publications</h2>
          {<PublicationList data={userData.favourites} />}
        </Styled.MainContent>
      </Styled.MainWrapper>
    </Main>
  );
};

export default FavoritePage;
