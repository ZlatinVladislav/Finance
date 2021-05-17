import React, { Fragment } from "react";
import { useStore } from "../../../app/stores/store";
import { observer } from "mobx-react-lite";
import TransactionListItem from "./TransactionListItem";
import { Typography } from "@material-ui/core";
import { useStyles } from "../../../assets/pages";

export default observer(function TransactionList() {
    const classes = useStyles();
    const {transactionStore} = useStore();
    const {groupedTransactions} = transactionStore;

    return (
        <>
            {groupedTransactions.map(([group, transactions]) => (
                <Fragment key={group}>
                    <Typography className={classes.colorTeal}>
                        {group}
                    </Typography>
                    {transactions.map((transaction) => (
                        <TransactionListItem key={transaction.id} transaction={transaction}/>
                    ))}
                </Fragment>
            ))}
        </>
    )
})