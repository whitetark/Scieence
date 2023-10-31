import React, { useContext } from 'react';

import AuthContext from '../app/store/auth-context';
import PublicationList from '../components/Publications/PublicationList';
import Background from '../components/UI/Background';
import Searchbar from '../components/UI/Searchbar';
import * as Styled from '../styles/Results.styled';
import { Main } from '../styles/UI.styled';

const FavoritePage = () => {
  const { userData } = useContext(AuthContext);
  const isShown = false;
  return (
    <Main>
      <Styled.MainSearchbar>
        <Background />
        <Searchbar />
      </Styled.MainSearchbar>
      <Styled.MainWrapper>
        <Styled.MainContent>
          <h2>Favourite Publications</h2>
          {isShown && <PublicationList data={userData.Favourites} />}
        </Styled.MainContent>
      </Styled.MainWrapper>
    </Main>
  );
};

export default FavoritePage;
