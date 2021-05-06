import { Profile } from "./profile";

export interface Transaction {
    money: number;
    transactionStatus: boolean;
    dateTransaction: Date | null;
    transactionTypeId: string;
    id: string;
    isCanceled?:boolean;
    transactions?:Profile[]
}

export interface TransactionList {
    transactions: Transaction[];
    id: string;
}

