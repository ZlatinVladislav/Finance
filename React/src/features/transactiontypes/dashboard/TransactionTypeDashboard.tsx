import React, { useEffect, useState } from "react";
import { Grid } from "semantic-ui-react";
import { useStore } from "../../../app/stores/store";
import { observer } from "mobx-react-lite";
import LoadingComponent from "../../../app/layout/LoadingComponent";
import TransactionTypeLists from "./TransactionTypeLists";

export default observer(function TransactionTypeDashboard() {
    const {transactionTypeStore} = useStore();
    const {transactionTypeRegistry, loadingTransactions} = transactionTypeStore;

    useEffect(() => {
        if (transactionTypeRegistry.size <= 1) {
            loadingTransactions();
        }
    }, [transactionTypeRegistry.size]);

    if (transactionTypeStore.loadingInitial) return <LoadingComponent content='Loading transaction types...'/>

    return (
        <Grid>
            <Grid.Column width='10'>
                <TransactionTypeLists/>
            </Grid.Column>
            <Grid.Column width='6'>
                {/*<TransactionFilters/>*/}
            </Grid.Column>
        </Grid>

    );
})
