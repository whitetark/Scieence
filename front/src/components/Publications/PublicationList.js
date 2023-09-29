import React from 'react'
import Publication from './Publication'
import { PubList } from '../styles/Publications.styled'

const PublicationList = () => {
  return (
    <PubList>
      <Publication />
      <Publication />
      <Publication />
      <Publication />
    </PubList>
  )
}

export default PublicationList
