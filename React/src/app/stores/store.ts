import TransactionStore from "./transactionStore";
import { createContext, useContext } from "react";
import CommonStore from "./commonStore";
import UserStore from "./userStore";
import ModalStore from "./modalStore";
import { TransactionType } from "../models/transactionType";
import TransactionTypeStore from "./transactionTypeStore";

interface Store {
    transactionTypeStore:TransactionTypeStore
    transactionStore: TransactionStore
    commonStore: CommonStore
    userStore: UserStore
    modalStore: ModalStore
}

export const store: Store = {
    transactionTypeStore:new TransactionTypeStore(),
    transactionStore: new TransactionStore(),
    commonStore: new CommonStore(),
    userStore: new UserStore(),
    modalStore: new ModalStore()
}

export const StoreContext = createContext(store);

export function useStore() {
    return useContext(StoreContext);
}