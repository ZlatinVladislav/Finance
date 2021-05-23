export interface Bank{
    id:string;
    name:string;
}

export class BankFormValues {
    id?: string = undefined;
    name: string = '';

    constructor(bank?: BankFormValues) {
        if (bank) {
            this.id = bank.id;
            this.name = bank.name;
        }
    }
}

export class Bank implements Bank {
    constructor(init?: BankFormValues) {
        Object.assign(this, init);
    }
}