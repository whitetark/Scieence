import React from 'react'
import { useNavigate } from 'react-router-dom'
import { ModalWrapper, ModalContent, Overlay } from '../styles/Modal.styled'
import { createPortal } from 'react-dom'
import { ContainerWrapper } from '../styles/UI.styled'

const Modal = ({ children, isShowing, hide, className }) => {
  const renderChildren = () => {
    return React.cloneElement(children, {
      onClick: clickHandler,
    })
  }
  const clickHandler = (e) => {
    e.stopPropagation()
  }

  return isShowing
    ? createPortal(
        <Overlay onClick={hide}>
          <ModalWrapper className={className}>{renderChildren()}</ModalWrapper>
        </Overlay>,
        document.getElementById('modal_root')
      )
    : null
}

export default Modal
