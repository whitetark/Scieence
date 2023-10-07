import { styled } from 'styled-components';
import { Container } from './UI.styled.js';

export const Overlay = styled.div`
  content: '';
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background-color: black;
  opacity: 0.5;
`;
export const Children = styled(Container)`
  display: flex;
  flex-direction: column;
  position: fixed;
  z-index: 102;

  &.login-modal {
    padding: 0 1rem;
    top: 7rem;
    width: 100%;
  }

  &.pub-modal {
    height: 100%;
    justify-content: center;
    align-items: center;
  }
`;

export const ModalWrapper = styled.div`
  position: absolute;
  top: 0;
  left: 0;
  overflow-y: hidden;
  height: 100vh;
  width: 100%;
  z-index: 101;

  display: flex;
  justify-content: center;
  align-items: center;
`;
