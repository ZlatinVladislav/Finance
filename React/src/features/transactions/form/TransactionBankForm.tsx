import React, { useEffect, useState } from "react";
import { Button, Header, Segment } from "semantic-ui-react";
import { useStore } from "../../../app/stores/store";
import { observer } from "mobx-react-lite";
import { Link, useHistory, useParams } from "react-router-dom";
import LoadingComponent from "../../../app/layout/LoadingComponent";
import { ErrorMessage, Form, Formik } from "formik";
import * as Yup from 'yup'
import MySelectInput from "../../../app/common/form/MySelectInput";
import { TransactionFormValues } from "../../../app/models/transaction";
import { BankFormValues } from "../../../app/models/bank";
import ValidationError from "../../errors/ValidationError";

export default observer(function TransactionForm() {
    const history = useHistory();
    const {
        bankStore: {
            loadBanks,
            banksOptionsArray,
            loadingBanks,
            assignBank
        }, transactionStore
    } = useStore();
    const {
        loadTransactions,
        loadingInitial
    } = transactionStore;
    const {id} = useParams<{ id: string, bankId: string }>();

    const [transaction, setTransaction] = useState<TransactionFormValues>(new TransactionFormValues());
    const [bankTransaction, setBankTransaction] = useState<BankFormValues>(new BankFormValues());

    const validationSchema = Yup.object({
        id: Yup.string().required('The bank field is required'),
    })

    useEffect(() => {
        if (id) {
            // @ts-ignore
            loadTransactions(id).then(transaction => setTransaction(new TransactionFormValues(transaction)));
        }
        loadingBanks();
    }, [id, loadTransactions, loadBanks])

    function handleFormSubmit(bankTransaction: BankFormValues) {
        // @ts-ignore
        assignBank(bankTransaction.id, id)
            .then(() => history.push(`/transactions/${transaction.id}`))
    }

    if (loadingInitial) return <LoadingComponent content='Loading banks...'/>

    return (
        <Segment clearing>
            <Header content='Available Banks' sub color='teal'/>
            <Formik
                validationSchema={validationSchema}
                enableReinitialize initialValues={bankTransaction}
                onSubmit={(values, {setErrors}) => handleFormSubmit(values)}>
                {({handleSubmit, isValid, isSubmitting, dirty, errors}) => (
                    <Form className='ui form' onSubmit={handleSubmit} autoComplete='off'>
                        <MySelectInput options={banksOptionsArray} placeholder='Bank'
                                       name='id'/>
                        <ErrorMessage name='error' render={() =>
                            <ValidationError errors={errors.name}/>}
                        />
                        <Button
                            disabled={isSubmitting || !dirty || !isValid}
                            loading={isSubmitting} floated='right' positive type='submit' content='Submit'/>
                        <Button as={Link} to={`/transactions/${transaction.id}`} floated='right' type='button'
                                content='Cancel'/>
                    </Form>
                )}
            </Formik>
        </Segment>
    )
})