import { styled } from 'styled-components'
import * as variables from './Variables.js'

const BORDER_RADIUS = '25px'
const PADDING = '1rem 2rem'
const BOX_SHADOW = '4px 4px 4px 0px rgba(0, 0, 0, 0.25)'

export const Searchbar = styled.form`
  display: flex;
  flex-direction: row;
  align-items: center;
  justify-content: center;
  color: ${variables.BLACK_COLOR};
  font-size: 2.2rem;
  width: 100%;

  input {
    width: 100%;
    background-color: ${variables.WHITE_COLOR};
    border-radius: 0 ${BORDER_RADIUS} ${BORDER_RADIUS} 0;
    border: none;
    outline: none;
    padding: ${PADDING};
    box-shadow: ${BOX_SHADOW};
  }

  button {
    background-color: ${variables.WHITE_COLOR};
    font-size: 2.2rem;
    padding: ${PADDING};
    border-radius: ${BORDER_RADIUS} 0 0 ${BORDER_RADIUS};
    box-shadow: ${BOX_SHADOW};
    transition: all 0.2s ease-out;
    &:hover {
      background-color: ${variables.GRAY_COLOR};
    }
  }
`
