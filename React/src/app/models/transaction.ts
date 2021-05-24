import { UserProfile } from "./profile";
import { Bank } from "./bank";

export interface Transaction {
    id: string;
    money: number;
    transactionStatus: boolean;
    dateTransaction: Date | null;
    transactionType: string;
    isCanceled: boolean;
    userProfile?: UserProfile[];
    bankDto?: Bank[];
}

export class TransactionFormValues {
    id?: string = undefined;
    money: number = 0;
    transactionStatus: boolean = false;
    dateTransaction: Date | null = null;
    transactionType: string = '';
    isCanceled: boolean = false;
    userProfile?: UserProfile[] = undefined;
    error: null

    constructor(transaction?: TransactionFormValues) {
        if (transaction) {
            this.id = transaction.id;
            this.money = transaction.money;
            this.dateTransaction = transaction.dateTransaction;
            this.transactionType = transaction.transactionType;
            this.isCanceled = transaction.isCanceled;
            this.userProfile = transaction.userProfile;
        }
    }
}

export class Transaction implements Transaction {
    constructor(init?: TransactionFormValues) {
        Object.assign(this, init);
    }
}