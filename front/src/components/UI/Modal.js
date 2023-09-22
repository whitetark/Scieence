import React from 'react'
import { useNavigate } from 'react-router-dom'
import { ModalWrapper, ModalContent } from '../styles/Modal.styled'
import { createPortal } from 'react-dom'

const Modal = (props) => {
  const navigate = useNavigate()
  return createPortal(
    <ModalWrapper onClick={() => navigate('/')}>
      <ModalContent onClick={(e) => e.stopPropagation()}>{props.children}</ModalContent>
    </ModalWrapper>,
    document.getElementById('modal_root')
  )
}

export default Modal
