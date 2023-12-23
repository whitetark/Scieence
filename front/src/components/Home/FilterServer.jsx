import Box from '@mui/material/Box';
import Slider from '@mui/material/Slider';
import React, { useState } from 'react';
import * as Styled from '../../styles/Filter.styled';
import { Button } from '../../styles/UI.styled';

const FilterServer = ({ getServerFilters, isLoading }) => {
  const [language, setLanguage] = useState('en');
  const [year, setYear] = useState([2020, 2024]);

  const handleLanguage = (event) => {
    event.preventDefault();
    setLanguage(event.target.value);
  };

  const handleYear = (event, newYear) => {
    event.preventDefault();
    setYear(newYear);
  };

  const handleSubmit = () => {
    getServerFilters({ lang: language, year: year });
  };

  return (
    <Styled.Filter>
      <Styled.FilterDiv>
        <p>Language</p>
        <select name='language' value={language} onChange={handleLanguage}>
          <option value='en'>English</option>
          <option value='de'>German</option>
          <option value='fr'>French</option>
        </select>
      </Styled.FilterDiv>
      <Styled.FilterDiv>
        <p>Year</p>
        <Styled.FilterSlider>
          <Box sx={{ width: 200 }}>
            <Slider
              getAriaLabel={() => 'Choose Year'}
              value={year}
              min={2000}
              max={2024}
              onChange={handleYear}
              valueLabelDisplay='auto'
              color='primary'
            />
          </Box>
        </Styled.FilterSlider>
      </Styled.FilterDiv>
      <Button onClick={handleSubmit} disabled={isLoading}>
        Apply
      </Button>
    </Styled.Filter>
  );
};

export default FilterServer;
