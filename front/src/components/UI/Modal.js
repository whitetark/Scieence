import React from 'react'
import * as Styled from '../../styles/Modal.styled'
import { createPortal } from 'react-dom'
import { Container } from '../../styles/UI.styled'

const Modal = ({ children, isShowing, hide, className, hasOverlay }) => {
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
        <Styled.ModalWrapper onClick={hide}>
          {hasOverlay ? <Styled.Overlay color={'black'} onClick={hide} /> : undefined}
          <Styled.Children className={className || undefined}> {renderChildren()}</Styled.Children>
        </Styled.ModalWrapper>,
        document.getElementById('modal_root')
      )
    : null
}

export default Modal
