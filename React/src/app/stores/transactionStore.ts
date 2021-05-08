import { makeAutoObservable, runInAction } from "mobx";
import { Transaction } from "../models/transaction";
import agent from "../api/agent";
import { format } from "date-fns";

export default class TransactionStore {
    transactionRegistry = new Map<string, Transaction>();
    selectedTransaction: Transaction | undefined = undefined;
    editMode = false;
    loading = false;
    loadingInitial = false;

    constructor() {
        makeAutoObservable(this)
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
            const transactions = await agent.Transacions.list();
            transactions.transactions.forEach(transaction => {
                this.setTransaction(transaction);
            })
            this.setLoadingInitial(false);
        } catch (error) {
            console.log(error);
            this.setLoadingInitial(false);
        }
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

    createTransaction = async (transaction: Transaction) => {
        this.loading = true;
        try {
            await agent.Transacions.create(transaction);
            runInAction(() => {
                this.transactionRegistry.set(transaction.id, transaction);
                this.selectedTransaction = transaction;
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

    updateTransaction = async (transaction: Transaction) => {
        this.loading = true;
        try {
            await agent.Transacions.update(transaction);
            runInAction(() => {
                this.transactionRegistry.set(transaction.id, transaction);
                this.selectedTransaction = transaction;
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

    cancelTransaction=async ()=>{
        this.loading=true;
        try {
            await agent.Transacions.cancell(this.selectedTransaction!.id);
            runInAction(()=>{
                this.selectedTransaction!.isCanceled=!this.selectedTransaction?.isCanceled;
                this.transactionRegistry.set(this.selectedTransaction!.id,this.selectedTransaction!);
            })
        }catch (error) {
            console.log(error)
        }finally {
            runInAction(()=>this.loading=false)
        }
    }
}
