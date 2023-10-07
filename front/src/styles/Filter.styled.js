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
  align-self: flex-start;

  span {
    padding: 4rem;
    font-size: 44px;
    color: gray;
  }
`;
