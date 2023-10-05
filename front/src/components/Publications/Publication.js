import React from 'react'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'

import * as Styled from '../../styles/Publications.styled'

const Publication = (props) => {
  return (
    <Styled.Publication onClick={props.onClick}>
      <Styled.PublicationActions>
        <button>
          <FontAwesomeIcon icon='fa-regular fa-heart' fixedWidth />
        </button>
        <button onClick={props.hide}>
          <FontAwesomeIcon icon='fa-solid fa-xmark' fixedWidth />
        </button>
      </Styled.PublicationActions>
      <Styled.PublicationMain>
        <h1>Title Of Publication</h1>
        <p className='authors'>
          Some author, some author, some author, some author, some author, some author, some author,
          some author
        </p>
        <p className='details'>
          Detailed description, Detailed description, Detailed description, Detailed description,
          Detailed description, Detailed description, Detailed description, Detailed description,
          Detailed description, Detailed description, Detailed description, Detailed description,
          Detailed description, Detailed description, Detailed description, Detailed description,
          Detailed description, Detailed description, Detailed description,
        </p>
        <Styled.PublicationDetails>
          <Styled.PublicationInfo>
            <p>
              Publication Date: <span>xx.xx.xxxx</span>
            </p>
            <p>
              Genre: <span>some genre</span>
            </p>
            <p>
              Language: <span>English</span>
            </p>
            <p>
              Publication Type: <span>some type</span>
            </p>
          </Styled.PublicationInfo>
          <Styled.PublicationLinks>
            <p>
              URL: <a href='https://somelinkwithpub.com'>https://somelinkwithpub.com</a>
            </p>
            <div>
              <span>Keywords:</span>
              <Styled.PublicationKeywords>
                <span>Keyword</span>
                <span>Keyword</span>
                <span>Keyword</span>
                <span>Keyword</span>
                <span>Keyword</span>
                <span>Keyword</span>
              </Styled.PublicationKeywords>
            </div>
          </Styled.PublicationLinks>
        </Styled.PublicationDetails>
      </Styled.PublicationMain>
    </Styled.Publication>
  )
}

export default Publication
