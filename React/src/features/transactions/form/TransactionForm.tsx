import React, { useEffect, useState } from "react";
import { Button, Header, Segment } from "semantic-ui-react";
import { useStore } from "../../../app/stores/store";
import { observer } from "mobx-react-lite";
import { Link, useHistory, useParams } from "react-router-dom";
import LoadingComponent from "../../../app/layout/LoadingComponent";
import { v4 as uuid } from 'uuid';
import { ErrorMessage, Form, Formik } from "formik";
import * as Yup from 'yup'
import MyTextInput from "../../../app/common/form/MyTextInput";
import MySelectInput from "../../../app/common/form/MySelectInput";
import MyDateInput from "../../../app/common/form/MyDateInput";
import { TransactionFormValues } from "../../../app/models/transaction";
import ValidationError from "../../errors/ValidationError";
import MyRadioInput from "../../../app/common/form/MyRadioInput";

export default observer(function TransactionForm() {
    const history = useHistory();
    const {
        transactionTypeStore: {
            loadTransactionTypes,
            transactionTypesOptionsArray,
            loadingTransactionTypesAll
        }, transactionStore
    } = useStore();
    const {
        createTransaction,
        updateTransaction,
        loadTransactions,
        loadingInitial
    } = transactionStore;
    const {id} = useParams<{ id: string }>();

    const [transaction, setTransaction] = useState<TransactionFormValues>(new TransactionFormValues());

    const validationSchema = Yup.object({
        money: Yup.number().min(1).required('The money field is required and greater than 0'),
        transactionStatus: Yup.boolean().required('The transaction status field is required'),
        transactionTypeId: Yup.string().required('The transaction type field is required'),
        dateTransaction: Yup.string().required('The transaction date field is required').nullable()
    })

    useEffect(() => {
        if (id) {
            // @ts-ignore
            loadTransactions(id).then(transaction => setTransaction(new TransactionFormValues(transaction)));
        }
        loadingTransactionTypesAll();
    }, [id, loadTransactions, loadTransactionTypes])

    function handleFormSubmit(transaction: TransactionFormValues) {
        if (!transaction.id) {
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
                enableReinitialize initialValues={transaction}
                onSubmit={(values, {setErrors}) => handleFormSubmit(values)}>
                {({handleSubmit, isValid, isSubmitting, dirty, errors}) => (
                    <Form className='ui form' onSubmit={handleSubmit} autoComplete='off'>
                        <MyRadioInput placeholder='TransactionStatus'
                                      name='transactionStatus' label='Income'/>
                        <MyTextInput placeholder='Money' name='money'/>
                        <MySelectInput options={transactionTypesOptionsArray} placeholder='TransactionType'
                                       name='transactionType'/>
                        <MyDateInput
                            placeholderText='TransactionDate'
                            name='dateTransaction'
                            dateFormat='d MMMM,yyyy'/>
                        <ErrorMessage name='error' render={() =>
                            <ValidationError errors={errors.error}/>}
                        />
                        <Button
                            disabled={isSubmitting || !dirty || !isValid}
                            loading={isSubmitting} floated='right' positive type='submit' content='Submit'/>
                        {transaction.id === 'undefined' ?
                            <Button as={Link} to={`/transactions`} floated='right' type='button'
                                    content='Cancel'/> :
                            <Button as={Link} to={`/transactions/${transaction.id}`} floated='right' type='button'
                                    content='Cancel'/>}
                    </Form>
                )}
            </Formik>
        </Segment>
    )
})