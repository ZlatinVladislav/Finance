import { makeAutoObservable, reaction, runInAction } from "mobx";
import agent from "../api/agent";
import { TransactionType, TransactionTypeFormValues } from "../models/transactionType";
import { Transaction, TransactionFormValues } from "../models/transaction";
import { PaginatedResult, Pagination, PagingParams } from "../models/pagination";

export default class TransactionTypeStore {
    transactionTypeRegistry = new Map<string, TransactionType>();
    transactionTypeOption = new Map<string, string>();
    selectedTransactionType: TransactionType | undefined = undefined;
    editMode = false;
    loading = false;
    loadingInitial = false;
    pagination: Pagination | null = null;
    predicate = new Map().set('all', 'true');
    pagingParams = new PagingParams();

    constructor() {
        makeAutoObservable(this)

        reaction(() => this.predicate.keys(), () => {
            this.pagingParams = new PagingParams();
            this.transactionTypeRegistry.clear();
            this.loadingTransactionTypes()
        })
    }

    setPagingParams = (pagingParams: PagingParams) => {
        this.pagingParams = pagingParams;
    }

    get axiosParams() {
        const params = new URLSearchParams();
        params.append('pageNumber', this.pagingParams.pageNumber.toString());
        params.append('pageSize', this.pagingParams.pageSize.toString());
        this.predicate.forEach(((value, key) => {
            params.append(key, value);
        }))
        return params;
    }

    get transactionTypesAlphabetically() {
        return Array.from(this.transactionTypeRegistry.values())
    }

    loadingTransactionTypes = async () => {
        this.loadingInitial = true;
        try {
            const result = await agent.TransactionTypes.list(this.axiosParams);
            result.data.forEach(transactionType => {
                this.setTransactionType(transactionType);
                this.setTransactionTypeOption(transactionType);
            })
            this.setPagination(result.pagination)
            this.setLoadingInitial(false);
        } catch (error) {
            console.log(error);
            this.setLoadingInitial(false);
        }
    }

    setPagination = (pagination: Pagination) => {
        this.pagination = pagination;
    }

    get transactionTypesOptionsArray() {
        return Array.from(this.transactionTypeOption, ([text, value]) => ({text, value}));
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

    createTransactionType = async (transactionType: TransactionTypeFormValues) => {
        try {
            await agent.TransactionTypes.create(transactionType);
            const newTransactionType = new TransactionType(transactionType);
            this.setTransactionType(newTransactionType);
            runInAction(() => {
                this.selectedTransactionType = newTransactionType;
            })
        } catch (error) {
            console.log(error)
        }
    }

    updateTransactionType = async (transactionType: TransactionTypeFormValues) => {
        try {
            await agent.TransactionTypes.update(transactionType);
            runInAction(() => {
                if (transactionType.id) {
                    let updatedTransactionType = {...this.getTransactionType(transactionType.id), ...transactionType}
                    this.transactionTypeRegistry.set(transactionType.id, updatedTransactionType as TransactionType);
                    this.selectedTransactionType = transactionType as TransactionType;
                    this.transactionTypeOption.clear()
                }
            })
        } catch (error) {
            console.log(error)
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

    private setTransactionTypeOption = (transactionType: TransactionType) => {
        this.transactionTypeOption.set(transactionType.transactionType, transactionType.id);
    }

    private getTransactionType = (id: string) => {
        return this.transactionTypeRegistry.get(id);
    }
}
