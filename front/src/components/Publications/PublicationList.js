import React from 'react'
import Publication from './Publication'
import * as Styled from '../../styles/Publications.styled'

const PublicationList = () => {
  return (
    <Styled.PubList>
      <Publication />
      <Publication />
      <Publication />
      <Publication />
    </Styled.PubList>
  )
}

export default PublicationList
