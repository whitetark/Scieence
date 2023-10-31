import { Field, Form, Formik } from 'formik';
import React from 'react';
import * as Yup from 'yup';
import { useConfirmationData } from '../../hooks/use-auth';
import Button from '../UI/Button';
import Loading from '../UI/Loading';

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
          <Field type='password' name='password' placeholder='Current Password' />
          {errors.password && touched.password ? <div>{errors.password}</div> : null}
          {confirmationError ? confirmationError.response.data : null}
          {isSubmitting ? (
            <Loading />
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
