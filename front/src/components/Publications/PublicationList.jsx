import React from 'react'

import * as Styled from '../../styles/Publications.styled'
import PublicationButton from './PublicationButton'

const PublicationList = (props) => {
  const publications = props.data
  return (
    <Styled.PublicationList>
      {publications.map((publication, index) => {
        return <PublicationButton data={publication} key={index} />
      })}
    </Styled.PublicationList>
  )
}

export default PublicationList
