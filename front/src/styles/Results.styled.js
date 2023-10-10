import { styled } from 'styled-components';
import * as variables from './Variables.js';
import { Container } from './UI.styled.js';

export const MainWrapper = styled.div`
  background-color: ${variables.WHITE_COLOR};
  height: 100%;
`;

export const MainContent = styled(Container)`
  padding: 1rem;
  h2 {
    padding: 1rem 0;
    font-size: 24px;
    text-transform: uppercase;
    font-weight: 600;
  }
`;

export const MainSearchbar = styled.div`
  display: flex;
  position: relative;
  height: 20rem;
  input {
    margin: 1rem 0;
    width: 50%;
    max-width: 46rem;
  }
`;
export const FoundHeader = styled.div`
  display: flex;
  flex-direction: row;
  align-items: center;
`;

export const FoundContent = styled.div`
  display: flex;
  flex-direction: row;
  gap: 2rem;
  .divider {
    content: '';
    border-left: 2px solid ${variables.DIVIDER_COLOR};
  }
`;
