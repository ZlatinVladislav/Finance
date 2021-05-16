import React from "react";
import Calendar from "react-calendar";
import { Header, Menu } from "semantic-ui-react";
import { observer } from "mobx-react-lite";
import { useStore } from "../../../app/stores/store";

export default observer(function TransactionFilters() {
    const {transactionStore: {predicate, setPredicate}} = useStore();
    return (
        <>
            <Menu vertical size='large' style={{width: '100%', marginTop: 25}}>
                <Header icon='filter' attached color='teal' content='Filters'/>
                <Menu.Item
                    content='All Transactions'
                    active={predicate.has('all')}
                    onClick={() => setPredicate('all', 'true')}
                />
                <Menu.Item content="Income transactions"
                           active={predicate.get('transactionStatus')===true}
                           onClick={() => setPredicate('transactionStatusIncome', 'true')}/>
                <Menu.Item content="Outcome transactions"
                           active={predicate.get('transactionStatus')===false}
                           onClick={() => setPredicate('transactionStatusOutcome', 'false')}/>
            </Menu>
            <Header/>
            <Calendar
                onChange={(date => setPredicate('startDate', date as Date))}
                value={predicate.get('startDate') || new Date()}
            />
        </>
    )
})