import React, { useEffect, useState } from "react";
import { Button, Header, Label, Segment } from "semantic-ui-react";
import { useStore } from "../../../app/stores/store";
import { observer } from "mobx-react-lite";
import { Link, useHistory, useParams } from "react-router-dom";
import LoadingComponent from "../../../app/layout/LoadingComponent";
import { v4 as uuid } from 'uuid';
import { ErrorMessage, Form, Formik } from "formik";
import * as Yup from 'yup'
import MyTextInput from "../../../app/common/form/MyTextInput";
import { TransactionType, TransactionTypeFormValues } from "../../../app/models/transactionType";
import { TransactionFormValues } from "../../../app/models/transaction";

export default observer(function TransactionForm() {
    const history = useHistory();
    const {transactionTypeStore} = useStore();
    const {
        createTransactionType,
        updateTransactionType,
        loading,
        loadTransactionTypes,
        loadingInitial
    } = transactionTypeStore;
    const {id} = useParams<{ id: string }>();

    const [transactionType, setTransactionType] = useState<TransactionTypeFormValues>(new TransactionTypeFormValues());

    const validationSchema = Yup.object({
        transactionType: Yup.string().required('The transaction type field is required'),
    })

    useEffect(() => {
        if (id) {
            // @ts-ignore
            loadTransactionTypes(id).then(transactionType => setTransactionType(new TransactionTypeFormValues(transactionType)));
        }
    }, [id, loadTransactionTypes])

    function handleFormSubmit(transactionType: TransactionTypeFormValues) {
        if (!transactionType.id) {
            let newTransactionType = {
                ...transactionType, id: uuid()
            };
            createTransactionType(newTransactionType)
                .then(() => history.push(`/transactionTypes`))
        } else {
            updateTransactionType(transactionType)
                .then(() => history.push(`/transactionTypes`))
        }
    }

    if (loadingInitial) return <LoadingComponent content='Loading transactionTypes...'/>

    return (
        <Segment clearing>
            <Header content='TransactionType Details' sub color='teal'/>
            <Formik
                validationSchema={validationSchema}
                enableReinitialize initialValues={transactionType}
                onSubmit={(values, {setErrors}) => handleFormSubmit(values)}>
                {({handleSubmit, isValid, isSubmitting, dirty, errors}) => (
                    <Form className='ui form' onSubmit={handleSubmit} autoComplete='off'>
                        <MyTextInput placeholder='TransactionType'
                                     name='transactionType'/>
                        <Button
                            disabled={isSubmitting || !dirty || !isValid}
                            loading={isSubmitting} floated='right' positive type='submit' content='Submit'/>
                        <Button as={Link} to={`/transactionTypes`} floated='right' type='button'
                                content='Cancel'/>
                    </Form>
                )}
            </Formik>
        </Segment>
    )
})