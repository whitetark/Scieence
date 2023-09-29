import { styled } from 'styled-components'
import * as variables from '../../scss/Variables.js'

export const PubList = styled.div`
  display: grid;
  grid-template-columns: 1fr;
  justify-content: space-between;
  gap: 2rem;
`

export const Pub = styled.div`
  display: flex;
  flex-direction: column;
  gap: 0.5rem;
  background-color: ${variables.PUBLICATION_COLOR};
  box-shadow: 0px 4px 4px 0px rgba(0, 0, 0, 0.25);
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
