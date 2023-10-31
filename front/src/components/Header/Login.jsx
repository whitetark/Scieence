import { Field, Form, Formik } from 'formik';
import React from 'react';
import * as Yup from 'yup';

import { useLogin } from '../../hooks/use-auth';
import Button from '../UI/Button';
import Loading from '../UI/Loading';
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
            <Field type='text' name='login' placeholder='Login' />
            {errors.login && touched.login ? <div>{errors.login}</div> : null}
            <Field type='password' placeholder='Password' name='password' />
            {errors.password && touched.password ? <div>{errors.password}</div> : null}
            {loginError ? <div>{loginError.response.data}</div> : null}
            {isSubmitting ? (
              <Loading />
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
