import React from 'react'
import Hero from './Hero'
import Searchbar from '../UI/Searchbar'
import { Background, BackgroundItem, Home, HomeWrapper } from '../styles/HomeContent.styled'

const HomeContent = () => {
  return (
    <Home>
      <Background>
        <BackgroundItem />
      </Background>
      <HomeWrapper>
        <Hero />
        <button className='advanced'>Advanced Search</button>
        <Searchbar />
      </HomeWrapper>
    </Home>
  )
}

export default HomeContent
