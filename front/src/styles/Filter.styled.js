import { styled } from 'styled-components';
import * as variables from './Variables.js';

export const Filter = styled.div`
  display: flex;
  flex-direction: column;
  background-color: ${variables.PUBLICATION_COLOR};
  box-shadow: ${variables.BOX_SHADOW};
  border-radius: 10px;
  justify-content: center;
  align-items: center;

  span {
    padding: 4rem;
    font-size: 44px;
    color: gray;
  }
  button {
    background-color: ${variables.BLUE_COLOR};
    color: ${variables.WHITE_COLOR};
    padding: 0.5rem 5.5rem;
    margin: 2rem 3rem;
    border-radius: 10px;
    justify-self: flex-end;

    transition: background-color 0.2s ease-out;
    &:hover {
      background-color: ${variables.BLUE_HOVER_COLOR};
    }
  }
`;

export const FilterDiv = styled.div``;
