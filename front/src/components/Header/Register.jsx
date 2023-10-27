import React from 'react';
import { Formik, Field, Form } from 'formik';
import * as Yup from 'yup';

import Button from '../UI/Button';
import AuthWrapper from './AuthWrapper';
import { useRegister } from '../../hooks/use-auth';
import Loading from '../UI/Loading';

const DisplayingErrorMessagesSchema = Yup.object().shape({
  login: Yup.string().min(3, 'Too Short!').max(12, 'Too Long!').required('Required'),
  password: Yup.string()
    .min(3, 'Too Short!')
    .max(12, 'Must be 12 characters or less')
    .required('Required'),
  repeatPassword: Yup.string()
    .min(3, 'Too Short!')
    .max(12, 'Must be 12 characters or less')
    .required('Required'),
});

const repeatPasswordValidation = (value, passwordValue) => {
  return value === passwordValue;
};

const Register = (props) => {
  const { mutateAsync: register } = useRegister();

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

          await register(user);
          props.onHide();
          actions.setSubmitting(false);
          actions.resetForm();
        }}>
        {({ values, errors, touched, isValid, isSubmitting }) => (
          <Form>
            <Field type='text' name='login' placeholder='Login' />
            {errors.login && touched.login ? <div>{errors.login}</div> : null}
            <Field type='password' placeholder='Password' name='password' />
            {errors.password && touched.password ? <div>{errors.password}</div> : null}
            <Field
              type='password'
              placeholder='Password'
              name='repeatPassword'
              validate={() => repeatPasswordValidation(values.repeatPassword, values.password)}
            />
            {errors.repeatPassword && touched.repeatPassword ? (
              <div>{errors.repeatPassword}</div>
            ) : null}
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
