import { RouterProvider, createBrowserRouter } from 'react-router-dom'
import './App.css'
import RootLayout from './pages/Root'
import ErrorPage from './pages/Error'
import HomePage from './pages/Home'

function App() {
  const router = createBrowserRouter([
    {
      path: '/',
      element: <RootLayout />,
      errorElement: <ErrorPage />,
      children: [
        {
          index: true,
          element: <HomePage />,
        },
      ],
    },
  ])
  return <RouterProvider router={router} />
}

export default App
