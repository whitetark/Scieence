import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import React from 'react';

import { useAuthContext } from '../../app/store/auth-context';
import * as Styled from '../../styles/Publications.styled';

const Publication = ({ data, onClick, hide }) => {
  const { userData, addPublicationToUser, removePublicationFromUser } = useAuthContext();
  const userLikePub = userData.favourites.some((publication) =>
    data.doi ? publication.doi === data.doi : publication.url === data.url,
  );
  const addPublicationHandler = () => {
    const request = {
      account: userData,
      publicationToAdd: data,
    };

    addPublicationToUser(request);
  };

  const removePublicationHandler = () => {
    const request = {
      account: userData,
      publicationToAdd: data,
    };

    removePublicationFromUser(request);
    hide();
  };

  return (
    <Styled.Publication onClick={onClick}>
      <Styled.PublicationActions>
        {userData ? (
          userLikePub ? (
            <button onClick={removePublicationHandler}>
              <FontAwesomeIcon icon='fa-solid fa-heart' fixedWidth />
            </button>
          ) : (
            <button onClick={addPublicationHandler}>
              <FontAwesomeIcon icon='fa-regular fa-heart' fixedWidth />
            </button>
          )
        ) : undefined}
        <button onClick={hide}>
          <FontAwesomeIcon icon='fa-solid fa-xmark' fixedWidth />
        </button>
      </Styled.PublicationActions>
      <Styled.PublicationMain>
        <h1 className='title'>{data.title}</h1>
        <p className='authors'>{data.authors ? data.authors.replace('; ', ', ') : 'No authors.'}</p>
        <p className='details'>{data.description || 'No description.'}</p>
        <Styled.PublicationDetails>
          <Styled.PublicationInfo>
            <p>
              Publication Date: <span>{data.publicationDate || data.publicationYear}</span>
            </p>
            <p>
              Language: <span>{data.language || 'eng'}</span>
            </p>
            <p>
              Publication Type:{' '}
              <span>{data.publicationType ? data.publicationType.split(' ')[0] : 'Article'}</span>
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
                data.subjects.split('; ').map((keyword, index) => {
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
