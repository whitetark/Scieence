import React from 'react'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { SearchbarStyle } from '../styles/Searchbar.styled'

const Searchbar = () => {
  return (
    <SearchbarStyle>
      <button>
        <FontAwesomeIcon icon='fa-solid fa-magnifying-glass' fixedWidth />
      </button>
      <input type='text' name='search' placeholder='What do you want to find?' />
    </SearchbarStyle>
  )
}

export default Searchbar
