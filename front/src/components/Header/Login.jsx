import React from 'react';
import { Formik, Field, Form } from 'formik';
import * as Yup from 'yup';

import Button from '../UI/Button';
import AuthWrapper from './AuthWrapper';
import Loading from '../UI/Loading';
import { useLogin } from '../../hooks/use-auth';

const DisplayingErrorMessagesSchema = Yup.object().shape({
  login: Yup.string().min(3, 'Too Short!').max(12, 'Too Long!').required('Required'),
  password: Yup.string()
    .min(3, 'Too Short!')
    .max(12, 'Must be 12 characters or less')
    .required('Required'),
});

const Login = (props) => {
  const { mutateAsync: login } = useLogin();
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

          await login(user);
          props.onHide();
          actions.setSubmitting(false);
          actions.resetForm();
        }}>
        {({ errors, touched, isValid, isSubmitting }) => (
          <Form>
            <Field type='text' name='login' placeholder='Login' />
            {errors.login && touched.login ? <div>{errors.login}</div> : null}
            <Field type='password' placeholder='Password' name='password' />
            {errors.password && touched.password ? <div>{errors.password}</div> : null}
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
