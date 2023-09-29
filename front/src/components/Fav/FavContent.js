import React from 'react'
import Searchbar from '../UI/Searchbar'
import { Background, BackgroundItem } from '../styles/HomeContent.styled'
import { Fav, MainWrapper, MainContent, MainSearchbar } from '../styles/FavContent.styled'
import PublicationList from '../Publications/PublicationList'

const FavContent = () => {
  return (
    <Fav>
      <MainSearchbar>
        <Background>
          <BackgroundItem />
        </Background>
        <Searchbar />
      </MainSearchbar>
      <MainWrapper>
        <MainContent>
          <h2>Favourite Publications</h2>
          <PublicationList />
        </MainContent>
      </MainWrapper>
    </Fav>
  )
}

export default FavContent
