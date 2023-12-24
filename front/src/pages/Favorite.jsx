import React, { useEffect, useState } from 'react';

import { useSearchParams } from 'react-router-dom';
import { useAuthContext } from '../app/store/auth-context';
import FilterClient from '../components/Home/FilterClient';
import PublicationList from '../components/Publications/PublicationList';
import Background from '../components/UI/Background';
import Pagination from '../components/UI/Pagination';
import * as Styled from '../styles/Results.styled';
import { Main } from '../styles/UI.styled';

const FavoritePage = () => {
  const [currentPage, setCurrentPage] = useState(1);
  const [totalPages, setTotalPages] = useState(2);
  const [searchParams, setSearchParams] = useSearchParams();
  const [favourites, setFavourites] = useState([]);
  const postsPerPage = 4;
  const { userData } = useAuthContext();

  useEffect(() => {
    const pageValue = parseInt(searchParams.get('page'));
    if (pageValue) {
      setCurrentPage(pageValue);
    }
  }, []);
  useEffect(() => {
    favourites && setTotalPages(Math.ceil(favourites.length / postsPerPage));
  }, [favourites]);
  useEffect(() => {
    const newSearch = new URLSearchParams(searchParams);
    newSearch.set('page', currentPage);
    setSearchParams(newSearch);
  }, [currentPage]);
  useEffect(() => {
    setFavourites(userData.favourites);
  }, [userData.favourites]);

  const clientFiltersHandler = (filtersValue) => {
    let rawData = favourites ? Array.from(favourites) : [];

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

    setFavourites(sorted);
  };

  const lastPostIndex = currentPage * postsPerPage;
  const firstPostIndex = lastPostIndex - postsPerPage;
  let currentPosts = favourites && favourites.slice(firstPostIndex, lastPostIndex);

  return (
    <Main>
      <Styled.MainSearchbar>
        <Background />
      </Styled.MainSearchbar>
      <Styled.MainWrapper>
        <Styled.MainContent>
          <Styled.FoundHeader>
            <h2>Favourite Publications</h2>
            {favourites && favourites.length > 0 ? (
              <Pagination
                totalPages={totalPages}
                currentPage={currentPage}
                setCurrentPage={setCurrentPage}
              />
            ) : undefined}
          </Styled.FoundHeader>
          <Styled.FoundContent>
            <Styled.Filters>
              <FilterClient getClientFilters={clientFiltersHandler} />
            </Styled.Filters>
            <div className='divider'></div>
            {favourites && <PublicationList data={currentPosts} />}
          </Styled.FoundContent>
        </Styled.MainContent>
      </Styled.MainWrapper>
    </Main>
  );
};

export default FavoritePage;
