import { styled } from 'styled-components'
import * as variables from '../../scss/Variables.js'

export const ModalWrapper = styled.div`
  position: fixed;
  top: 0;
  left: 0;
  content: '';
  background-color: black;
  height: 100vh;
  width: 100%;
  opacity: 0.75;
  z-index: 101;
`
export const ModalContent = styled.div`
  width: 100%;
  height: fit-content;
  max-width: 300px;
  background-color: #a4d1c8;
  position: relative;
  border: 5px;
  box-shadow: 2px 4px 20px 0px rgba(0, 0, 0, 0.75);
  border-radius: 10px;
  padding: 20px;
`
