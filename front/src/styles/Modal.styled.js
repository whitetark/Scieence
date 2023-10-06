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
  position: relative;
  z-index: 102;
`;

export const ModalWrapper = styled.div`
  position: absolute;
  top: 0;
  left: 0;
  overflow-y: hidden;
  height: 100vh;
  width: 100%;
  z-index: 101;
`;
