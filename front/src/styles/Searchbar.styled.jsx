import { styled } from 'styled-components';
import * as variables from './Variables.js';

const BORDER_RADIUS = '25px';
const PADDING = '1rem 2rem';
const BOX_SHADOW = '4px 4px 4px 0px rgba(0, 0, 0, 0.25)';

export const Searchbar = styled.div`
  display: flex;
  flex-direction: row;
  align-items: center;
  justify-content: center;
  color: ${variables.BLACK_COLOR};
  font-size: 2.2rem;
  width: 100%;
  padding: ${PADDING};

  input {
    width: 100%;
    border: none;
    outline: none;
    padding: ${PADDING};
    background-color: ${variables.WHITE_COLOR};
  }
`;

export const SearchbarDiv = styled.div`
  display: flex;
  flex-direction: row;
  background-color: ${variables.WHITE_COLOR};
  box-shadow: ${BOX_SHADOW};
  border-radius: ${BORDER_RADIUS} ${BORDER_RADIUS} ${BORDER_RADIUS} ${BORDER_RADIUS};
  overflow: hidden;
`;

export const SearchButton = styled.button`
  font-size: 2.6rem;
  transition: all 0.2s ease-out;
  padding: ${PADDING};
  justify-self: flex-start;
  &:hover {
    background-color: ${variables.GRAY_COLOR};
  }
`;

export const TypeSelect = styled.select`
  background-color: ${variables.BLUE_COLOR};
  color: ${variables.WHITE_COLOR};
  padding: ${PADDING};
  transition: all 0.2s ease-out;
  outline: none;
  border: none;
  cursor: pointer;
  &:hover {
    background-color: ${variables.BLUE_HOVER_COLOR};
  }
`;
