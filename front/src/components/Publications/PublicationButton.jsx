import React from 'react';

import useModal from '../../hooks/use-modal';
import * as Styled from '../../styles/Publications.styled';
import Modal from '../UI/Modal';
import Publication from './Publication';

const PublicationButton = (props) => {
  const { isShowing, toggle } = useModal();
  const data = props.data;

  return (
    <>
      {isShowing && (
        <Modal isShowing={isShowing} hide={toggle} className={'pub-modal'} hasOverlay>
          <Publication hide={toggle} data={data} />
        </Modal>
      )}
      <Styled.PublicationButton onClick={toggle}>
        <h4 className='title'>{data.title}</h4>
        <p className='details'>{data.description || 'No description.'}</p>
        <p className='authors'>
          {data.authors ? data.authors.replaceAll(';', ', ') : 'No authors.'}
        </p>
      </Styled.PublicationButton>
    </>
  );
};

export default PublicationButton;
