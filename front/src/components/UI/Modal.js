import React from 'react'
import * as Styled from '../../styles/UI.styled'
import { createPortal } from 'react-dom'

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
        <Styled.Overlay onClick={hide}>
          <Styled.ModalWrapper className={className}>{renderChildren()}</Styled.ModalWrapper>
        </Styled.Overlay>,
        document.getElementById('modal_root')
      )
    : null
}

export default Modal
