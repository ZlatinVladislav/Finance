import React, { useEffect } from "react";
import { Container } from "@material-ui/core";
import { useStore } from "../../../app/stores/store";
import LoadingComponent from "../../../app/layout/LoadingComponent";
import { useParams } from "react-router-dom";
import { observer } from "mobx-react-lite";
import TransactionDetailedHeader from "./TransactionDetailedHeader";
import TransactionDetailedInfo from "./TransactionDetailedInfo";
import { useStyles } from "../../../assets/pages";

export default observer(function TransactionDetails() {
    const classes = useStyles();
    const {transactionStore} = useStore();
    const {selectedTransaction: transaction, loadTransactions, loadingInitial,transactionRegistry} = transactionStore;
    const {id} = useParams<{ id: string }>();

    useEffect(() => {
        if (id) {
            loadTransactions(id);
        }
    }, [id, loadTransactions,transactionRegistry]);

    if (loadingInitial || !transaction) return <LoadingComponent/>;

    return (
        <Container maxWidth='md' className={classes.alignLeft} >
                <TransactionDetailedHeader transaction={transaction}/>
                <TransactionDetailedInfo transaction={transaction}/>
        </Container>
    )
})