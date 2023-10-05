import React from 'react'

import * as Styled from '../../styles/Publications.styled'
import PublicationButton from './PublicationButton'

const PublicationList = () => {
  return (
    <Styled.PublicationList>
      <PublicationButton />
      <PublicationButton />
      <PublicationButton />
      <PublicationButton />
    </Styled.PublicationList>
  )
}

export default PublicationList
