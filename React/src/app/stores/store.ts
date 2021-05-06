import TransactionStore from "./transactionStore";
import { createContext, useContext } from "react";
import CommonStore from "./commonStore";
import UserStore from "./userStore";
import ModalStore from "./modalStore";

interface Store {
    transactionStore: TransactionStore
    commonStore: CommonStore
    userStore: UserStore
    modalStore: ModalStore
}

export const store: Store = {
    transactionStore: new TransactionStore(),
    commonStore: new CommonStore(),
    userStore: new UserStore(),
    modalStore: new ModalStore()
}

export const StoreContext = createContext(store);

export function useStore() {
    return useContext(StoreContext);
}