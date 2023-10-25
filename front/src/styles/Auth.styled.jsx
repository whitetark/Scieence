import { styled } from 'styled-components';
import * as variables from './Variables.js';

const padding = '1rem';

export const AuthWrapper = styled.div`
  display: flex;
  flex-direction: column;
  gap: 2rem;
  background-color: ${variables.WHITE_COLOR};
  border-radius: 10px;
  overflow: hidden;
  max-width: 29.2rem;
  width: 100%;
  align-self: flex-end;
  box-shadow: 0px 4px 4px 0px rgba(0, 0, 0, 0.25);
`;

export const AuthHeader = styled.div`
  background-color: ${variables.BLUE_COLOR};
  color: ${variables.WHITE_COLOR};
  padding: ${padding};
  text-align: center;
  font-weight: 500;
  h2 {
    font-size: 2.4rem;
  }
`;
export const AuthMain = styled.div`
  display: flex;
  flex-direction: column;
  padding: 0rem ${padding} ${padding};
  gap: 1rem;
  form {
    display: flex;
    flex-direction: column;
    align-items: center;
    gap: 1rem;
    input {
      border: 1px solid ${variables.GRAY_COLOR};
      border-radius: 10px;
      padding: 1rem;
      font-size: 1.4rem;
      width: 100%;
      max-width: 27.1rem;
      outline: none;
      &::placeholder {
        color: ${variables.GRAY_COLOR};
        font-weight: 500;
        width: 100%;
      }

      &.input-error {
        border-color: red;
      }
    }
  }
`;

export const AuthInfo = styled.div`
  text-align: center;
  color: ${variables.BLACK_COLOR};
  font-weight: 500;
  font-size: 1.4rem;

  button {
    color: ${variables.BLUE_COLOR};
    transition: color 0.2s ease-out;
    &:hover {
      color: ${variables.BLUE_HOVER_COLOR};
    }
  }
`;
