import React from 'react'
import * as Styled from '../../styles/Publications.styled'
import useModal from '../../hooks/use-modal'
import Modal from '../UI/Modal'
import Publication from './Publication'

const PublicationButton = () => {
  const { isShowing, toggle } = useModal()

  return (
    <>
      {isShowing && (
        <Modal isShowing={isShowing} hide={toggle} className={'pub-modal'} hasOverlay>
          <Publication hide={toggle} />
        </Modal>
      )}
      <Styled.PublicationButton onClick={toggle}>
        <h4 id='title'>Title Of Publication</h4>
        <p id='detailed'>
          Detailed descriptionDetailed descriptionDetailed descriptionDetailed descriptionDetailed
          descriptionDetailed description Detailed description Detailed description Detailed
          descriptionDetailed descriptionDetailed descriptionDetailed descriptionDetailed
          descriptionDetailed description Detailed description Detailed description Detailed
          descriptionDetailed descriptionDetailed descriptionDetailed descriptionDetailed
          descriptionDetailed description Detailed description Detailed descriptions
        </p>
        <p id='authors'>Some author, Some author, Some author</p>
      </Styled.PublicationButton>
    </>
  )
}

export default PublicationButton
