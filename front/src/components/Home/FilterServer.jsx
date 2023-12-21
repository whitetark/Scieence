import Box from '@mui/material/Box';
import Slider from '@mui/material/Slider';
import React, { useState } from 'react';
import * as Styled from '../../styles/Filter.styled';
import { Button } from '../../styles/UI.styled';

const FilterServer = ({ getServerFilters }) => {
  const [language, setLanguage] = useState('eng');
  const [year, setYear] = useState([2015, 2023]);

  const handleLanguage = (event) => {
    event.preventDefault();
    setLanguage(event.target.value);
  };

  const handleYear = (event, newYear) => {
    event.preventDefault();
    setYear(newYear);
  };

  const handleSubmit = () => {
    getServerFilters({ language, year });
  };

  return (
    <Styled.Filter>
      <Styled.FilterDiv>
        <p>Language</p>
        <select name='language' value={language} onChange={handleLanguage}>
          <option value='eng'>English</option>
          <option value='ger'>German</option>
          <option value='spa'>Spanish</option>
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
      <Button onClick={handleSubmit}>Apply</Button>
    </Styled.Filter>
  );
};

export default FilterServer;
