import { Profiler } from "inspector";
import { makeAutoObservable, runInAction } from "mobx";
import { Photo, UserProfile } from "../models/profile";
import agent from "../api/agent";
import { store } from "./store";

export default class UserProfileStore {
    userProfile: UserProfile | null = null;
    loadingProfile = false;
    uploading = false;
    loading = false;

    constructor() {
        makeAutoObservable(this)
    }

    get isCurrentUser() {
        if (store.userStore.user && this.userProfile) {
            return store.userStore.user.username === this.userProfile.userName;
        }
        return false;
    }

    loadUserProfile = async (userName: string) => {
        this.loadingProfile = true;
        try {
            const userProfile = await agent.UserProfiles.get(userName);
            runInAction(() => {
                this.userProfile = userProfile;
                this.loadingProfile = false
            })
        } catch (error) {
            console.log(error);
            runInAction(() => this.loadingProfile = false)
        }
    }

    uploadPhoto = async (file: Blob) => {
        this.uploading = true;
        try {
            const response = await agent.UserProfiles.uploadPhoto(file);
            const photo = response.data
            runInAction(() => {
                if (this.userProfile) {
                    this.userProfile.photos?.push(photo);
                    if (photo.isMain && store.userStore.user) {
                        store.userStore.setImage(photo.url);
                        this.userProfile.image = photo.url;
                    }
                }
                this.uploading = false;
            })
        } catch (error) {
            console.log(error)
            runInAction(() => this.uploading = false);
        }
    }

    setMainPhoto = async (photo: Photo) => {
        this.loading = true;
        try {
            await agent.UserProfiles.setMainPhoto(photo.id);
            store.userStore.setImage(photo.url);
            runInAction(() => {
                if (this.userProfile && this.userProfile.photos) {
                    this.userProfile.photos.find(p => p.isMain)!.isMain = false;
                    this.userProfile.photos.find(p => p.id === photo.id)!.isMain = true;
                    this.userProfile.image = photo.url;
                    this.loading = false;
                }
            })
        } catch (error) {
            runInAction(() => this.loading = false)
            console.log(error)
        }
    }

    deletePhoto = async (photo: Photo) => {
        this.loading = true;
        try {
            await agent.UserProfiles.deletePhoto(photo.id);
            runInAction(() => {
                if (this.userProfile) {
                    this.userProfile.photos = this.userProfile.photos?.filter(p => p.id !== photo.id);
                    this.loading = false;
                }
            })
        } catch (error) {
            runInAction(() => this.loading = false)
            console.log(error)
        }
    }
}