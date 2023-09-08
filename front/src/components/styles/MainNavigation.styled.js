import { styled } from 'styled-components'
import * as variables from '../../scss/Variables.js'

export const Header = styled.header`
  position: sticky;
  z-index: 1;
  top: 0;
  background-color: ${variables.WHITE_COLOR};
`

export const Logo = styled.div`
  display: flex;
  flex-direction: row;
  align-items: center;
  gap: 0.75rem;

  img {
    width: 4.3rem;
  }
  h1 {
    font-size: 3rem;
    font-weight: 600;
  }
`

export const Nav = styled.nav`
  display: flex;
  flex-direction: row;
  align-items: center;
  justify-content: space-between;
  padding: 1rem 3rem;

  a:link,
  a:visited {
    text-decoration: none;
    color: ${variables.BLACK_COLOR};
  }
`

export const Actions = styled.div`
  display: flex;
  flex-direction: row;
  gap: 1rem;

  > * {
    padding: 0.75rem;
    border-radius: 0.625rem;
    box-shadow: 0px 4px 4px 0px rgba(0, 0, 0, 0.25);
    font-size: 2rem;
    background-color: ${variables.WHITE_COLOR};
    transition: background-color 0.2s ease-out;

    &.active {
      background-color: ${variables.GRAY_COLOR};
    }

    &:hover {
      background-color: ${variables.GRAY_COLOR};
    }
  }
`
