import TransactionStore from "./transactionStore";
import { createContext, useContext } from "react";
import CommonStore from "./commonStore";
import UserStore from "./userStore";
import ModalStore from "./modalStore";
import { TransactionType } from "../models/transactionType";
import TransactionTypeStore from "./transactionTypeStore";
import UserProfileStore from "./userProfileStore";

interface Store {
    transactionTypeStore:TransactionTypeStore
    transactionStore: TransactionStore
    commonStore: CommonStore
    userStore: UserStore
    modalStore: ModalStore
    userProfileStore:UserProfileStore
}

export const store: Store = {
    transactionTypeStore:new TransactionTypeStore(),
    transactionStore: new TransactionStore(),
    commonStore: new CommonStore(),
    userStore: new UserStore(),
    modalStore: new ModalStore(),
    userProfileStore:new UserProfileStore()
}

export const StoreContext = createContext(store);

export function useStore() {
    return useContext(StoreContext);
}