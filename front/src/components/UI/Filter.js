import React from 'react'
import * as Styled from '../../styles/Filter.styled'

const Filter = () => {
  return (
    <Styled.FilterDiv>
      <Styled.Filter>
        <span>FILTERS</span>
        <button>Apply</button>
      </Styled.Filter>
    </Styled.FilterDiv>
  )
}

export default Filter
