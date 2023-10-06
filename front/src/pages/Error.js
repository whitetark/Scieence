import React from 'react'
import { useRouteError } from 'react-router-dom'

const ErrorPage = () => {
  const error = useRouteError()

  let title = 'Something went wrong!'
  return (
    <>
      <div>{title}</div>
      <div>{error.message}</div>
    </>
  )
}

export default ErrorPage
