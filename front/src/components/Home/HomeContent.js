import React from 'react'
import Hero from './Hero'
import Searchbar from '../UI/Searchbar'
import { Background, BackgroundItem, Main, MainWrapper } from '../styles/HomeContent.styled'

const HomeContent = () => {
  return (
    <Main>
      <Background>
        <BackgroundItem />
      </Background>
      <MainWrapper>
        <Hero />
        <button className='advanced'>Advanced Search</button>
        <Searchbar />
      </MainWrapper>
    </Main>
  )
}

export default HomeContent
