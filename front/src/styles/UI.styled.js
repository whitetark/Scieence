import { styled } from 'styled-components';
import * as variables from './Variables';

export const Main = styled.main`
  flex: 1 1 auto;
  display: flex;
  flex-direction: column;
  position: relative;
  justify-content: center;
`;
export const Container = styled.div`
  max-width: 120rem;
  margin: 0 auto;
`;
export const BackgroundWrapper = styled.div`
  position: absolute;
  width: 100%;
  height: 100%;
  z-index: -1;
`;
export const BackgroundItem = styled.div`
  position: absolute;
  width: 100%;
  height: 100%;
  background-image: url(${(props) => props.image});
  background-repeat: no-repeat;
  background-position: center;
  background-size: cover;
  filter: blur(5px) brightness(51%);
`;
export const Overlay = styled.div`
  position: fixed;
  top: 0;
  left: 0;
  content: '';
  height: 100vh;
  width: 100%;
  z-index: 2;
  background-color: ${(props) => props.color};
  opacity: 0.75;
`;
export const Pagination = styled.div`
  margin-left: auto;
  display: flex;
  flex-direction: row;
  gap: 0.5rem;
  font-size: 2rem;
  font-weight: 600;
  span {
    width: 5rem;
  }

  button {
    &:disabled {
      color: ${variables.TEXT_COLOR};
      cursor: default;
    }
  }
`;
