import { styled } from 'styled-components';
import { HelpDiv } from './UI.styled.jsx';
import * as variables from './Variables.js';

const MAIN_MARGIN = '4rem';

export const Publication = styled.article`
  display: flex;
  flex-direction: column;
  background-color: ${variables.WHITE_COLOR};
  padding: 0 0 ${MAIN_MARGIN} ${MAIN_MARGIN};
  border-radius: 10px;
  max-width: 76.2rem;
  width: 100%;
`;

export const PublicationActions = styled.div`
  align-self: flex-end;
  margin: 1rem 1rem 0 0;
  font-size: 2.4rem;
`;

export const PublicationMain = styled.div`
  margin-right: ${MAIN_MARGIN};
  display: flex;
  flex-direction: column;
  gap: 1.5rem;
  font-weight: 500;

  .title {
    font-size: 2.8rem;
    font-weight: 600;
    text-transform: uppercase;
  }

  .authors {
    color: ${variables.BLUE_COLOR};
    max-height: 10ch;
    overflow-y: hidden;
  }

  .details {
    color: ${variables.TEXT_COLOR};
    overflow-y: auto;
    max-height: 24ch;
  }
`;

export const PublicationDetails = styled.div`
  display: grid;
  grid-template-columns: repeat(2, 50%);
  gap: 2rem;
  font-size: 1.4rem;
`;

export const PublicationInfo = styled.div`
  display: flex;
  flex-direction: column;
  justify-content: center;
  color: ${variables.TEXT_COLOR};
  background-color: ${variables.PUBLICATION_COLOR};
  box-shadow: ${variables.BOX_SHADOW};
  border-radius: 10px;
  padding: 1rem;
`;

export const PublicationLinks = styled.div`
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  overflow-wrap: break-word;
  color: ${variables.TEXT_COLOR};
  a {
    color: ${variables.BLUE_COLOR};
  }
`;

export const PublicationKeywords = styled.div`
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(8rem, 1fr));
  gap: 1rem;

  > * {
    background-color: ${variables.PUBLICATION_COLOR};
    box-shadow: ${variables.BOX_SHADOW};
    border-radius: 10px;
    text-align: center;
    overflow: hidden;
  }
`;

export const PublicationList = styled.div`
  display: flex;
  flex-direction: column;
  width: 100%;
  gap: 2rem;

  ${HelpDiv} {
    display: flex;
    justify-content: center;
    align-items: center;
    height: 100%;
    width: 100%;
    color: ${variables.BLUE_COLOR};
    font-weight: 500;
  }
`;

export const PublicationButton = styled.article`
  display: flex;
  flex-direction: column;
  gap: 0.5rem;
  background-color: ${variables.PUBLICATION_COLOR};
  box-shadow: ${variables.BOX_SHADOW};
  border-radius: 10px;
  padding: 1.3rem;
  font-weight: 500;
  overflow: hidden;
  transition: all 0.3s ease-out;

  .title {
    text-transform: uppercase;
    font-weight: 600;
    overflow: hidden;
    text-overflow: ellipsis;
    max-height: 4ch;
    font-size: 1.8rem;
  }

  .details {
    color: ${variables.DETAILED_COLOR};
    font-size: 1.4rem;
    overflow: hidden;
    text-overflow: ellipsis;
    max-height: 4ch;
  }

  .authors {
    color: ${variables.BLUE_COLOR};
    font-size: 1.4rem;
    overflow: hidden;
    text-overflow: ellipsis;
    max-height: 4ch;
    transition: all 0.3s ease-out;
  }

  &:hover {
    background-color: ${variables.GRAY_COLOR};
    cursor: pointer;
    .authors {
      color: ${variables.BLUE_HOVER_COLOR};
    }
  }
`;
