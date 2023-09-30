import React from 'react'
import * as Styled from '../styles/Home.styled'
import { Main } from '../styles/UI.styled'
import Hero from '../components/Home/Hero'
import Searchbar from '../components/UI/Searchbar'
import Background from '../components/UI/Background'

const HomePage = () => {
  return (
    <Main>
      <Background />
      <Styled.Home>
        <Hero />
        <button className='advanced'>Advanced Search</button>
        <Searchbar />
      </Styled.Home>
    </Main>
  )
}

export default HomePage
