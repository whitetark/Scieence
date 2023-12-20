import React from 'react';

import * as Styled from '../../styles/Publications.styled';
import { HelpDiv } from '../../styles/UI.styled';
import PublicationButton from './PublicationButton';

const PublicationList = (props) => {
  const publications = props.data;
  return (
    <Styled.PublicationList>
      {publications.length !== 0 ? (
        publications.map((publication, index) => {
          return <PublicationButton data={publication} key={index} />;
        })
      ) : (
        <HelpDiv>Not Found :(</HelpDiv>
      )}
    </Styled.PublicationList>
  );
};

export default PublicationList;
