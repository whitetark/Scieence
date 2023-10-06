import React from 'react'

import * as Styled from '../styles/Results.styled'
import { Main } from '../styles/UI.styled'
import Searchbar from '../components/UI/Searchbar'
import Background from '../components/UI/Background'
import PublicationList from '../components/Publications/PublicationList'

const FavoritePage = () => {
  const isShown = false
  return (
    <Main>
      <Styled.MainSearchbar>
        <Background />
        <Searchbar />
      </Styled.MainSearchbar>
      <Styled.MainWrapper>
        <Styled.MainContent>
          <h2>Favourite Publications</h2>
          {isShown && <PublicationList />}
        </Styled.MainContent>
      </Styled.MainWrapper>
    </Main>
  )
}

export default FavoritePage
