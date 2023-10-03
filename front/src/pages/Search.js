import React from 'react'
import * as Styled from '../styles/Favorite.styled'
import { Main } from '../styles/UI.styled'
import Searchbar from '../components/UI/Searchbar'
import Background from '../components/UI/Background'
import PublicationList from '../components/Publications/PublicationList'
import Filter from '../components/UI/Filter'

const SearchPage = () => {
  return (
    <Main>
      <Styled.MainSearchbar>
        <Background />
        <Searchbar />
      </Styled.MainSearchbar>
      <Styled.MainWrapper>
        <Styled.MainContent>
          <h2>Found Publications</h2>
          <Styled.FoundContent>
            <Filter />
            <div className='divider'></div>
            <PublicationList />
          </Styled.FoundContent>
        </Styled.MainContent>
      </Styled.MainWrapper>
    </Main>
  )
}

export default SearchPage
