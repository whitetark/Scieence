import { styled } from 'styled-components';
import * as variables from './Variables.js';

export const Filter = styled.div`
  display: flex;
  flex-direction: column;
  background-color: ${variables.PUBLICATION_COLOR};
  box-shadow: ${variables.BOX_SHADOW};
  border-radius: 10px;
  justify-content: center;
  align-items: flex-start;
  align-self: flex-start;
  padding: 2rem;
  gap: 1rem;

  p {
    font-size: 2rem;
    font-weight: 500;
    color: ${variables.DETAILED_COLOR};
  }
`;

export const FilterDiv = styled.div`
  display: flex;
  flex-direction: column;
  gap: 1rem;

  select {
    border-radius: 10px;
    border: none;
    outline: none;
    padding: 0.5rem;
    color: ${variables.TEXT_COLOR};
    font-weight: 400;
    cursor: pointer;
  }
`;

export const FilterSlider = styled.div`
  width: 100%;
`;

const keywordPadding = '1rem';

export const KeywordList = styled.div`
  display: flex;
  flex-direction: column;
  justify-content: flex-start;
  overflow-y: scroll;
  max-height: 15rem;
  background-color: ${variables.WHITE_COLOR};
  border-radius: 10px;
  caret-color: ${variables.BLUE_COLOR};
  text-transform: capitalize;
  gap: 0.5rem;
  color: ${variables.TEXT_COLOR};

  > div {
    display: flex;
    flex-direction: row;
    justify-content: space-between;
    padding-left: ${keywordPadding};
    padding-right: ${keywordPadding};
    gap: 1rem;

    input {
      cursor: pointer;
    }

    .keyword {
      text-align: center;
      word-break: break-all;
    }
    &:first-child {
      padding-top: ${keywordPadding};
    }
    &:last-child {
      padding-bottom: ${keywordPadding};
    }

    &.odd {
      background-color: ${variables.DIVIDER_COLOR};
    }
  }

  span {
    font-size: 1.4rem;
  }

  &::-webkit-scrollbar {
    width: 1rem;
  }

  &::-webkit-scrollbar-track {
    -webkit-box-shadow: inset 0 0 6px rgba(0, 0, 0, 0.068);
    border-radius: 10px;
  }

  &::-webkit-scrollbar-thumb {
    border-radius: 10px;
    background-color: ${variables.BLUE_COLOR};
  }
`;
