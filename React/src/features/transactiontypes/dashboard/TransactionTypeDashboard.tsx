import React, { useEffect, useState } from "react";
import { Grid, Loader } from "semantic-ui-react";
import { useStore } from "../../../app/stores/store";
import { observer } from "mobx-react-lite";
import LoadingComponent from "../../../app/layout/LoadingComponent";
import TransactionTypeLists from "./TransactionTypeLists";
import TransactionTypeButton from "./TransactionTypeFilters";
import { PagingParams } from "../../../app/models/pagination";
import TransactionListItemPlaceholder from "../../transactions/dashboard/TransactionListItemPlaceholder";
import InfiniteScroll from "react-infinite-scroller";
import TransactionList from "../../transactions/dashboard/TransactionList";

export default observer(function TransactionTypeDashboard() {
    const {transactionTypeStore} = useStore();
    const {
        transactionTypeRegistry,
        loadingTransactionTypes,
        loadTransactionTypes,
        setPagingParams,
        pagination,
        pagingParams
    } = transactionTypeStore;
    const [loadingNext, setLoadingNext] = useState(false);

    function handleGetNext() {
        setLoadingNext(true);
        setPagingParams(new PagingParams(pagination!.currentPage + 1))
        loadingTransactionTypes().then(() => setLoadingNext(false));
    }

    useEffect(() => {
        if (transactionTypeRegistry.size <= 1) {
            loadingTransactionTypes();
        }
    }, [transactionTypeRegistry.size, pagingParams,loadTransactionTypes]);

   // if (transactionTypeStore.loadingInitial) return <LoadingComponent content='Loading transaction types...'/>

    return (
        <Grid>
            <Grid.Column width='10'>
                {transactionTypeStore.loadingInitial && !loadingNext ? (
                        <>
                            <TransactionListItemPlaceholder/>
                            <TransactionListItemPlaceholder/>
                        </>
                    ) :
                    <InfiniteScroll pageStart={0} loadMore={handleGetNext}
                                    hasMore={!loadingNext && !!pagination && pagination.currentPage < pagination.totalPages}
                                    initialLoad={false}>
                        <TransactionTypeLists/>
                    </InfiniteScroll>
                }
            </Grid.Column>
            <Grid.Column width='6'>
                <TransactionTypeButton/>
            </Grid.Column>
            <Grid.Column width={10}>
                <Loader active={loadingNext}/>
            </Grid.Column>
        </Grid>
    );
})
