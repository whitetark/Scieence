import React, { useEffect, useState } from 'react'

import * as Styled from '../styles/Results.styled'
import { Main } from '../styles/UI.styled'
import Searchbar from '../components/UI/Searchbar'
import Background from '../components/UI/Background'
import Filter from '../components/UI/Filter'
import Pagination from '../components/UI/Pagination'
import PublicationList from '../components/Publications/PublicationList'
import data from '../store/data.json'
import { useSearchParams } from 'react-router-dom'

const SearchPage = () => {
  const [jsonData, setJsonData] = useState([])
  const [currentPage, setCurrentPage] = useState('')
  const [totalPages, setTotalPages] = useState(2)
  const [searchParams, setSearchParams] = useSearchParams()
  const postsPerPage = 4

  useEffect(() => {
    setJsonData(data.data)
    const pageValue = parseInt(searchParams.get('page'))
    if (pageValue) {
      setCurrentPage(pageValue)
    } else {
      setCurrentPage(1)
    }
  }, [])

  useEffect(() => {
    setTotalPages(Math.ceil(jsonData.length / postsPerPage))
  }, [jsonData])

  useEffect(() => {
    setSearchParams({ page: currentPage })
  }, [currentPage])

  const lastPostIndex = currentPage * postsPerPage
  const firstPostIndex = lastPostIndex - postsPerPage
  const currentPosts = jsonData.slice(firstPostIndex, lastPostIndex)
  return (
    <Main>
      <Styled.MainSearchbar>
        <Background />
        <Searchbar />
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
            {jsonData.length === 0 ? (
              <div>Loading...</div>
            ) : (
              <PublicationList data={currentPosts} />
            )}
          </Styled.FoundContent>
        </Styled.MainContent>
      </Styled.MainWrapper>
    </Main>
  )
}

export default SearchPage