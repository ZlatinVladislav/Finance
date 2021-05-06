import React, { useEffect } from "react";
import { Grid } from "semantic-ui-react";
import { useStore } from "../../../app/stores/store";
import LoadingComponent from "../../../app/layout/LoadingComponent";
import { useParams } from "react-router-dom";
import { observer } from "mobx-react-lite";
import TransactionDetailedHeader from "./TransactionDetailedHeader";
import TransactionDetailedInfo from "./TransactionDetailedInfo";
import TransactionDetailedChat from "./TransactionDetailedChat";
import TransactionDetailedSidebar from "./TransactionDetailedSidebar";

export default observer(function TransactionDetails() {
    const {transactionStore} = useStore();
    const {selectedTransaction: transaction, loadTransactions, loadingInitial} = transactionStore;
    const {id} = useParams<{ id: string }>();

    useEffect(() => {
        if (id) {
            loadTransactions(id);
        }
    }, [id, loadTransactions]);

    if (loadingInitial || !transaction) return <LoadingComponent/>;

    return (
        <Grid>
            <Grid.Column width={10}>
                <TransactionDetailedHeader transaction={transaction}/>
                <TransactionDetailedInfo transaction={transaction}/>
                <TransactionDetailedChat/>
            </Grid.Column>
            <Grid.Column width={6}>
                <TransactionDetailedSidebar/>
            </Grid.Column>
        </Grid>
    )
})