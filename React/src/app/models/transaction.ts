import { UserProfile } from "./profile";

export interface Transaction {
    id: string;
    money: number;
    transactionStatus: boolean;
    dateTransaction: Date | null;
    transactionType: string;
    isCanceled: boolean;
    userProfile?: UserProfile[]
}

export class TransactionFormValues {
    id?: string = undefined;
    money: number = 0;
    transactionStatus: boolean = false;
    dateTransaction: Date | null = null;
    transactionType: string = '';
    isCanceled: boolean = false;
    error: null

    constructor(transaction?: TransactionFormValues) {
        if (transaction) {
            this.id = transaction.id;
            this.money = transaction.money;
            this.dateTransaction = transaction.dateTransaction;
            this.transactionType = transaction.transactionType;
            this.isCanceled = transaction.isCanceled;
        }
    }
}

export class Transaction implements Transaction {
    constructor(init?: TransactionFormValues) {
        Object.assign(this, init);
    }
}