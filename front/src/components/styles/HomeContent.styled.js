import { styled } from 'styled-components'
import * as variables from '../../scss/Variables.js'
import { ContainerWrapper } from '../styles/UI.styled'

export const Main = styled.main`
  display: flex;
  flex: 1;
  color: ${variables.WHITE_COLOR};
`
export const MainWrapper = styled(ContainerWrapper)`
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  max-width: 60rem;
  z-index: 1;
  > div:not(:last-child) {
    margin-bottom: 2rem;
  }

  .advanced {
    border-bottom: 1px solid ${variables.WHITE_COLOR};
    align-self: flex-end;
    margin-bottom: 1rem;
    transition: all 0.2s ease-out;
    &:hover {
      color: ${variables.GRAY_COLOR};
      border-color: ${variables.GRAY_COLOR};
    }
  }
`
export const Background = styled.div`
  position: absolute;
  width: 100%;
  height: 100%;
`
export const BackgroundItem = styled.div`
  position: absolute;
  width: 100%;
  height: 100%;
  background-image: url('https://trumpwallpapers.com/wp-content/uploads/Science-Wallpaper-27-1920x1080-1.png');
  background-repeat: no-repeat;
  background-position: center;
  background-size: cover;
  filter: blur(5px) brightness(51%);
`
