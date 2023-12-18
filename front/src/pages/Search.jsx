import React, { useEffect, useState } from 'react';
import { useSearchParams } from 'react-router-dom';

import { ProgressBar } from 'react-loader-spinner';
import { useQuery } from 'react-query';
import { PubService } from '../app/services/api';
import data from '../app/store/data.json';
import Filter from '../components/Home/Filter';
import PublicationList from '../components/Publications/PublicationList';
import Background from '../components/UI/Background';
import Pagination from '../components/UI/Pagination';
import Searchbar from '../components/UI/Searchbar';
import * as Styled from '../styles/Results.styled';
import { Main } from '../styles/UI.styled';

const initialState = {
  Query: '',
};

const SearchPage = () => {
  const [jsonData, setJsonData] = useState(data.data);
  const [currentPage, setCurrentPage] = useState(1);
  const [totalPages, setTotalPages] = useState(2);
  const [type, setType] = useState('keyword');
  const [searchParams, setSearchParams] = useSearchParams();
  const [requestBody, setRequestBody] = useState(initialState);

  const { isLoading: authorIsLoading, refetch: refetchByAuthor } = useQuery(
    'getPubsByAuthor',
    (payload) => PubService.getPubsByAuthor(payload),
    {
      onError: (error) => {
        console.log('Get Publications By Authors error: ' + error.message);
      },
      onSuccess: (data) => {
        setJsonData(data);
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
      },
      enabled: false,
    },
  );
  const postsPerPage = 4;

  useEffect(() => {
    setJsonData(data.data);
    const pageValue = parseInt(searchParams.get('page'));
    if (searchParams.get('type')) {
      setType(searchParams.get('type'));
    }
    if (pageValue) {
      setCurrentPage(pageValue);
    }
    if (searchParams.get('query')) {
      setRequestBody((prevState) => {
        return { ...prevState, Query: searchParams.get('query') };
      });
    }

    fetchDataByType();
  }, []);

  useEffect(() => {
    setTotalPages(Math.ceil(jsonData.length / postsPerPage));
  }, [jsonData]);

  useEffect(() => {
    const newSearch = new URLSearchParams(searchParams);
    newSearch.set('page', currentPage);
    setSearchParams(newSearch);
  }, [currentPage]);

  useEffect(() => {
    const newSearch = new URLSearchParams(searchParams);
    newSearch.set('type', type);
    setSearchParams(newSearch);
  }, [type]);

  useEffect(() => {
    fetchDataByType();
  }, [requestBody]);

  const fetchDataByType = () => {
    switch (searchParams.get('type')) {
      case 'keyword':
        refetchByKeyword(requestBody);
        break;

      case 'author':
        refetchByAuthor(requestBody);
        break;

      default:
        console.log('No Type');
        break;
    }
  };

  const handleSubmit = (payload) => {
    const newSearch = new URLSearchParams(searchParams);
    newSearch.set('query', payload.value);
    newSearch.set('type', payload.type);
    setSearchParams(newSearch);
    setType(payload.type);
    setRequestBody((prevState) => {
      return { ...prevState, Query: payload.value };
    });
  };

  const lastPostIndex = currentPage * postsPerPage;
  const firstPostIndex = lastPostIndex - postsPerPage;
  const currentPosts = jsonData.slice(firstPostIndex, lastPostIndex);
  return (
    <Main>
      <Styled.MainSearchbar>
        <Background />
        <Searchbar handleSubmit={handleSubmit} />
      </Styled.MainSearchbar>
      <Styled.MainWrapper>
        <Styled.MainContent>
          <Styled.FoundHeader>
            <h2>Your Search Result</h2>
            <Pagination
              totalPages={totalPages}
              currentPage={currentPage}
              setCurrentPage={setCurrentPage}
            />
          </Styled.FoundHeader>
          <Styled.FoundContent>
            <Filter />
            <div className='divider'></div>
            {authorIsLoading || keywordIsLoading ? (
              <ProgressBar width='50' height='50' borderColor='#98A4DF' barColor='#747DAB' />
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
