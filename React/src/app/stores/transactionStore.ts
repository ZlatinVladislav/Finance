import { makeAutoObservable, reaction, runInAction } from "mobx";
import { Transaction, TransactionFormValues } from "../models/transaction";
import agent from "../api/agent";
import { format } from "date-fns";
import { Pagination, PagingParams } from "../models/pagination";
import { toast } from "react-toastify";

export default class TransactionStore {
    transactionRegistry = new Map<string, Transaction>();
    selectedTransaction: Transaction | undefined = undefined;
    editMode = false;
    loading = false;
    loadingInitial = false;
    pagination: Pagination | null = null;
    pagingParams = new PagingParams();
    predicate = new Map().set('all', 'true');

    constructor() {
        makeAutoObservable(this)

        reaction(() => this.predicate.keys(), () => {
            this.pagingParams = new PagingParams();
            this.transactionRegistry.clear();
            this.loadingTransactions()
        })
    }

    setPagingParams = (pagingParams: PagingParams) => {
        this.pagingParams = pagingParams;
    }

    setPredicate = (predicate: string, value: string | Date) => {
        const resetPredicate = () => {
            this.predicate.forEach((value, key) => {
                if (key !== 'startDate') this.predicate.delete(key);
            })
        }
        switch (predicate) {
            case 'all':
                resetPredicate()
                this.predicate.set('all','true' );
                break;
            case'transactionStatusIncome':
                resetPredicate()
                this.predicate.set('transactionStatus', true);
                break;
            case'transactionStatusOutcome':
                resetPredicate()
                this.predicate.set('transactionStatus', false);
                break;
            case 'startDate':
                this.predicate.delete('startDate');
                this.predicate.set('startDate', value);
                break;
        }
    }

    get axiosParams() {
        const params = new URLSearchParams();
        params.append('pageNumber', this.pagingParams.pageNumber.toString());
        params.append('pageSize', this.pagingParams.pageSize.toString());
        this.predicate.forEach(((value, key) => {
            if (key === 'startDate') {
                params.append(key, (value as Date).toISOString())
            } else {
                params.append(key, value);
            }
        }))
        return params;
    }

    get transactionsByDate() {
        return Array.from(this.transactionRegistry.values()).sort((a, b) =>
            a.dateTransaction!.getTime() + b.dateTransaction!.getTime());
    }

    get groupedTransactions() {
        return Object.entries(
            this.transactionsByDate.reduce((transactions, transaction) => {
                const dateTransaction = format(transaction.dateTransaction!, 'dd MMM yyyy');
                transactions[dateTransaction] = transactions[dateTransaction] ? [...transactions[dateTransaction], transaction] : [transaction];
                return transactions;
            }, {} as { [key: string]: Transaction[] })
        )
    }

    loadingTransactions = async () => {
        this.loadingInitial = true;
        try {
            const result = await agent.Transacions.list(this.axiosParams);
            result.data.forEach(transaction => {
                this.setTransaction(transaction);
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

    setRegistryClear = () => {
        this.transactionRegistry.clear();
    }

    loadTransactions = async (id: string) => {
        let transaction = this.getTransaction(id);
        if (transaction) {
            this.selectedTransaction = transaction;
            return transaction;
        } else {
            this.loadingInitial = true;
            try {
                transaction = await agent.Transacions.details(id);
                this.setTransaction(transaction);
                runInAction(() => {
                    this.selectedTransaction = transaction;
                })
                this.setLoadingInitial(false);
                return transaction;
            } catch (error) {
                console.log(error);
                this.setLoadingInitial(false);
            }
        }
    }

    setLoadingInitial = (state: boolean) => {
        this.loadingInitial = state;
    }

    createTransaction = async (transaction: TransactionFormValues) => {
        try {
            await agent.Transacions.create(transaction);
            const newTransaction=new Transaction(transaction);
            this.setTransaction(newTransaction);
            runInAction(() => {
                this.selectedTransaction = newTransaction;
            })
        } catch (error) {
            console.log(error.response)
            throw error;
        }
    }

    updateTransaction = async (transaction: TransactionFormValues) => {
        try {
            await agent.Transacions.update(transaction);
            runInAction(() => {
                if(transaction.id){
                    let updatedTransaction={...this.getTransaction(transaction.id),...transaction}
                    this.transactionRegistry.set(transaction.id, updatedTransaction  as Transaction);
                    this.selectedTransaction = updatedTransaction as Transaction;
                }
            })
        } catch (error) {
            console.log(error)
            throw error;
        }
    }

    deleteTransaction = async (id: string) => {
        this.loading = true;
        try {
            await agent.Transacions.delete(id);
            runInAction(() => {
                this.transactionRegistry.delete(id);
                this.loading = false;
            })
        } catch (error) {
            console.log(error);
            runInAction(() => {
                this.loading = false;
            })
        }
    }

    private setTransaction = (transaction: Transaction) => {
        transaction.dateTransaction = new Date(transaction.dateTransaction!);
        this.transactionRegistry.set(transaction.id, transaction);
    }

    private getTransaction = (id: string) => {
        return this.transactionRegistry.get(id);
    }

    cancelTransaction = async () => {
        this.loading = true;
        try {
            await agent.Transacions.cancell(this.selectedTransaction!.id);
            runInAction(() => {
                this.selectedTransaction!.isCanceled = !this.selectedTransaction?.isCanceled;
                this.transactionRegistry.set(this.selectedTransaction!.id, this.selectedTransaction!);
            })
        } catch (error) {
            console.log(error)
        } finally {
            runInAction(() => this.loading = false)
        }
    }
}
