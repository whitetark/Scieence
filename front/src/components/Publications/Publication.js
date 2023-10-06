import React from 'react'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'

import * as Styled from '../../styles/Publications.styled'

const Publication = ({ data, onClick, hide }) => {
  return (
    <Styled.Publication onClick={onClick}>
      <Styled.PublicationActions>
        <button>
          <FontAwesomeIcon icon='fa-regular fa-heart' fixedWidth />
        </button>
        <button onClick={hide}>
          <FontAwesomeIcon icon='fa-solid fa-xmark' fixedWidth />
        </button>
      </Styled.PublicationActions>
      <Styled.PublicationMain>
        <h1>{data.title}</h1>
        <p className='authors'>{data.authors.join(', ')}</p>
        <p className='details'>{data.description}</p>
        <Styled.PublicationDetails>
          <Styled.PublicationInfo>
            <p>
              Publication Date: <span>{data.publication_date}</span>
            </p>
            <p>
              Genre: <span>{data.genre}</span>
            </p>
            <p>
              Language: <span>{data.language}</span>
            </p>
            <p>
              Publication Type: <span>{data.publication_type}</span>
            </p>
          </Styled.PublicationInfo>
          <Styled.PublicationLinks>
            <p>
              URL:{' '}
              <a href={data.url} target='_blank' rel='noopener noreferrer'>
                {data.url}
              </a>
            </p>
            <span>Keywords:</span>
            <Styled.PublicationKeywords>
              {data.keywords.map((keyword, index) => {
                return <span key={index}>{keyword}</span>
              })}
            </Styled.PublicationKeywords>
          </Styled.PublicationLinks>
        </Styled.PublicationDetails>
      </Styled.PublicationMain>
    </Styled.Publication>
  )
}

export default Publication
