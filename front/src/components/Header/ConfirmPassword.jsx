import { ErrorMessage, Field, Form, Formik } from 'formik';
import React from 'react';
import { ProgressBar } from 'react-loader-spinner';
import * as Yup from 'yup';
import { useConfirmationData } from '../../hooks/use-auth';
import { Error } from '../../styles/UI.styled';
import Button from '../UI/Button';

const ConfirmPassword = ({ setIsConfirmed, username }) => {
  const { mutateAsync: checkCredentials, error: confirmationError } = useConfirmationData();

  return (
    <Formik
      initialValues={{ password: '' }}
      validationSchema={Yup.object().shape({
        password: Yup.string()
          .min(3, 'Too Short!')
          .max(12, 'Must be 12 characters or less')
          .required('Required'),
      })}
      onSubmit={(values, actions) => {
        const user = {
          username: username,
          password: values.password,
        };
        checkCredentials(user).then(() => {
          setIsConfirmed(true);
        });
        actions.resetForm();
        actions.setSubmitting(false);
      }}>
      {({ errors, touched, isValid, isSubmitting }) => (
        <Form>
          <Field
            type='password'
            name='password'
            placeholder='Current Password'
            className={errors.password && touched.password ? 'error' : undefined}
          />
          <ErrorMessage name='password' component={Error} />
          {confirmationError ? confirmationError.response.data : null}
          {isSubmitting ? (
            <ProgressBar width='50' height='50' borderColor='#98A4DF' barColor='#747DAB' />
          ) : (
            <Button type='submit' disabled={!isValid}>
              Check
            </Button>
          )}
        </Form>
      )}
    </Formik>
  );
};

export default ConfirmPassword;
