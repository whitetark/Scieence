import { Routes, Route, BrowserRouter, useLocation } from 'react-router-dom'
import './App.css'
import RootLayout from './pages/Root'
import ErrorPage from './pages/Error'
import HomePage from './pages/Home'
import { library } from '@fortawesome/fontawesome-svg-core'
import { faUser, faHeart, faMagnifyingGlass } from '@fortawesome/free-solid-svg-icons'

library.add(faUser, faHeart, faMagnifyingGlass)

function App() {
  return (
    <BrowserRouter basename='/'>
      <Routes>
        <Route path='/' element={<RootLayout />} errorElement={<ErrorPage />}>
          <Route index element={<HomePage />} />
        </Route>
      </Routes>
    </BrowserRouter>
  )
}

export default App
