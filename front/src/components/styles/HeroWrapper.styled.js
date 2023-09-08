import { styled } from 'styled-components'
import * as variables from '../../scss/Variables.js'

export const HeroWrapper = styled.div`
  display: flex;
  flex-direction: column;
  justify-content: center;
  gap: 2rem;

  text-align: center;

  h1 {
    font-size: 3.2rem;
    font-weight: 600;
  }
`
