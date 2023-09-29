import { styled } from 'styled-components'
import * as variables from '../../scss/Variables.js'
import { ContainerWrapper } from '../styles/UI.styled'

export const Fav = styled.div`
  display: flex;
  flex-direction: column;
  height: 100%;
`

export const MainWrapper = styled.div`
  height: 100%;
  background-color: ${variables.WHITE_COLOR};
`

export const MainContent = styled(ContainerWrapper)`
  padding: 1rem 0;
  h2 {
    padding: 1rem 0;
    font-size: 24px;
    text-transform: uppercase;
    font-weight: 600;
  }
`

export const MainSearchbar = styled.div`
  display: flex;
  position: relative;
  height: 100%;
  input {
    margin: 1rem 0;
    width: 50%;
    max-width: 46rem;
  }
`
