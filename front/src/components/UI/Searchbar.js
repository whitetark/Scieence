import React from 'react'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import * as Styled from '../../styles/Searchbar.styled'

const Searchbar = () => {
  return (
    <Styled.Searchbar>
      <button type='submit' onClick={(e) => e.preventDefault()}>
        <FontAwesomeIcon icon='fa-solid fa-magnifying-glass' fixedWidth />
      </button>
      <input type='text' name='search' placeholder='What do you want to find?' autoComplete='off' />
    </Styled.Searchbar>
  )
}

export default Searchbar
