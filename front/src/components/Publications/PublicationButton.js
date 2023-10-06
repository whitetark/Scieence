import React from 'react'

import * as Styled from '../../styles/Publications.styled'
import useModal from '../../hooks/use-modal'
import Modal from '../UI/Modal'
import Publication from './Publication'

const PublicationButton = (props) => {
  const { isShowing, toggle } = useModal()

  const data = props.data

  return (
    <>
      {isShowing && (
        <Modal isShowing={isShowing} hide={toggle} className={'pub-modal'} hasOverlay>
          <Publication hide={toggle} data={data} />
        </Modal>
      )}
      <Styled.PublicationButton onClick={toggle}>
        <h4 id='title'>{data.title}</h4>
        <p id='detailed'>{data.description}</p>
        <p id='authors'>{data.authors.join(', ')}</p>
      </Styled.PublicationButton>
    </>
  )
}

export default PublicationButton
