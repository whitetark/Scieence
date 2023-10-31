import { Field, Form, Formik } from 'formik';
import React from 'react';
import * as Yup from 'yup';

import { useRegister } from '../../hooks/use-auth';
import Button from '../UI/Button';
import Loading from '../UI/Loading';
import AuthWrapper from './AuthWrapper';

const DisplayingErrorMessagesSchema = Yup.object().shape({
  login: Yup.string().min(3, 'Too Short!').max(12, 'Too Long!').required('Required'),
  password: Yup.string()
    .min(3, 'Too Short!')
    .max(12, 'Must be 12 characters or less')
    .required('Required'),
  repeatPassword: Yup.string()
    .min(3, 'Too Short!')
    .max(12, 'Must be 12 characters or less')
    .required('Required')
    .oneOf([Yup.ref('password')], 'Passwords must match'),
});

const Register = (props) => {
  const { mutateAsync: register, error: registerError } = useRegister();

  return (
    <AuthWrapper onClick={props.onClick} onToggle={props.onToggle} type='Register'>
      <Formik
        initialValues={{
          login: '',
          password: '',
          repeatPassword: '',
        }}
        validationSchema={DisplayingErrorMessagesSchema}
        onSubmit={async (values, actions) => {
          const user = {
            username: values.login,
            password: values.password,
          };

          await register(user).then(() => {
            props.onHide();
          });
          actions.resetForm();
          actions.setSubmitting(false);
        }}>
        {({ errors, touched, isValid, isSubmitting }) => (
          <Form>
            <Field type='text' name='login' placeholder='Login' />
            {errors.login && touched.login ? <div>{errors.login}</div> : null}
            <Field type='password' placeholder='Password' name='password' />
            {errors.password && touched.password ? <div>{errors.password}</div> : null}
            <Field type='password' placeholder='Password' name='repeatPassword' />
            {errors.repeatPassword && touched.repeatPassword ? (
              <div>{errors.repeatPassword}</div>
            ) : null}
            {registerError ? <div>{registerError.response.data}</div> : null}
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

export default Register;
