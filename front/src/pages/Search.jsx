import React, { useEffect, useState } from 'react';
import { useLocation, useSearchParams } from 'react-router-dom';

import { ProgressBar } from 'react-loader-spinner';
import { useQuery } from 'react-query';
import { PubService } from '../app/services/api';
import FilterClient from '../components/Home/FilterClient';
import PublicationList from '../components/Publications/PublicationList';
import Background from '../components/UI/Background';
import Pagination from '../components/UI/Pagination';
import Searchbar from '../components/UI/Searchbar';
import * as Styled from '../styles/Results.styled';
import { HelpDiv, Main } from '../styles/UI.styled';

const initialState = {
  Query: 'a',
  Type: 'keyword',
};

const SearchPage = () => {
  const [jsonData, setJsonData] = useState([]);
  const [keywordsList, setKeywordsList] = useState([]);
  const [showData, setShowData] = useState([]);
  const [currentPage, setCurrentPage] = useState(1);
  const [totalPages, setTotalPages] = useState(2);
  const [searchParams, setSearchParams] = useSearchParams();
  const [requestBody, setRequestBody] = useState(initialState);
  const postsPerPage = 4;

  const { state } = useLocation();
  const { isLoading: authorIsLoading, refetch: refetchByAuthor } = useQuery(
    ['getPubsByAuthor', requestBody],
    () => PubService.getPubsByAuthor(requestBody),
    {
      onError: (error) => {
        console.log('Get Publications By Authors error: ' + error.message);
      },
      onSuccess: (data) => {
        setJsonData(data.data.records);
        setKeywordsList(data.data.keywordCounts);
      },
      enabled: false,
    },
  );
  const { isLoading: keywordIsLoading, refetch: refetchByKeyword } = useQuery(
    ['getPubsByKeyword', requestBody],
    () => PubService.getPubsByKeyword(requestBody),
    {
      onError: (error) => {
        console.log('Get Publications By Keyword error: ' + error.message);
      },
      onSuccess: (data) => {
        setJsonData(data.data.records);
        setKeywordsList(data.data.keywordCounts);
      },
      enabled: false,
    },
  );
  const { isLoading: subjectIsLoading, refetch: refetchBySubject } = useQuery(
    ['getPubsBySubject', requestBody],
    () => PubService.getPubsBySubject(requestBody),
    {
      onError: (error) => {
        console.log('Get Publications By Subject error: ' + error.message);
      },
      onSuccess: (data) => {
        setJsonData(data.data.records);
        setKeywordsList(data.data.keywordCounts);
      },
      enabled: false,
    },
  );

  useEffect(() => {
    if (state) {
      setRequestBody({ ...requestBody, Query: state.value, Type: state.type });
    } else {
      let request = {};
      searchParams.get('type')
        ? (request = { ...request, Type: searchParams.get('type') })
        : undefined;
      searchParams.get('query')
        ? (request = { ...request, Query: searchParams.get('query') })
        : undefined;

      setRequestBody({ ...requestBody, Query: request.Query, Type: request.Type });
    }
    const pageValue = parseInt(searchParams.get('page'));
    if (pageValue) {
      setCurrentPage(pageValue);
    }
  }, []);
  useEffect(() => {
    setShowData(jsonData);
  }, [jsonData]);
  useEffect(() => {
    setTotalPages(Math.ceil(showData.length / postsPerPage));
  }, [showData]);
  useEffect(() => {
    const newSearch = new URLSearchParams(searchParams);
    newSearch.set('page', currentPage);
    setSearchParams(newSearch);
  }, [currentPage]);
  useEffect(() => {
    const newSearch = new URLSearchParams(searchParams);
    newSearch.set('query', requestBody.Query);
    newSearch.set('type', requestBody.Type);
    setSearchParams(newSearch);
    fetchDataByType();
  }, [requestBody]);

  const fetchDataByType = () => {
    if (requestBody === initialState) {
      return;
    }
    switch (requestBody.Type) {
      case 'keyword':
        refetchByKeyword(requestBody);
        break;

      case 'author':
        refetchByAuthor(requestBody);
        break;

      case 'subject':
        refetchBySubject(requestBody);
        break;

      default:
        console.log('No Type', searchParams.get('type'));
        break;
    }
  };
  const handleSubmit = (payload) => {
    const newSearch = new URLSearchParams(searchParams);
    newSearch.set('query', payload.value);
    newSearch.set('type', payload.type);
    setSearchParams(newSearch);
    setRequestBody((prevState) => {
      return { ...prevState, Query: payload.value, Type: payload.type };
    });
  };
  const getFiltersHandler = (filtersValue) => {
    let rawData = Array.from(jsonData);

    if (filtersValue.checkedKeywords.length > 0) {
      rawData = rawData.filter((publication) =>
        filtersValue.checkedKeywords.some((keyword) => publication.subjects.includes(keyword)),
      );
    }

    const sorted = Array.from(
      rawData.sort((pub1, pub2) => {
        switch (filtersValue.sortBy) {
          case 'titleAsc':
            return pub1.title.localeCompare(pub2.title);
          case 'titleDesc':
            return pub2.title.localeCompare(pub1.title);
          case 'yearAsc':
            return pub2.publicationYear - pub1.publicationYear;
          case 'yearDesc':
            return pub1.publicationYear - pub2.publicationYear;
          default:
            return 0;
        }
      }),
    );

    setShowData(sorted);
  };

  const lastPostIndex = currentPage * postsPerPage;
  const firstPostIndex = lastPostIndex - postsPerPage;
  let currentPosts = showData.slice(firstPostIndex, lastPostIndex);

  const isLoading = authorIsLoading || keywordIsLoading || subjectIsLoading;
  return (
    <Main>
      <Styled.MainSearchbar>
        <Background />
        <Searchbar handleSubmit={handleSubmit} type={requestBody.Type} />
      </Styled.MainSearchbar>
      <Styled.MainWrapper>
        <Styled.MainContent>
          <Styled.FoundHeader>
            <h2>Your Search Result</h2>
            {!isLoading || jsonData.length !== 0 ? (
              <Pagination
                totalPages={totalPages}
                currentPage={currentPage}
                setCurrentPage={setCurrentPage}
              />
            ) : undefined}
          </Styled.FoundHeader>
          <Styled.FoundContent>
            <div>
              {jsonData && (
                <FilterClient keywordsList={keywordsList} getFilters={getFiltersHandler} />
              )}
            </div>
            <div className='divider'></div>
            {isLoading ? (
              <HelpDiv>
                <ProgressBar width='100' height='100' borderColor='#98A4DF' barColor='#747DAB' />
              </HelpDiv>
            ) : (
              <PublicationList data={currentPosts} />
            )}
          </Styled.FoundContent>
        </Styled.MainContent>
      </Styled.MainWrapper>
    </Main>
  );
};
export default SearchPage;
