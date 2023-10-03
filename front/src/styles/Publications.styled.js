import { styled } from 'styled-components'
import * as variables from './Variables.js'

export const PubList = styled.div`
  display: flex;
  flex-direction: column;
  gap: 2rem;
`

export const Pub = styled.div`
  display: flex;
  flex-direction: column;
  gap: 0.5rem;
  background-color: ${variables.PUBLICATION_COLOR};
  box-shadow: ${variables.BOX_SHADOW};
  border-radius: 10px;
  padding: 1.3rem;
  font-weight: 500;
  overflow: hidden;

  #title {
    text-transform: uppercase;
    font-weight: 600;
    font-size: 1.8rem;
  }

  #detailed {
    color: ${variables.DETAILED_COLOR};
    font-size: 1.4rem;
    overflow: hidden;
    text-overflow: ellipsis;
    max-height: 4ch;
  }

  #authors {
    color: ${variables.BLUE_COLOR};
    font-size: 1.4rem;
  }
`
