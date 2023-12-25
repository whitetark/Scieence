import { ErrorMessage, Field, Form, Formik } from 'formik';
import React from 'react';
import * as Yup from 'yup';

import { ProgressBar } from 'react-loader-spinner';
import { useLogin } from '../../hooks/use-auth';
import { Error } from '../../styles/UI.styled';
import Button from '../UI/Button';
import AuthWrapper from './AuthWrapper';

const DisplayingErrorMessagesSchema = Yup.object().shape({
  login: Yup.string().min(3, 'Too Short!').max(12, 'Too Long!').required('Required'),
  password: Yup.string()
    .min(3, 'Too Short!')
    .max(12, 'Must be 12 characters or less')
    .required('Required'),
});

const Login = (props) => {
  const { mutateAsync: login, error: loginError } = useLogin();
  return (
    <AuthWrapper onClick={props.onClick} onToggle={props.onToggle} type='Login'>
      <Formik
        initialValues={{
          login: '',
          password: '',
        }}
        validationSchema={DisplayingErrorMessagesSchema}
        onSubmit={async (values, actions) => {
          const user = {
            username: values.login,
            password: values.password,
          };

          await login(user).then(() => {
            props.onHide();
            actions.resetForm();
          });
          actions.setSubmitting(false);
        }}>
        {({ errors, touched, isValid, isSubmitting }) => (
          <Form>
            <Field
              type='text'
              name='login'
              placeholder='Login'
              className={errors.login && touched.login ? 'error' : undefined}
            />
            <ErrorMessage name='login' component={Error} />
            <Field
              type='password'
              placeholder='Password'
              name='password'
              className={errors.password && touched.password ? 'error' : undefined}
            />
            <ErrorMessage name='password' component={Error} />
            {loginError ? (
              loginError.response.data.length < 15 ? (
                <div>{loginError.response.data}</div>
              ) : (
                <div>Server error!</div>
              )
            ) : null}
            {isSubmitting ? (
              <ProgressBar width='50' height='50' borderColor='#98A4DF' barColor='#747DAB' />
            ) : (
              <Button type='submit' disabled={!isValid}>
                Login
              </Button>
            )}
          </Form>
        )}
      </Formik>
    </AuthWrapper>
  );
};

export default Login;
