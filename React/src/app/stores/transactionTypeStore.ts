import { makeAutoObservable, runInAction } from "mobx";
import agent from "../api/agent";
import { TransactionType } from "../models/transactionType";

export default class TransactionTypeStore {
    transactionTypeRegistry = new Map<string, TransactionType>();
    selectedTransactionType: TransactionType | undefined = undefined;
    editMode = false;
    loading = false;
    loadingInitial = false;

    constructor() {
        makeAutoObservable(this)
    }

    get transactionTypesAlphabetically() {
        return Array.from(this.transactionTypeRegistry.values())
    }

    loadingTransactions = async () => {
        this.loadingInitial = true;
        try {
            const transactionTypes = await agent.TransactionTypes.list();
            transactionTypes.transactionTypes.forEach(transactionType => {
                this.setTransactionType(transactionType);
            })
            this.setLoadingInitial(false);
        } catch (error) {
            console.log(error);
            this.setLoadingInitial(false);
        }
    }

    setRegistryClear = () => {
        this.transactionTypeRegistry.clear();
    }

    loadTransactionTypes = async (id: string) => {
        let transactionType = this.getTransactionType(id);
        if (transactionType) {
            this.selectedTransactionType = transactionType;
            return transactionType;
        } else {
            this.loadingInitial = true;
            try {
                transactionType = await agent.TransactionTypes.details(id);
                this.setTransactionType(transactionType);
                runInAction(() => {
                    this.selectedTransactionType = transactionType;
                })
                this.setLoadingInitial(false);
                return transactionType;
            } catch (error) {
                console.log(error);
                this.setLoadingInitial(false);
            }
        }
    }

    setLoadingInitial = (state: boolean) => {
        this.loadingInitial = state;
    }

    createTransactionType = async (transactionType: TransactionType) => {
        this.loading = true;
        try {
            await agent.TransactionTypes.create(transactionType);
            runInAction(() => {
                this.transactionTypeRegistry.set(transactionType.id, transactionType);
                this.selectedTransactionType = transactionType;
                this.editMode = false;
                this.loading = false;
            })
        } catch (error) {
            console.log(error)
            runInAction(() => {
                this.loading = false;
            })
        }
    }

    updateTransactionType = async (transactionType: TransactionType) => {
        this.loading = true;
        try {
            await agent.TransactionTypes.update(transactionType);
            runInAction(() => {
                this.transactionTypeRegistry.set(transactionType.id, transactionType);
                this.selectedTransactionType = transactionType;
                this.editMode = false;
                this.loading = false;
            })
        } catch (error) {
            console.log(error)
            runInAction(() => {
                this.loading = false;
            })
        }
    }

    deleteTransactionType = async (id: string) => {
        this.loading = true;
        try {
            await agent.TransactionTypes.delete(id);
            runInAction(() => {
                this.transactionTypeRegistry.delete(id);
                this.loading = false;
            })
        } catch (error) {
            console.log(error);
            runInAction(() => {
                this.loading = false;
            })
        }
    }

    private setTransactionType = (transactionType: TransactionType) => {
        this.transactionTypeRegistry.set(transactionType.id, transactionType);
    }

    private getTransactionType = (id: string) => {
        return this.transactionTypeRegistry.get(id);
    }
}
