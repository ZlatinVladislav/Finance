import React, { useEffect, useState } from "react";
import { Button, Header, Segment } from "semantic-ui-react";
import { useStore } from "../../../app/stores/store";
import { observer } from "mobx-react-lite";
import { Link, useHistory, useParams } from "react-router-dom";
import LoadingComponent from "../../../app/layout/LoadingComponent";
import { v4 as uuid } from 'uuid';
import { Form, Formik } from "formik";
import * as Yup from 'yup'
import MyTextInput from "../../../app/common/form/MyTextInput";
import MySelectInput from "../../../app/common/form/MySelectInput";
import { categoryOptions } from "../../../app/common/options/categoryOptions";
import MyDateInput from "../../../app/common/form/MyDateInput";
import { Transaction } from "../../../app/models/transaction";
import { Simulate } from "react-dom/test-utils";

export default observer(function TransactionForm() {
    const history = useHistory();
    const {transactionStore} = useStore();
    const {
        createTransaction,
        updateTransaction,
        loading,
        loadTransactions,
        loadingInitial
    } = transactionStore;
    const {id} = useParams<{ id: string }>();

    const [transaction, setTransaction] = useState({
        id: '',
        money: 0,
        transactionStatus: true,
        dateTransaction: null,
        transactionTypeId: ''
    });

    const validationSchema = Yup.object({
        money: Yup.number().required('The money field is required'),
        transactionStatus: Yup.string().required('The transaction status field is required'),
        transactionTypeId: Yup.string().required('The transaction type field is required'),
        dateTransaction: Yup.string().required('The transaction date field is required').nullable()
    })

    useEffect(() => {
        if (id) {
            // @ts-ignore
            loadTransactions(id).then(transaction => setTransaction(transaction));
        }
    }, [id, loadTransactions])

    function handleFormSubmit(transaction: Transaction) {
        if (transaction.id.length === 0) {
            let newTransaction = {
                ...transaction, id: uuid()
            };
            createTransaction(newTransaction)
                .then(() => history.push(`/transactions/${newTransaction.id}`))
        } else {
            updateTransaction(transaction)
                .then(() => history.push(`/transactions/${transaction.id}`))
        }
    }

    if (loadingInitial) return <LoadingComponent content='Loading transaction...'/>

    return (
        <Segment clearing>
            <Header content='Transaction Details' sub color='teal'/>
            <Formik
                validationSchema={validationSchema}
                enableReinitialize initialValues={transaction} onSubmit={values => handleFormSubmit(values)}>
                {({handleSubmit, isValid, isSubmitting, dirty}) => (
                    <Form className='ui form' onSubmit={handleSubmit} autoComplete='off'>
                        <MyTextInput placeholder='Money' name='money'/>
                        <MyTextInput placeholder='TransactionStatus'
                                     name='transactionStatus'/>
                        <MySelectInput options={categoryOptions} placeholder='TransactionType'
                                       name='transactionType'/>
                        <MyDateInput
                            placeholderText='TransactionDate'
                            name='dateTransaction'
                            dateFormat='d MMMM,yyyy'/>
                        <Button
                            disabled={isSubmitting || !dirty || !isValid}
                            loading={isSubmitting} floated='right' positive type='submit' content='Submit'/>
                        <Button as={Link} to={`/transactions/${transaction.id}`} floated='right' type='button' content='Cancel'/>
                    </Form>
                )}
            </Formik>
        </Segment>
    )
})