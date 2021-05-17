import React, { Fragment } from "react";
import { useStore } from "../../../app/stores/store";
import { observer } from "mobx-react-lite";
import TransactionTypeListItem from "./TransactionTypeListItem";

export default observer(function TransactionList() {
    const {transactionTypeStore} = useStore();
    const {transactionTypesAlphabetically} = transactionTypeStore;

    return (
        <>
                <Fragment>
                    {transactionTypesAlphabetically.map((transactionType) => (
                        <TransactionTypeListItem key={transactionType.id} transactionType={transactionType}/>
                    ))}
                </Fragment>
        </>
    )
})