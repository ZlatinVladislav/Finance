import React, { useEffect, useState } from "react";
import { Grid } from "semantic-ui-react";
import TransactionList from "./TransactionList";
import { useStore } from "../../../app/stores/store";
import { observer } from "mobx-react-lite";
import LoadingComponent from "../../../app/layout/LoadingComponent";
import TransactionFilters from "./TransactionFilters";

export default observer(function TransactionDashboard() {
    const {transactionStore} = useStore();
    const {loadTransactions, transactionRegistry, loadingTransactions} = transactionStore;

    useEffect(() => {
        if (transactionRegistry.size <= 1) {
            loadingTransactions();
        }
    }, [transactionRegistry.size, loadTransactions]);

    if (transactionStore.loadingInitial) return <LoadingComponent content='Loading transactions...'/>

    return (
        <Grid>
            <Grid.Column width='10'>
                <TransactionList/>
            </Grid.Column>
            <Grid.Column width='6'>
                <TransactionFilters/>
            </Grid.Column>
        </Grid>

    );
})
  