export interface TransactionType {
    id: string;
    transactionType: string;
}

export class TransactionTypeFormValues {
    id?: string = undefined;
    transactionType: string = '';

    constructor(transactionType?: TransactionTypeFormValues) {
        if (transactionType) {
            this.id = transactionType.id;
            this.transactionType = transactionType.transactionType;
        }
    }
}

export class TransactionType implements TransactionType {
    constructor(init?: TransactionTypeFormValues) {
        Object.assign(this, init);
    }
}