import { library } from '@fortawesome/fontawesome-svg-core';
import { faHeart as faHeartRegular } from '@fortawesome/free-regular-svg-icons';
import {
  faArrowLeft,
  faArrowRight,
  faHeart,
  faMagnifyingGlass,
  faUser,
  faXmark,
} from '@fortawesome/free-solid-svg-icons';
import { BrowserRouter, Route, Routes } from 'react-router-dom';

import ErrorPage from './pages/Error';
import FavoritePage from './pages/Favorite';
import HomePage from './pages/Home';
import NotFound from './pages/NotFound';
import RootLayout from './pages/Root';
import SearchPage from './pages/Search';
library.add(faUser, faHeart, faMagnifyingGlass, faXmark, faHeartRegular, faArrowLeft, faArrowRight);

function App() {
  return (
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
  );
}

export default App;
