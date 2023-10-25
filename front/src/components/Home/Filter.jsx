import React from 'react';

import * as Styled from '../../styles/Filter.styled';
import Button from '../UI/Button';

const Filter = () => {
  return (
    <Styled.Filter>
      <span>FILTERS</span>
      <Button>Apply</Button>
    </Styled.Filter>
  );
};

export default Filter;
