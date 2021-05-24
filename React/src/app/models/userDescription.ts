export interface UserDescription {
    userDescription: string;
}

export class UserDescriptionFormValues {
    userDescription: string = '';

    constructor(userDescription?: string) {
        if (userDescription) {
            this.userDescription = userDescription;
        }
    }
}

export class UserDescription implements UserDescription {
    constructor(init?: UserDescriptionFormValues) {
        Object.assign(this, init);
    }
}