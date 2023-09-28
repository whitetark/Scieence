import { styled } from 'styled-components'
import * as variables from '../../scss/Variables.js'
import { ContainerWrapper } from './UI.styled.js'

export const Overlay = styled.div`
  position: fixed;
  top: 0;
  left: 0;
  content: '';
  height: 100vh;
  width: 100%;
  z-index: 2;
`

export const ModalWrapper = styled(ContainerWrapper)`
  display: flex;
  flex-direction: column;
  padding: 0 1rem;
`
