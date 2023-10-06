import { styled } from 'styled-components';
import * as variables from './Variables.js';
import { Container } from './UI.styled.js';

export const Home = styled(Container)`
  max-width: 60rem;
  width: 100%;
  z-index: 1;
  color: ${variables.WHITE_COLOR};
  > div:not(:last-child) {
    margin-bottom: 3.5rem;
  }

  .advanced {
    border-bottom: 1px solid ${variables.WHITE_COLOR};
    margin-bottom: 1rem;
    float: right;
    transition: all 0.2s ease-out;
    &:hover {
      color: ${variables.GRAY_COLOR};
      border-color: ${variables.GRAY_COLOR};
    }
  }
`;

export const HeroWrapper = styled.div`
  display: flex;
  flex-direction: column;
  justify-content: center;
  gap: 1rem;

  text-align: center;

  h1 {
    font-size: 3.2rem;
    font-weight: 600;
  }
`;
