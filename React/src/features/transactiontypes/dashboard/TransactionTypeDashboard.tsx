import React, { useEffect, useState } from "react";
import { CircularProgress, Container} from "@material-ui/core"
import { useStore } from "../../../app/stores/store";
import { observer } from "mobx-react-lite";
import TransactionTypeLists from "./TransactionTypeLists";
import TransactionTypeButton from "./TransactionTypeFilters";
import { PagingParams } from "../../../app/models/pagination";
import TransactionListItemPlaceholder from "../../transactions/dashboard/TransactionListItemPlaceholder";
import InfiniteScroll from "react-infinite-scroller";
import { useStyles } from "../../../assets/pages";

export default observer(function TransactionTypeDashboard() {
    const classes = useStyles();
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

    return (
        <Container>
            <Container>
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
            </Container>
            <Container >
                <TransactionTypeButton/>
            </Container>
            <Container className={classes.alignCenter}>
                {loadingNext?
                <CircularProgress/>:false}
            </Container>
        </Container>
    );
})
