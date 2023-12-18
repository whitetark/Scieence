import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import React from 'react';

import { useAuthContext } from '../../app/store/auth-context';
import * as Styled from '../../styles/Publications.styled';

const Publication = ({ data, onClick, hide }) => {
  const { userData, updateUser } = useAuthContext();
  const addPublicationHandler = () => {
    let user = userData;
    console.log(userData);
    data.authors = data.authors.join(', ');
    data.subjects = data.subjects.join(', ');
    user.favourites.push(data);
    updateUser(user);
  };

  return (
    <Styled.Publication onClick={onClick}>
      <Styled.PublicationActions>
        <button onClick={addPublicationHandler}>
          <FontAwesomeIcon icon='fa-regular fa-heart' fixedWidth />
        </button>
        <button onClick={hide}>
          <FontAwesomeIcon icon='fa-solid fa-xmark' fixedWidth />
        </button>
      </Styled.PublicationActions>
      <Styled.PublicationMain>
        <h1 className='title'>{data.title}</h1>
        <p className='authors'>{data.authors.join(', ')}</p>
        <p className='details'>{data.description}</p>
        <Styled.PublicationDetails>
          <Styled.PublicationInfo>
            <p>
              Publication Date: <span>{data.publicationDate}</span>
            </p>
            <p>
              Genre: <span>{data.Genre}</span>
            </p>
            <p>
              Language: <span>{data.language}</span>
            </p>
            <p>
              Publication Type: <span>{data.publicationType.split(' ')[0]}</span>
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
              {data.subjects ? (
                data.subjects.map((keyword, index) => {
                  const first = keyword.split(' ')[0];
                  return <span key={index}>{first}</span>;
                })
              ) : (
                <span>No Keywords</span>
              )}
            </Styled.PublicationKeywords>
          </Styled.PublicationLinks>
        </Styled.PublicationDetails>
      </Styled.PublicationMain>
    </Styled.Publication>
  );
};

export default Publication;
