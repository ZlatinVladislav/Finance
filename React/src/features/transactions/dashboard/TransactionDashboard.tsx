import React, { useEffect, useState } from "react";
import { CircularProgress, Container } from "@material-ui/core";
import TransactionList from "./TransactionList";
import { useStore } from "../../../app/stores/store";
import { observer } from "mobx-react-lite";
import TransactionFilters from "./TransactionFilters";
import { PagingParams } from "../../../app/models/pagination";
import InfiniteScroll from "react-infinite-scroller";
import TransactionListItemPlaceholder from "./TransactionListItemPlaceholder";
import { useStyles } from "../../../assets/pages";

export default observer(function TransactionDashboard() {
    const {transactionStore} = useStore();
    const classes = useStyles();
    const {
        loadTransactions,
        transactionRegistry,
        loadingTransactions,
        setPagingParams,
        pagination,
        pagingParams
    } = transactionStore;
    const [loadingNext, setLoadingNext] = useState(false);

    function handleGetNext() {
        setLoadingNext(true);
        setPagingParams(new PagingParams(pagination!.currentPage + 1))
        loadingTransactions().then(() => setLoadingNext(false));
    }

    useEffect(() => {
        if (transactionRegistry.size <= 1) {
            loadingTransactions();
        }
    }, [transactionRegistry.size, loadTransactions, pagingParams]);

    return (
        <Container>
            <Container>
                {transactionStore.loadingInitial && !loadingNext ? (
                        <>
                            <TransactionListItemPlaceholder/>
                            <TransactionListItemPlaceholder/>
                        </>
                    ) :
                    <InfiniteScroll pageStart={0} loadMore={handleGetNext}
                                    hasMore={!loadingNext && !!pagination && pagination.currentPage < pagination.totalPages}
                                    initialLoad={false}>
                        <TransactionList/>
                        <Container className={classes.alignCenter}>
                            {loadingNext ?
                                <CircularProgress/> : false}
                        </Container>
                    </InfiniteScroll>
                }
            </Container>
            <Container className={classes.positionRight}>
                <TransactionFilters/>
            </Container>
        </Container>
    );
})
  