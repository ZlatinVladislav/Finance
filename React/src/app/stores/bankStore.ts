import { makeAutoObservable, reaction, runInAction } from "mobx";
import agent from "../api/agent";
import { Pagination, PagingParams } from "../models/pagination";
import { Bank } from "../models/bank";

export default class BankStore {
    bankRegistry = new Map<string, Bank>();
    bankOption = new Map<string, string>();
    selectedBank: Bank | undefined = undefined;
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
            this.bankRegistry.clear();
            this.loadingBanks()
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

    get banksAlphabetically() {
        return Array.from(this.bankRegistry.values())
    }

    loadingBanks = async () => {
        this.loadingInitial = true;
        try {
            const result = await agent.Banks.list();
            result.data.forEach(bank => {
                this.setBank(bank);
                this.setBankOption(bank);
            })
            this.setLoadingInitial(false);
        } catch (error) {
            console.log(error);
            this.setLoadingInitial(false);
        }
    }

    setPagination = (pagination: Pagination) => {
        this.pagination = pagination;
    }

    get banksOptionsArray() {
        return Array.from(this.bankOption, ([text, value]) => ({text, value}));
    }

    setRegistryClear = () => {
        this.bankRegistry.clear();
    }

    loadBanks = async (id: string) => {
        let bank = this.getBank(id);
        if (bank) {
            this.selectedBank = bank;
            return bank;
        } else {
            this.loadingInitial = true;
            try {
                bank = await agent.Banks.details(id);
                this.setBank(bank);
                runInAction(() => {
                    this.selectedBank = bank;
                })
                this.setLoadingInitial(false);
                return bank;
            } catch (error) {
                console.log(error);
                this.setLoadingInitial(false);
            }
        }
    }

    assignBank = async (bankId: string,transactionId:string) => {
            this.loadingInitial = true;
            try {
                await agent.Banks.assignBank(bankId,transactionId);
                this.setLoadingInitial(false);
            } catch (error) {
                console.log(error);
                this.setLoadingInitial(false);
            }
    }

    setLoadingInitial = (state: boolean) => {
        this.loadingInitial = state;
    }

    // createBank = async (bank: TransactionTypeFormValues) => {
    //     try {
    //         await agent.TransactionTypes.create(transactionType);
    //         const newTransactionType = new TransactionType(transactionType);
    //         this.setTransactionType(newTransactionType);
    //         runInAction(() => {
    //             this.selectedTransactionType = newTransactionType;
    //         })
    //     } catch (error) {
    //         console.log(error)
    //     }
    // }

    // updateTransactionType = async (transactionType: TransactionTypeFormValues) => {
    //     try {
    //         await agent.TransactionTypes.update(transactionType);
    //         runInAction(() => {
    //             if (transactionType.id) {
    //                 let updatedTransactionType = {...this.getTransactionType(transactionType.id), ...transactionType}
    //                 this.transactionTypeRegistry.set(transactionType.id, updatedTransactionType as TransactionType);
    //                 this.selectedTransactionType = transactionType as TransactionType;
    //                 this.transactionTypeOption.clear()
    //             }
    //         })
    //     } catch (error) {
    //         console.log(error)
    //     }
    // }

    // deleteTransactionType = async (id: string) => {
    //     this.loading = true;
    //     try {
    //         await agent.TransactionTypes.delete(id);
    //         runInAction(() => {
    //             this.transactionTypeRegistry.delete(id);
    //             this.loading = false;
    //         })
    //     } catch (error) {
    //         console.log(error);
    //         runInAction(() => {
    //             this.loading = false;
    //         })
    //     }
    // }

    private setBank = (bank: Bank) => {
        this.bankRegistry.set(bank.id, bank);
    }

    private setBankOption = (bank: Bank) => {
        this.bankOption.set(bank.name, bank.id);
    }

    private getBank = (id: string) => {
        return this.bankRegistry.get(id);
    }
}
