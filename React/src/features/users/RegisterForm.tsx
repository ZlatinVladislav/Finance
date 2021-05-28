import { ErrorMessage, Form, Formik } from "formik";
import React from "react";
import MyTextInput from "../../app/common/form/MyTextInput";
import { Button, Header } from "semantic-ui-react";
import { useStore } from "../../app/stores/store";
import { observer } from "mobx-react-lite";
import * as Yup from 'yup'
import ValidationError from "../errors/ValidationError";

export default observer(function RegisterForm() {
    const {userStore} = useStore();
    return (
        <Formik
            initialValues={{displayName: '', username: '', email: '', password: '', error: null}}
            onSubmit={(values, {setErrors}) =>
                userStore.register(values).catch(error => setErrors({error}))}
            validationSchema={Yup.object({
                displayName: Yup.string().required(),
                username: Yup.string().required(),
                email: Yup.string().required().email(),
                password: Yup.string().required(),
            })}
        >
            {({handleSubmit, isSubmitting, errors, isValid, dirty}) => (
                <Form className='ui form error' onSubmit={handleSubmit} autoComplete='off'>
                    <Header as='h2' content='Sign up to Finance' color='teal' textAlign='center'/>
                    <MyTextInput placeholder='DisplayName' name='displayName'/>
                    <MyTextInput placeholder='Username' name='username'/>
                    <MyTextInput placeholder='Email' name='email'/>
                    <MyTextInput placeholder='Password' name='password' type='password'/>
                    <ErrorMessage name='error' render={() =>
                        <ValidationError errors={errors.error}/>}
                    />
                    <Button disabled={!isValid || !dirty || isSubmitting} loading={isSubmitting} positive
                            content='Register' type='submit' fluid/>
                </Form>
            )}
        </Formik>
    )
})