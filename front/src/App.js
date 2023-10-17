import { Routes, Route, BrowserRouter } from 'react-router-dom';
import { library } from '@fortawesome/fontawesome-svg-core';
import {
  faUser,
  faHeart,
  faMagnifyingGlass,
  faXmark,
  faArrowRight,
  faArrowLeft,
} from '@fortawesome/free-solid-svg-icons';
import { faHeart as faHeartRegular } from '@fortawesome/free-regular-svg-icons';

import RootLayout from './pages/Root';
import ErrorPage from './pages/Error';
import HomePage from './pages/Home';
import FavoritePage from './pages/Favorite';
import SearchPage from './pages/Search';
import NotFound from './pages/NotFound';
import { AuthContextProvider } from './app/store/auth-context';

library.add(faUser, faHeart, faMagnifyingGlass, faXmark, faHeartRegular, faArrowLeft, faArrowRight);

function App() {
  return (
    <AuthContextProvider>
      <BrowserRouter basename='/'>
        <Routes>
          <Route path='/' element={<RootLayout />} errorElement={<ErrorPage />}>
            <Route index element={<HomePage />} />
            <Route path='/favorite' element={<FavoritePage />} />
            <Route path='/search' element={<SearchPage />} />
            <Route path='*' element={<NotFound />} />
          </Route>
        </Routes>
      </BrowserRouter>
    </AuthContextProvider>
  );
}

export default App;
