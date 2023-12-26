import { ErrorMessage, Field, Form, Formik } from 'formik';
import React from 'react';
import * as Yup from 'yup';

import { ProgressBar } from 'react-loader-spinner';
import { useRegister } from '../../hooks/use-auth';
import { Error } from '../../styles/UI.styled';
import Button from '../UI/Button';
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
            <Field
              type='password'
              placeholder='Password'
              name='repeatPassword'
              className={errors.repeatPassword && touched.repeatPassword ? 'error' : undefined}
            />
            <ErrorMessage name='repeatPassword' component={Error} />
            {registerError ? <div>{registerError.response.data}</div> : null}
            {isSubmitting ? (
              <ProgressBar width='50' height='50' borderColor='#98A4DF' barColor='#747DAB' />
            ) : (
              <Button type='submit' disabled={!isValid}>
                Register
              </Button>
            )}
          </Form>
        )}
      </Formik>
    </AuthWrapper>
  );
};

export default Register;
