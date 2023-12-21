import React, { useEffect, useState } from 'react';

import * as Styled from '../../styles/Filter.styled';

const FilterClient = ({ keywordsList, getClientFilters }) => {
  const [checkedKeywords, setCheckedKeywords] = useState([]);
  const [sortBy, setSortBy] = useState('titleAsc');

  const handleCheckboxChange = (keyword) => {
    setCheckedKeywords((prevCheckedKeywords) => {
      return prevCheckedKeywords.includes(keyword)
        ? prevCheckedKeywords.filter((kw) => kw !== keyword)
        : [...prevCheckedKeywords, keyword];
    });
  };

  const handleSortingChange = (event) => {
    event.preventDefault();
    setSortBy(event.target.value);
  };

  useEffect(() => {
    const filtersValue = {
      checkedKeywords: checkedKeywords,
      sortBy: sortBy,
    };

    getClientFilters(filtersValue);
  }, [checkedKeywords, sortBy]);

  return (
    <Styled.Filter>
      <Styled.FilterDiv>
        <p>Sorting</p>
        <select name='sorting' value={sortBy} onChange={handleSortingChange}>
          <option value='titleAsc'>By Title from A to Z</option>
          <option value='titleDesc'>By Title from Z to A</option>
          <option value='yearAsc'>By Year - New First</option>
          <option value='yearDesc'>By Year - Old First</option>
        </select>
      </Styled.FilterDiv>
      <Styled.FilterDiv>
        <p>Keywords</p>
        <Styled.KeywordList>
          {keywordsList &&
            keywordsList.map((keyword, index) => {
              return (
                <div key={index} className={index % 2 ? 'odd' : 'even'}>
                  <input
                    type='checkbox'
                    value={keyword.value}
                    checked={checkedKeywords.includes(keyword.value)}
                    onChange={() => handleCheckboxChange(keyword.value)}
                  />
                  <span className='keyword'>{keyword.value}</span>
                  <span className='count'>({keyword.count})</span>
                </div>
              );
            })}
        </Styled.KeywordList>
      </Styled.FilterDiv>
    </Styled.Filter>
  );
};

export default FilterClient;
