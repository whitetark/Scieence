import React from 'react';
import { Outlet } from 'react-router-dom';

import Header from '../components/Header/Header';
import { useFetchData } from '../hooks/use-auth';

const RootLayout = () => {
  return (
    <>
      <Header />
      <Outlet />
    </>
  );
};

export const loader = async (params) => {
  useFetchData();
};

export default RootLayout;
