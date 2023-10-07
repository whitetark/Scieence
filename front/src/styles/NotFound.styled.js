import { styled } from 'styled-components';
import * as variables from './Variables.js';

export const NotFound = styled.div`
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  height: 100%;
  background-color: ${variables.PUBLICATION_COLOR};
  font-size: 6.4rem;
  font-weight: 500;

  button {
    font-size: 3.6rem;
    color: ${variables.BLUE_COLOR};
    transition: all 0.2s ease-out;

    &:hover {
      color: ${variables.BLUE_HOVER_COLOR};
    }
  }
`;
