import React from 'react'

import * as Styled from '../../styles/UI.styled'
import BackgroundImg from '../../assets/background.png'

const Background = () => {
  return (
    <Styled.BackgroundWrapper>
      <Styled.BackgroundItem image={BackgroundImg} />
    </Styled.BackgroundWrapper>
  )
}

export default Background
