import React, { useEffect } from "react";
import { Container } from "@material-ui/core"
import { useStore } from "../../../app/stores/store";
import { observer } from "mobx-react-lite";
import TransactionListItemPlaceholder from "../../transactions/dashboard/TransactionListItemPlaceholder";
import BankListItem from "./BankListItem";

export default observer(function BankDashboard() {
    const {bankStore} = useStore();
    const {
        bankOption,
        bankRegistry,
        loadingBanks,
        loadBanks,
        pagingParams
    } = bankStore;


    useEffect(() => {
        if (bankRegistry.size <= 1) {
            loadingBanks();
        }
    }, [bankRegistry.size, pagingParams, loadBanks]);

    return (
        <Container>
            <Container>
                {bankStore.loadingInitial ? (
                        <>
                            <TransactionListItemPlaceholder/>
                            <TransactionListItemPlaceholder/>
                        </>
                    ) :
                    <BankListItem bankOption={bankOption}/>
                }
            </Container>
        </Container>
    );
})
