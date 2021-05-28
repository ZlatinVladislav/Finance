import React from "react";
import Calendar from "react-calendar";
import { Typography, MenuItem, Container } from "@material-ui/core";
import { observer } from "mobx-react-lite";
import { useStore } from "../../../app/stores/store";
import { useStyles } from "../../../assets/pages";


export default observer(function TransactionFilters() {
    const classes = useStyles();
    const {transactionStore: {predicate, setPredicate}} = useStore();
    return (
        <>
            <Container className={classes.backgroundColorWhite}>
                <Typography variant='h3'>Filters</Typography>
                <MenuItem
                    disabled={predicate.has('all')}
                    onClick={() => setPredicate('all', 'true')}
                >All Transactions</MenuItem>
                <MenuItem
                    disabled={predicate.get('transactionStatus') === true}
                    onClick={() => setPredicate('transactionStatusIncome', 'true')}>
                    Income transactions</MenuItem>
                <MenuItem disabled={predicate.get('transactionStatus') === false}
                          onClick={() => setPredicate('transactionStatusOutcome', 'false')}>Outcome
                    transactions</MenuItem>
            </Container>
            <Typography style={{marginBottom:'20px'}}/>
            <Calendar
                onChange={(date => setPredicate('startDate', date as Date))}
                value={predicate.get('startDate') || new Date()}
            />
        </>
    )
})