import React, { Fragment, useState } from "react";
import { Header } from "semantic-ui-react";
import { useStore } from "../../../app/stores/store";
import { observer } from "mobx-react-lite";
import TransactionListItem from "./TransactionListItem";

export default observer(function TransactionList() {
    const {transactionStore} = useStore();
    const {groupedTransactions} = transactionStore;

    return (
        <>
            {groupedTransactions.map(([group, transactions]) => (
                <Fragment key={group}>
                    <Header sub color='teal'>
                        {group}
                    </Header>
                    {transactions.map((transaction) => (
                        <TransactionListItem key={transaction.id} transaction={transaction}/>
                    ))}
                </Fragment>
            ))}
        </>
    )
})