import React, { useEffect, useState } from "react";
import { Button, Grid, Loader } from "semantic-ui-react";
import TransactionList from "./TransactionList";
import { useStore } from "../../../app/stores/store";
import { observer } from "mobx-react-lite";
import LoadingComponent from "../../../app/layout/LoadingComponent";
import TransactionFilters from "./TransactionFilters";
import { PagingParams } from "../../../app/models/pagination";
import InfiniteScroll from "react-infinite-scroller";
import TransactionListItemPlaceholder from "./TransactionListItemPlaceholder";

export default observer(function TransactionDashboard() {
    const {transactionStore} = useStore();
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

    // if (transactionStore.loadingInitial && !loadingNext) return <LoadingComponent content='Loading transactions...'/>

    return (
        <Grid>
            <Grid.Column width='10'>
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
                    </InfiniteScroll>
                }
            </Grid.Column>
            <Grid.Column width='6'>
                <TransactionFilters/>
            </Grid.Column>
            <Grid.Column width={10}>
                <Loader active={loadingNext}/>
            </Grid.Column>
        </Grid>
    );
})
  