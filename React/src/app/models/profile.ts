import { User } from "./user";

export interface UserProfile{
    userName:string;
    displayName:string;
    userDescription?:string;
    image?:string;
    bio?:string;
    photos?:Photo[]
}

export class UserProfile implements UserProfile{
    constructor(user:User) {
        this.userName=user.username;
        this.displayName=user.displayName;
        this.image=user.image;
    }
}

export interface Photo{
    id:string;
    url:string;
    isMain:boolean;
}