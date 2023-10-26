import React from 'react';
import ReactDOM from 'react-dom/client';
import { QueryClient, QueryClientProvider } from 'react-query';

import { AuthContextProvider } from './app/store/auth-context';
import './index.css';
import App from './App';

const queryClient = new QueryClient({
  defaultOptions: {
    queries: {
      refetchOnWindowFocus: false,
    },
  },
});

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <React.StrictMode>
    <QueryClientProvider client={queryClient}>
      <AuthContextProvider>
        <App />
      </AuthContextProvider>
    </QueryClientProvider>
  </React.StrictMode>,
);
